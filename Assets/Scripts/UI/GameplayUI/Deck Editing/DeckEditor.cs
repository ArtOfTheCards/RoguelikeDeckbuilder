using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public enum CardPile { ownedCards, NULL, deck }

public class DeckEditor : MonoBehaviour
{
    public int maxCopies = 5;
    public int minDeckSize = 10;

    // Debug contents.
    [SerializeField]
    private Card[] deck = new Card[] {};
    private Card[] ownedCards = new Card[] {};

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
            {CardPile.ownedCards, ownedCards},
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
            if (hand.Count < maxHandSize)
            {
                DrawCards(normalDrawAmount);
                DrawTimer = 0;
            }
            else
            {
                DrawTimer = drawDelay;
            }
        }

        DrawTimer += Time.deltaTime;
    }

    // ================================================================
    // Public interface methods
    // ================================================================

    public void ShuffleDiscardIntoDrawpile()
    {
        // We shuffle the discard pile first, and then pop it to draw pile.
        // This way, if we're shuffling into a non-empty deck, only the new
        // cards get shuffled, and the pre-existing ones maintain their order
        // in the draw pile.
        // ================

        Shuffle(discardPile);
        PopFromPushTo(discardPile, drawPile, discardPile.Count);
    }

    public void DrawCards(int n)
    {
        // Tries to push n number of cards from the drawPile and pushes them 
        // into our hand. Pushes as many cards as possible if it can't push n
        // number of cards.
        // ================

        if (drawPile.Count < n)
        {
            ShuffleDiscardIntoDrawpile();
        }
        PopFromPushTo(drawPile, hand, Mathf.Min(n, drawPile.Count));

        
    }

    public void Discard(Card card)
    {
        RemoveFromPushTo(card, hand, discardPile);
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

    public void UseCard(Card card, Card.UseMode useMode, Targetable target, bool removeFromDeck=false)
    {
        // TARGETABLE target: Uses a card with the given useMode {Play, Throw}.
        // ================

        if (ValidateUse(card, useMode) == false) { return; }

        if (!removeFromDeck) Discard(card);
        card.Use(this, useMode, target);
    }

    public void UseCard(Card card, Card.UseMode useMode, Vector3 target, bool removeFromDeck=false)
    {
        // VECTOR3 target: Uses a card with the given useMode {Play, Throw}.
        // ================

        if (ValidateUse(card, useMode) == false) { return; }

        card.Use(this, useMode, target);
        if (!removeFromDeck) Discard(card);
    }

    public void UseCard(Card card, Card.UseMode useMode, bool removeFromDeck=false)
    {
        // TARGETLESS target: Uses a card with the given useMode {Play, Throw}.
        // ================

        if (ValidateUse(card, useMode) == false) { return; }

        card.Use(this, useMode);
        if (!removeFromDeck) Discard(card);
    }

    private bool ValidateUse(Card card, Card.UseMode useMode)
    {
        if (useMode == Card.UseMode.NULL)
        {
            Debug.LogError($"CardUser Error. UseCard failed. NULL is not a valid UseMode.", this);
            return false;
        }
        if (!hand.Contains(card))
        {
            Debug.LogError($"CardUser Error. UseCard failed. Hand does not contain card {card}.", this);
            return false;
        }
        return true;
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