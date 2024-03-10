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
    [SerializeField] private DeckEditorCard cardPrefab;
    [SerializeField] private int isdeck;

    //private Dictionary<EditorCardPile, List<DeckEditorCard>> pileToList = null;

    [Header("Global")]
    [ReadOnly] public int w;
    [ReadOnly] public int h;
    public int cardFontSize = 50;

    [Header("Cards")]
    public int globalYOffset;
    public int xWidth;
    public Vector2 cardOffset = new();
    public Vector2 cardDimensions = new(200,300);
    public CardUser user;
    private bool highlighted;
    
    /*private GUIStyle cardStyle;
    Texture2D normalBackground, hoverBackground;*/

    public List<Card> ownedCards;
    public List<Card> deck;
    public Card mergeCard;
    //public bool isdeckvalue;

    private void Awake()
    {
        // Awake is called before Start().
        // ================
        
        ownedCards = user.ownedCards;
        deck = user.deck;
        /*pileToList = new()
        {
            {user.ownedCards.data, ownedCards},
            {user.deck, deck},
        };*/
        


        if(isdeck == 0)
        {
            for (int i = 0; i < ownedCards.Count; i++)
            {
                DeckEditorCard cardRender = Instantiate(cardPrefab, scrollViewContent);
                //ownedCards[i] = cardRender.GetComponent<Card>();
                mergeCard = ownedCards[i];
                /*mergeCard.cardNumber = i;
                cardRender.editNumber = i;*/
 
                cardRender.debug_ID = mergeCard.debug_ID;
                cardRender.title = mergeCard.title;
                cardRender.art = mergeCard.art;
                cardRender.playTarget = (DeckEditorCard.TargetType)mergeCard.playTarget;
                cardRender.playDescription = mergeCard.playDescription;
                cardRender.playEffects = mergeCard.playEffects;
                cardRender.throwTarget = (DeckEditorCard.TargetType)mergeCard.throwTarget;
                cardRender.throwDescription = mergeCard.throwDescription;
                cardRender.throwEffects = mergeCard.throwEffects;
            }
        }
        else
        {
            for (int i = 0; i < deck.Count; i++)
            {
                DeckEditorCard cardRender = Instantiate(cardPrefab, scrollViewContent);
                //deck[i] = cardRender.GetComponent<Card>();
                mergeCard = deck[i];
 
                cardRender.debug_ID = mergeCard.debug_ID;
                cardRender.title = mergeCard.title;
                cardRender.art = mergeCard.art;
                cardRender.playTarget = (DeckEditorCard.TargetType)mergeCard.playTarget;
                cardRender.playDescription = mergeCard.playDescription;
                cardRender.playEffects = mergeCard.playEffects;
                cardRender.throwTarget = (DeckEditorCard.TargetType)mergeCard.throwTarget;
                cardRender.throwDescription = mergeCard.throwDescription;
                cardRender.throwEffects = mergeCard.throwEffects;
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewContent);

        /*isdeckvalue = isdeck;
        Debug.Log(isdeckvalue);*/

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
        EditorSwitch(editorCard, ownedCards, deck);
        
        ownedCards = user.ownedCards;
        deck = user.deck;

        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewContent);
    }

    // ================================================================
    // Pile-editing methods
    // ================================================================

    private void EditorSwitch(DeckEditorCard card, List<Card> ownPile, List<Card> deckPile)
    {
        // Attempts to remove card from fromPile and pushes it to the start
        // of toPile. Raises an error if fromPile does not contain card.
        // ================

        if(card.transform.parent.name == "OwnedCardsContent")
        {
            //Debug.Log("OWNED CARD IN THE THIS");
            foreach(var checkname in ownPile)
            {
                if (checkname.title == card.title)
                {
                    ownPile.Remove(checkname);
                    deckPile.Insert(0, checkname);

                    //Debug.Log(card.parent);
                    card.transform.SetParent(GameObject.Find("DeckContent").transform);
                    card.transform.GetChild(1).gameObject.SetActive(false);
                    RectTransform rt = card.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
                    Debug.Log(rt.sizeDelta);
                    rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 2.85f);
                    rt.anchoredPosition = new Vector2(0, 60f);
                    Debug.Log(rt.sizeDelta);
                    card.transform.GetChild(0).gameObject.SetActive(true);

                    break;
                }
            }
        }
        else if(card.transform.parent.name == "DeckContent")
        {
            //Debug.Log("THIS IS IN THE DECK");
            foreach(var checkname in deckPile)
            {
                if (checkname.title == card.title)
                {
                    deckPile.Remove(checkname);
                    ownPile.Insert(0, checkname);

                    //Debug.Log(card.parent);
                    card.transform.SetParent(GameObject.Find("OwnedCardsContent").transform);
                    card.transform.GetChild(1).gameObject.SetActive(true);
                    card.transform.GetChild(0).gameObject.SetActive(false);

                    break;
                }
            }
        }
    }
}