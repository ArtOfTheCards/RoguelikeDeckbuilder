using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckEditorCard : MonoBehaviour
{
    // Start is called before the first frame update
    DeckEditor deckEditor;

    System.Action onClick;

    void Start()
    {
        deckEditor = FindObjectOfType<DeckEditor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchLists()
    {
        deckEditor.SwitchPile(this);
    }

    public void clicked()
    {
        onClick?.Invoke();
    }
}
