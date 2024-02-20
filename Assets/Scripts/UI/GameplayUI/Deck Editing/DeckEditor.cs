using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum EditorCardPile { ownedCards, NULL, deck }

public class DeckEditor : MonoBehaviour
{
    public int maxCopies = 5;
    public int minDeckSize = 10;

    // Debug contents.
    [SerializeField] private RectTransform scrollViewContent;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] public List<Card> ownedCards = null;
    [SerializeField] public List<Card> deck = null;
    [SerializeField] public bool isdeck;

    private Dictionary<EditorCardPile, List<Card>> pileToList = null;

    [Header("Global")]
    [ReadOnly] public int w;
    [ReadOnly] public int h;
    public int cardFontSize = 50;

    [Header("Cards")]
    public int globalYOffset;
    public int xWidth;
    public Vector2 cardOffset = new();
    public Vector2 cardDimensions = new(200,300);
    private CardUser user;    

    private GUIStyle cardStyle;
    Texture2D normalBackground, hoverBackground;



    private void Awake()
    {
        // Awake is called before Start().
        // ================
        pileToList = new()
        {
            {EditorCardPile.NULL, null},
            {EditorCardPile.ownedCards, ownedCards},
            {EditorCardPile.deck, deck},
        };
        // Create custom style for cards
        normalBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
        normalBackground.SetPixel(0, 0, new Color(0, 0, 0, 1f));
        normalBackground.Apply();
        // Create custom style for cards that are being hovered over
        hoverBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
        hoverBackground.SetPixel(0, 0, new Color(0.025f, 0.025f, 0.05f, 1f));
        hoverBackground.Apply();


        if(!isdeck)
        {
            foreach (Card card in ownedCards)
            {
                Debug.Log("card");
                GameObject cardRender = Instantiate(cardPrefab, scrollViewContent);
            }
        }
        else
        {
            foreach (Card card in deck)
            {
                Debug.Log("deck");
                GameObject cardRender = Instantiate(cardPrefab, scrollViewContent);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewContent);


    }

    // ================================================================
    // Public interface methods
    // ================================================================
    private void OnGUI()
    {
        w = Screen.width;
        h = Screen.height;
        
        cardStyle = new GUIStyle(GUI.skin.box); 
        cardStyle.normal.textColor = Color.white;
        cardStyle.normal.background = normalBackground;
        cardStyle.hover.textColor = Color.cyan;
        cardStyle.hover.background = hoverBackground;
        cardStyle.fontSize = cardFontSize;
    }

    /*public void ShuffleDiscardIntoDrawpile()
    {
        // We shuffle the discard pile first, and then pop it to draw pile.
        // This way, if we're shuffling into a non-empty deck, only the new
        // cards get shuffled, and the pre-existing ones maintain their order
        // in the draw pile.
        // ================

        Shuffle(discardPile);
        PopFromPushTo(discardPile, drawPile, discardPile.Count);
    }*/


    /*public void Discard(Card card)
    {
        RemoveFromPushTo(card, hand, discardPile);
    }*/
    
    public void MoveCard(Card card, EditorCardPile fromPile, EditorCardPile toPile)
    {
        // Acts as a public accessor for the RemoveFromPushTo function.
        // Moves a card from fromPile to the start of toPile.
        //
        // Should be used for status effects.
        // ================

        RemoveFromPushTo(card, pileToList[fromPile], pileToList[toPile]);
    }

    public Card GetRandom(EditorCardPile pile)
    {
        List<Card> pileList = pileToList[pile];
        return pileList[Random.Range(0, pileList.Count)];
    }

    // ================================================================
    // Pile-editing methods
    // ================================================================


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