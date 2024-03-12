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
    public DeckEditorCard tempOwnedCard;
    public DeckEditorCard tempDeckCard;

    [SerializeField]
    public Sprite cardHighlight;
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
                bool isUnique = true;
                int holdQuantity = 1;
                mergeCard = ownedCards[i];
                foreach(DeckEditorCard checkCard in this.transform.GetComponentsInChildren<DeckEditorCard>()) 
                {
                    //Debug.Log("CHECKING" + checkCard.title);
                    if(checkCard.title == mergeCard.title)
                    {
                        //Debug.Log("SAME CARD");
                        checkCard.ownedQuantity += 1;
                        holdQuantity = checkCard.ownedQuantity;
                        isUnique = false;
                    }
                    
                }

                if(isUnique == true)
                {
                    DeckEditorCard cardRender = Instantiate(cardPrefab, scrollViewContent);
                    //ownedCards[i] = cardRender.GetComponent<Card>();
                
                    /*mergeCard.cardNumber = i;
                    cardRender.editNumber = i;*/
                    cardRender.name = mergeCard.title;

                    cardRender.ownedQuantity = 1;
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
                else
                {
                    DeckEditorCard cardRender = Instantiate(cardPrefab, scrollViewContent);
                    //ownedCards[i] = cardRender.GetComponent<Card>();
                
                    /*mergeCard.cardNumber = i;
                    cardRender.editNumber = i;*/
                    cardRender.name = mergeCard.title;
 
                    cardRender.ownedQuantity = holdQuantity;
                    cardRender.debug_ID = mergeCard.debug_ID;
                    cardRender.title = mergeCard.title;
                    cardRender.art = mergeCard.art;
                    cardRender.playTarget = (DeckEditorCard.TargetType)mergeCard.playTarget;
                    cardRender.playDescription = mergeCard.playDescription;
                    cardRender.playEffects = mergeCard.playEffects;
                    cardRender.throwTarget = (DeckEditorCard.TargetType)mergeCard.throwTarget;
                    cardRender.throwDescription = mergeCard.throwDescription;
                    cardRender.throwEffects = mergeCard.throwEffects;
                    cardRender.transform.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < deck.Count; i++)
            {
                bool isUnique = true;
                int holdQuantity = 1;
                mergeCard = deck[i];
                foreach(DeckEditorCard checkCard in this.transform.GetComponentsInChildren<DeckEditorCard>()) 
                {
                    //Debug.Log("CHECKING" + checkCard.title);
                    if(checkCard.title == mergeCard.title)
                    {
                        //Debug.Log("SAME CARD");
                        checkCard.deckQuantity += 1;
                        holdQuantity = checkCard.deckQuantity;
                        isUnique = false;
                    }
                }

                if(isUnique == true)
                {
                    DeckEditorCard cardRender = Instantiate(cardPrefab, scrollViewContent);
                    //ownedCards[i] = cardRender.GetComponent<Card>();
                
                    /*mergeCard.cardNumber = i;
                    cardRender.editNumber = i;*/
                    cardRender.name = mergeCard.title;

                    cardRender.deckQuantity = 1;
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
                else
                {
                    DeckEditorCard cardRender = Instantiate(cardPrefab, scrollViewContent);
                    //ownedCards[i] = cardRender.GetComponent<Card>();
                
                    /*mergeCard.cardNumber = i;
                    cardRender.editNumber = i;*/
                    cardRender.name = mergeCard.title;
                    
                    cardRender.deckQuantity = holdQuantity;
                    cardRender.debug_ID = mergeCard.debug_ID;
                    cardRender.title = mergeCard.title;
                    cardRender.art = mergeCard.art;
                    cardRender.playTarget = (DeckEditorCard.TargetType)mergeCard.playTarget;
                    cardRender.playDescription = mergeCard.playDescription;
                    cardRender.playEffects = mergeCard.playEffects;
                    cardRender.throwTarget = (DeckEditorCard.TargetType)mergeCard.throwTarget;
                    cardRender.throwDescription = mergeCard.throwDescription;
                    cardRender.throwEffects = mergeCard.throwEffects;
                    cardRender.transform.gameObject.SetActive(false);
                }
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
                    
                    if(card.ownedQuantity > 0)
                    {
                        foreach(DeckEditorCard checkCard in GameObject.Find("OwnedCardsContent").transform.GetComponentsInChildren<DeckEditorCard>(true)) 
                        {
                            //Debug.Log("CHECKING" + checkCard.title);
                            if(checkCard.title == card.title && checkCard.gameObject.activeSelf == false)
                            {
                                tempOwnedCard = checkCard;
                                break;
                            }
                        }
                        if(card.ownedQuantity == 1)
                        {
                            tempOwnedCard = card;
                        }
                        tempOwnedCard.transform.gameObject.SetActive(false);
                        tempOwnedCard.transform.SetParent(GameObject.Find("DeckContent").transform);
                        bool isRepeat = false;
                        foreach(DeckEditorCard checkCard in GameObject.Find("DeckContent").transform.GetComponentsInChildren<DeckEditorCard>()) 
                        {
                            //Debug.Log("CHECKING" + checkCard.title);
                            
                            if(checkCard.title == tempOwnedCard.title && checkCard.gameObject.activeSelf == true)
                            {
                                checkCard.deckQuantity += 1;
                                isRepeat = true;
                                break;
                            }
                        }
                        if(!isRepeat)
                        {
                            tempOwnedCard.transform.gameObject.SetActive(true);
                            tempOwnedCard.deckQuantity = 1;
                        }
                        tempOwnedCard.transform.GetChild(1).gameObject.SetActive(false);
                        tempOwnedCard.transform.GetChild(4).gameObject.SetActive(false);
                        tempOwnedCard.transform.GetChild(5).gameObject.SetActive(true);

                        RectTransform rt = tempOwnedCard.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
                                //Debug.Log(rt.sizeDelta);
                        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 2.85f);
                        rt.anchoredPosition = new Vector2(0, 60f);
                        //Debug.Log(rt.sizeDelta);
                        tempOwnedCard.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = null;
                        tempOwnedCard.transform.GetChild(2).gameObject.SetActive(false);
                        tempOwnedCard.transform.GetChild(0).gameObject.SetActive(true);
                        card.ownedQuantity -= 1;
                        card.deckQuantity += 1;  
                    }
                    else
                    {
                        card.transform.SetParent(GameObject.Find("DeckContent").transform);
                        card.transform.GetChild(1).gameObject.SetActive(false);
                        card.transform.GetChild(4).gameObject.SetActive(false);
                        card.transform.GetChild(5).gameObject.SetActive(true);

                        RectTransform rt = card.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
                        //Debug.Log(rt.sizeDelta);
                        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 2.85f);
                        rt.anchoredPosition = new Vector2(0, 60f);
                        //Debug.Log(rt.sizeDelta);
                        card.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = null;
                        card.transform.GetChild(2).gameObject.SetActive(false);
                        card.transform.GetChild(0).gameObject.SetActive(true);
                        card.ownedQuantity -= 1;
                        card.deckQuantity += 1;
                    }
                    
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
                    
                    if(card.deckQuantity > 0)
                    {
                        foreach(DeckEditorCard checkCard in GameObject.Find("DeckContent").transform.GetComponentsInChildren<DeckEditorCard>(true)) 
                        {
                            //Debug.Log("CHECKING" + checkCard.title);
                            if(checkCard.title == card.title && checkCard.gameObject.activeSelf == false)
                            {
                                tempDeckCard = checkCard;
                                break;
                            }
                        }
                        if(card.deckQuantity == 1)
                        {
                            tempDeckCard = card;
                        }
                        tempDeckCard.transform.gameObject.SetActive(false);
                        tempDeckCard.transform.SetParent(GameObject.Find("OwnedCardsContent").transform);
                        bool isRepeat = false;
                        foreach(DeckEditorCard checkCard in GameObject.Find("OwnedCardsContent").transform.GetComponentsInChildren<DeckEditorCard>()) 
                        {
                            //Debug.Log("CHECKING" + checkCard.title);
                            if(checkCard.title == tempDeckCard.title && checkCard.gameObject.activeSelf == true)
                            {
                                checkCard.ownedQuantity += 1;
                                isRepeat = true;
                                break;
                            }
                        }
                        if(!isRepeat)
                        {
                            tempDeckCard.transform.gameObject.SetActive(true);
                            tempDeckCard.ownedQuantity = 1;
                        }
                        tempDeckCard.transform.GetChild(1).gameObject.SetActive(true);
                        tempDeckCard.transform.GetChild(5).gameObject.SetActive(false);
                        tempDeckCard.transform.GetChild(4).gameObject.SetActive(true);
    
                        RectTransform rt = tempDeckCard.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
                        //Debug.Log(rt.sizeDelta);
                        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 12.33f);
                        rt.anchoredPosition = new Vector2(0, 0);
                        //Debug.Log(rt.sizeDelta);
                        tempDeckCard.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = cardHighlight;
                        tempDeckCard.transform.GetChild(2).gameObject.SetActive(false);
                        tempDeckCard.transform.GetChild(0).gameObject.SetActive(false);
                        card.ownedQuantity += 1;
                        card.deckQuantity -= 1;    
                    }
                    //Debug.Log(card.parent);
                    else
                    {
                        card.transform.SetParent(GameObject.Find("OwnedCardsContent").transform);

                        card.transform.GetChild(1).gameObject.SetActive(true);
                        card.transform.GetChild(5).gameObject.SetActive(false);
                        card.transform.GetChild(4).gameObject.SetActive(true);
    
                        RectTransform rt = card.transform.GetChild(2).gameObject.GetComponent<RectTransform>();
                        //Debug.Log(rt.sizeDelta);
                        rt.sizeDelta = new Vector2 (rt.sizeDelta.x, 12.33f);
                        rt.anchoredPosition = new Vector2(0, 0);
                        //Debug.Log(rt.sizeDelta);
                        card.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = cardHighlight;
                        card.transform.GetChild(2).gameObject.SetActive(false);
                        card.transform.GetChild(0).gameObject.SetActive(false);
                        card.ownedQuantity += 1;
                        card.deckQuantity -= 1;
                    }
                    break;
                }
            }
        }
    }
}