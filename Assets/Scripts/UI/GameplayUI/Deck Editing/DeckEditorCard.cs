using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckEditorCard : MonoBehaviour
{
    // Start is called before the first frame update
    DeckEditor deckEditor;

    System.Action onClick;

    /// <MAKING A CARD>
    public enum UseMode { NULL, Play, Throw }
    public enum TargetType { NULL, Direct, Worldspace, Targetless }
    public string debug_ID = "New ID";
    public string title = "New Card";
    public Sprite art;
    public TargetType playTarget = TargetType.NULL;
    public string playDescription;
    public List<CardEffect> playEffects = new();
    public TargetType throwTarget = TargetType.NULL;
    public string throwDescription;
    public List<CardEffect> throwEffects = new();
    /// <CARD>
    public int editNumber;

    void Start()
    {
        deckEditor = FindObjectOfType<DeckEditor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked()
    {
        Debug.Log(title);
        deckEditor.SwitchPile(this);
        //onClick?.Invoke();
    }
}
