using UnityEngine;
using System.Collections.Generic;
using System.Collections;
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
    [SerializeField] public List<DeckEditorCard> ownedCards = null;
    [SerializeField] public List<DeckEditorCard> deck = null;
    [SerializeField] public bool isdeck;

    private Dictionary<EditorCardPile, List<DeckEditorCard>> pileToList = null;

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
    private bool highlighted;
    
    /*private GUIStyle cardStyle;
    Texture2D normalBackground, hoverBackground;*/



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
        


        if(!isdeck)
        {
            for (int i = 0; i < ownedCards.Count; i++)
            {
                Debug.Log("card");
                GameObject cardRender = Instantiate(cardPrefab, scrollViewContent);
                ownedCards[i] = cardRender.GetComponent<DeckEditorCard>();
            }
        }
        else
        {
            for (int i = 0; i < deck.Count; i++)
            {
                Debug.Log("deck");
                GameObject cardRender = Instantiate(cardPrefab, scrollViewContent);
                deck[i] = cardRender.GetComponent<DeckEditorCard>();
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewContent);


    }

    // ================================================================
    // Public interface methods
    // ================================================================
    private void OnGUI()
    {
        
        /*w = Screen.width;
        h = Screen.height;
        
        // Create custom style for cards
        normalBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
        normalBackground.SetPixel(0, 0, new Color(0, 0, 0, 1f));
        normalBackground.Apply();
        // Create custom style for cards that are being hovered over
        hoverBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
        hoverBackground.SetPixel(0, 0, new Color(0.025f, 0.025f, 0.05f, 1f));
        hoverBackground.Apply();

        cardStyle = new GUIStyle(GUI.skin.box); 
        cardStyle.normal.textColor = Color.white;
        cardStyle.normal.background = normalBackground;
        cardStyle.hover.textColor = Color.cyan;
        cardStyle.hover.background = hoverBackground;
        cardStyle.fontSize = cardFontSize;*/
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
    public void SwitchPile(DeckEditorCard editorCard)
    {
        RemoveFromPushTo(editorCard, ownedCards, deck);
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewContent);
    }

    // ================================================================
    // Pile-editing methods
    // ================================================================

    private void RemoveFromPushTo(DeckEditorCard card, List<DeckEditorCard> fromPile, List<DeckEditorCard> toPile)
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