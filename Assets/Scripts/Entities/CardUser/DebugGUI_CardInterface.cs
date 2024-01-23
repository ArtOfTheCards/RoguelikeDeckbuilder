using UnityEngine;

public class DebugGUI_CardInterface : MonoBehaviour
{
    private CardUser user;
    private void Awake()
    {
        user = GetComponent<CardUser>();
    }

    private void OnGUI()
    {
        // ================
        // Draw cards
        // ================
        GUI.Box(new Rect(10,10,100,30), $"Drawpile: {user.drawPile.Count}");

        // ================
        // Play cards
        // ================
        GUI.Box(new Rect(120,10,100,30*(user.hand.Count+1)), "Play cards");
    
        for (int i = 0; i < user.hand.Count; i++)
        {
            Card card = user.hand[i];
            if(GUI.Button(new Rect(130,40 + (30*i),80,20), $"{card}"))
            {
                user.PlayCard(card);

                if (card.ID == "Revive")
                {
                    user.MoveCard(user.GetRandom(CardPile.discardPile), CardPile.discardPile, CardPile.hand);
                }
            }
        }

        // ================
        // Dis cards
        // ================
        GUI.Box(new Rect(230,10,100,30), $"Discard: {user.discardPile.Count}");
    }
}