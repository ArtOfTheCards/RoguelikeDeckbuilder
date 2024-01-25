using UnityEngine;
using NaughtyAttributes;

public class DebugGUI_CardInterface : MonoBehaviour
{
    [Header("Global")]
    [ReadOnly] public int w;
    [ReadOnly] public int h;
    public Targetable debug_target;
    [Header("Drawpile")]
    public Vector2 drawOffset = new();
    public Vector2 drawDimensions = new(200,200);
    [Header("Cards")]
    public int globalYOffset;
    public int xWidth;
    public Vector2 cardOffset = new();
    public Vector2 cardDimensions = new(200,300);
    public Vector2 buttonMargins = new();
    public Vector2 playButtonOffset = new();
    public Vector2 throwButtonOffset = new();
    private CardUser user;
    [Header("Drawpile")]
    public Vector2 discardOffset = new();
    public Vector2 discardDimensions = new(200,200);

    private GUIStyle pileStyle, cardStyle;
    Texture2D normalBackground, hoverBackground;

    private void Awake()
    {
        user = GetComponent<CardUser>();

        // Create custom style for cards
        normalBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
        normalBackground.SetPixel(0, 0, new Color(0, 0, 0, 1f));
        normalBackground.Apply();
        // Create custom style for cards that are being hovered over
        hoverBackground = new Texture2D(1, 1, TextureFormat.RGBAFloat, false); 
        hoverBackground.SetPixel(0, 0, new Color(0.025f, 0.025f, 0.05f, 1f));
        hoverBackground.Apply();
    }

    private void OnGUI()
    {
        w = Screen.width;
        h = Screen.height;

        pileStyle = new(GUI.skin.box)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 50
        };
        
        cardStyle = new GUIStyle(GUI.skin.box); 
        cardStyle.normal.textColor = Color.white;
        cardStyle.normal.background = normalBackground;
        cardStyle.hover.textColor = Color.cyan;
        cardStyle.hover.background = hoverBackground;

        // ================
        // Draw cards
        // ================
    
        GUI.Box(new Rect(0+drawOffset.x,
                         h-drawOffset.y-drawDimensions.y-45,
                         drawDimensions.x * (user.DrawTimer/user.drawDelay),
                         8), "");
        GUI.Box(new Rect(0+drawOffset.x,
                         h-drawOffset.y-drawDimensions.y-30,
                         drawDimensions.x,
                         20), $"Drawpile");
        GUI.Box(new Rect(0+drawOffset.x,
                         h-drawOffset.y-drawDimensions.y,
                         drawDimensions.x,
                         drawDimensions.y), $"{user.drawPile.Count}", pileStyle);

        // ================
        // Play cards
        // ================
        for (int i = 0; i < user.hand.Count; i++)
        {
            Card card = user.hand[i];
            int offsetX = Mathf.RoundToInt(((i+1)*xWidth/(user.hand.Count+1)) + (w-xWidth)/2 - cardDimensions.x/2);
            int offsetY = Mathf.RoundToInt(h - (globalYOffset + (i*cardOffset.y)) - cardDimensions.y);

            string text = $"{card.title}\n{card.playDescription}\n\n\n\n{card.throwDescription}";

            GUI.Box(new Rect(offsetX,offsetY,cardDimensions.x, cardDimensions.y), $"{text}", cardStyle);

            if (GUI.Button(new Rect(offsetX+buttonMargins.x/2+playButtonOffset.x,
                                    offsetY+buttonMargins.y/2+playButtonOffset.y,
                                    cardDimensions.x-buttonMargins.x, 
                                    cardDimensions.y-buttonMargins.y), $"Play"))
            {
                card.Use(Card.UseMode.Play, debug_target);
                user.Discard(card);
            }

            if (GUI.Button(new Rect(offsetX+buttonMargins.x/2+throwButtonOffset.x,
                                    offsetY+buttonMargins.y/2+throwButtonOffset.y,
                                    cardDimensions.x-buttonMargins.x, 
                                    cardDimensions.y-buttonMargins.y), $"Throw"))
            {
                card.Use(Card.UseMode.Throw, debug_target);
                user.Discard(card);
            }
        }

        // ================
        // Dis cards
        // ================
        GUI.Box(new Rect(w-discardOffset.x-discardDimensions.x,
                         h-discardOffset.y-discardDimensions.y-30,
                         discardDimensions.x,
                         20), $"Discard");
        GUI.Box(new Rect(w-discardOffset.x-discardDimensions.x,
                         h-discardOffset.y-discardDimensions.y,
                         discardDimensions.x,
                         discardDimensions.y), $"{user.discardPile.Count}", pileStyle);
    }
}