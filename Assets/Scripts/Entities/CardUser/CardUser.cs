using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public enum CardPile { NULL, drawPile, hand, discardPile }

public class CardUser : MonoBehaviour
{
    public int startingHandSize = 3;
    public int normalDrawAmount = 1;
    public float drawDelay = 3f;

    // Debug contents.
    [SerializeField]
    private Card[] DEBUG_startingDeck = new Card[] {};

    // Piles of cards used in gameplay.
    [ReadOnly] public List<Card> drawPile = null;
    [ReadOnly] public List<Card> hand = new();
    [ReadOnly] public List<Card> discardPile = new();
    // Used as a quick converter between our CardPile enum and our actual card lists.
    private Dictionary<CardPile, List<Card>> pileToList = null;

    // Counts down the time until drawing a new card.
    public float DrawTimer { get; private set; } = 0;

    private void Awake()
    {
        // Awake is called before Start().
        // ================

        pileToList = new()
        {
            {CardPile.NULL, null},
            {CardPile.drawPile, drawPile},
            {CardPile.hand, hand},
            {CardPile.discardPile, discardPile},
        };

        drawPile = new List<Card>(DEBUG_startingDeck);
        Shuffle(drawPile);
                  // Don't draw more cards than we have.
        DrawCards(Mathf.Min(startingHandSize, drawPile.Count));
    }

    private void Update()
    {
        // Cards are drawn regularly over time. Update() detects when it's time
        // to do so.
        // ================

        if (DrawTimer > drawDelay)
        {
            DrawCards(normalDrawAmount);
            DrawTimer = 0;
        }

        DrawTimer += Time.deltaTime;
    }

    // ================================================================
    // Public interface methods
    // ================================================================

    public void ShuffleDiscardIntoDrawpile()
    {
        PopFromPushTo(discardPile, drawPile, discardPile.Count);
        Shuffle(drawPile);
    }

    public void DrawCards(int n)
    {
        // Tries to push n number of cards from the drawPile and pushes them 
        // into our hand. Pushes as many cards as possible if it can't push n
        // number of cards.
        // ================

        if (drawPile.Count == 0)
        {
            ShuffleDiscardIntoDrawpile();
        }

                                      // Don't draw more cards than we have.
        PopFromPushTo(drawPile, hand, Mathf.Min(n, drawPile.Count));
    }

    public void Discard(Card card)
    {
        RemoveFromPushTo(card, hand, discardPile);
    }

    public void UseCard<T>(Card card, Card.UseMode useMode, T target, bool removeFromDeck=false)
    {
        // Uses a card with the given useMode {Play, Throw}.
        // ================

        if (useMode == Card.UseMode.NULL) 
        {
            Debug.LogError($"CardUser Error. UseCard failed. NULL is not a valid UseMode.", this);
            return;
        }
        if (!hand.Contains(card)) 
        {
            Debug.LogError($"CardUser Error. UseCard failed. Hand does not contain card {card}.", this);
            return;
        }
        if (target is not Targetable && target is not Vector3)
        {
            Debug.LogError($"CardUser Error. UseCard failed. Target must be either of type Targetable or Vector3, not {typeof(T)}.", this);
            return;
        }

        Debug.Log(useMode);
        card.Use(this, useMode, target);

        if (!removeFromDeck) Discard(card);
    }

    public void MoveCard(Card card, CardPile fromPile, CardPile toPile)
    {
        // Acts as a public accessor for the RemoveFromPushTo function.
        // Moves a card from fromPile to the start of toPile.
        //
        // Should be used for status effects.
        // ================

        RemoveFromPushTo(card, pileToList[fromPile], pileToList[toPile]);
    }

    public Card GetRandom(CardPile pile)
    {
        List<Card> pileList = pileToList[pile];
        return pileList[Random.Range(0, pileList.Count)];
    }
    
    // ================================================================
    // Pile-editing methods
    // ================================================================

    private void Shuffle(List<Card> pile)
    {
        // Shuffles list pile in place using the Fisher-Yates algorithm.
        // ================

        for (int i = pile.Count-1; i > 0; i--)
        {
            SwapItems(pile, i, Random.Range(0,i));
        }
    }

    private void SwapItems<T>(List<T> pile, int indexA, int indexB)
    {
        // In-place swaps items in a list.
        // ================

        (pile[indexB], pile[indexA]) = (pile[indexA], pile[indexB]);
    }

    private void PopFromPushTo(List<Card> fromPile, List<Card> toPile, int number=1)
    {
        // Simulates a queuelike pop-push operation on two cardlists. Removes 
        // n = number cards from the end of fromPile, and adds them to the
        // start of toPile.
        // ================

        if (fromPile.Count < number)
        {
            Debug.LogError($"CardUser Error. PopFromPushTo failed. There are "
                         + $"fewer cards in fromPile ({fromPile.Count}) than "
                         + $"the number requested ({number}).", this);
            return;
        }

        for (int i = 0; i < number; i++)
        {
            Card card = fromPile[^1];
            fromPile.RemoveAt(fromPile.Count-1);
            toPile.Insert(0, card);
        }
    }

    private void RemoveFromPushTo(Card card, List<Card> fromPile, List<Card> toPile)
    {
        // Attempts to remove card from fromPile and pushes it to the start
        // of toPile. Raises an error if fromPile does not contain card.
        // ================

        if (!fromPile.Contains(card))
        {
            Debug.LogError($"CardUser Error. RemoveCardPushTo failed. fromPile does not contain card {card}.", this);
        }
        else 
        {
            fromPile.Remove(card);
            toPile.Insert(0, card);
        }
    }
}