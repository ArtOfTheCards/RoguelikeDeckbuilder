using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

[System.Serializable]
public class Card
{
    public string ID = "";
    public virtual void Play() { Debug.Log($"Played {ID}"); }

    public Card(string _ID) 
    { 
        ID = _ID;
    }

    public override string ToString() { return ID; }
}

public enum CardPile { NULL, drawPile, hand, discardPile }

class CardUser : MonoBehaviour
{
    [Tooltip("The ...")]
    public int startingHandSize = 3;
    public int normalDrawAmount = 1;
    public float drawDelay = 3f;

    // Debug contents.
    private Card[] DEBUG_startingDeck = new Card[]
    {
        new("Strike"), new("Strike"), new("Strike"), new("Strike"), 
        new("Defend"), new("Defend"), new("Defend"), new("Defend"), 
        new("Poison Strike"), new("Poison Strike"), 
        new("Snake Eyes"),
        new("Revive"),
        new("Smooth Moves"), 
        new("Jackpot!"),
    };

    // Piles of cards used in gameplay.
    [ReadOnly] public List<Card> drawPile = null;
    [ReadOnly] public List<Card> hand = new();
    [ReadOnly] public List<Card> discardPile = new();
    // Used as a quick converter between our CardPile enum and our actual card lists.
    private Dictionary<CardPile, List<Card>> pileToList = null;

    // Counts down the time until drawing a new card.
    private float drawTimer = 0;

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

        if (drawTimer > drawDelay)
        {
            DrawCards(normalDrawAmount);
            drawTimer = 0;
        }

        drawTimer += Time.deltaTime;
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

    public void PlayCard(Card card)
    {
        // Plays a card and discards it.
        // ================

        if (!hand.Contains(card))
        {
            Debug.LogError($"CardUser Error. PlayCard failed. Hand does not contain card {card}.", this);
        }
        else 
        {
            card.Play();
            Discard(card);
        }
    }

    public void PlayCardFromIndex(int i)
    {
        // Plays a card in the hand located at index i.
        // ================

        if (i >= hand.Count)
        {
            Debug.LogError($"CardUser Error. PlayCardFromIndex failed. Hand ({hand.Count}) is too small for the queried index ({i}).", this);
        }
        else 
        {
            hand[i].Play();
            Discard(hand[i]);
        }   
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