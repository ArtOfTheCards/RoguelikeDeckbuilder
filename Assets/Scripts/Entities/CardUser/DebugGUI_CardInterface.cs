using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using FMOD.Studio;
using UnityEngine.Localization;

public class DebugGUI_CardInterface : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool debugMode = false;

    [Header("Global")]
    [ReadOnly] public int w;
    [ReadOnly] public int h;
    [ReadOnly] public bool canPlay = true;
    public int pileLabelHeight = 50;
    public int pileFontSize = 50;
    public int pileLabelFontSize = 50;
    public int cardFontSize = 50;
    public int buttonFontSize = 50;

    [Header("Drawpile")]

    public Vector2 drawOffset = new();
    public Vector2 drawDimensions = new(200, 200);
    public int timerHeight = 16;

    [Header("Cards")]
    public int globalYOffset;
    public int xWidth;
    public Vector2 cardOffset = new();
    public Vector2 cardDimensions = new(200, 300);
    public Vector2 buttonMargins = new();
    public Vector2 playButtonOffset = new();
    public Vector2 throwButtonOffset = new();
    private CardUser user;
    [Header("Discard Pile")]
    public Vector2 discardOffset = new();
    public Vector2 discardDimensions = new(200, 200);

    [Header("Projectile (Temporary)")]
    public ProjectileManager projectileMng;
    public Damagable temporaryTarget;

    private GUIStyle pileStyle, pileLabelStyle, cardStyle, buttonStyle;
    Texture2D normalBackground, hoverBackground;


    private TargetAffiliation affiliation;
    private EventInstance hoverSound;
    private Vector2 lastMousePos = new Vector2();

    [Header("Localization")]
    public LocalizedString drawPileText;
    public LocalizedString playText;
    public LocalizedString throwText;
    public LocalizedString discardPileText;




    private void Awake()
    {
        user = GetComponent<CardUser>();
        affiliation = GetComponentInChildren<Targetable>().affiliation;

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
            fontSize = pileFontSize
        };

        pileLabelStyle = new(GUI.skin.box)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = pileLabelFontSize
        };

        cardStyle = new GUIStyle(GUI.skin.box);
        cardStyle.normal.textColor = Color.white;
        cardStyle.normal.background = normalBackground;
        cardStyle.hover.textColor = Color.cyan;
        cardStyle.hover.background = hoverBackground;
        cardStyle.fontSize = cardFontSize;

        buttonStyle = new(GUI.skin.button)
        {
            fontSize = buttonFontSize
        };

        // ================
        // Draw cards
        // ================

        GUI.Box(new Rect(0 + drawOffset.x,
                         h - drawOffset.y - drawDimensions.y - pileLabelHeight - 20 - timerHeight,
                         drawDimensions.x * (user.DrawTimer / user.drawDelay),
                         timerHeight), "", cardStyle);
        GUI.Box(new Rect(0 + drawOffset.x,
                         h - drawOffset.y - drawDimensions.y - pileLabelHeight - 10,
                         drawDimensions.x,
                         pileLabelHeight), $"{drawPileText.GetLocalizedString()}", pileLabelStyle);
        GUI.Box(new Rect(0 + drawOffset.x,
                         h - drawOffset.y - drawDimensions.y,
                         drawDimensions.x,
                         drawDimensions.y), $"{user.drawPile.Count}", pileStyle);

        // ================
        // Play cards
        // ================
        for (int i = 0; i < user.hand.Count; i++)
        {
            Card card = user.hand[i];
            int offsetX = Mathf.RoundToInt(((i + 1) * xWidth / (user.hand.Count + 1)) + (w - xWidth) / 2 - cardDimensions.x / 2);
            int offsetY = Mathf.RoundToInt(h - (globalYOffset + (i * cardOffset.y)) - cardDimensions.y);

            string text = $"{card.title}\n{card.playDescription}\n\n\n{card.throwDescription}";
            string cardContext = $"{card.title.GetLocalizedString()}\n{card.playDescription.GetLocalizedString()}\n\n\n{card.throwDescription.GetLocalizedString()}";

            Rect r = new Rect(offsetX, offsetY, cardDimensions.x, cardDimensions.y);
            if (r.Contains(Event.current.mousePosition) && !r.Contains(lastMousePos))
            {
                TriggerHoverSFX();
            }

            GUI.Box(r, $"{cardContext}", cardStyle);

            if (canPlay)
            {
                if (GUI.Button(new Rect(offsetX + buttonMargins.x / 2 + playButtonOffset.x,
                                        offsetY + buttonMargins.y / 2 + playButtonOffset.y,
                                        cardDimensions.x - buttonMargins.x,
                                        cardDimensions.y - buttonMargins.y), $"{playText.GetLocalizedString()}", buttonStyle))
                {
                    if (card.playTarget == Card.TargetType.Direct)
                    {
                        // Begin listening for a targetable target.
                        // If we get one, callback to a properly-parameterized UseCard call.
                        StartCoroutine(GetTargetTargetable((targetable) => user.UseCard(card, Card.UseMode.Play, targetable)));
                    }

                    if (card.playTarget == Card.TargetType.Worldspace)
                    {
                        // Begin listening for a targetable target.
                        // If we get one, callback to a properly-parameterized UseCard call.
                        StartCoroutine(GetTargetVector3((position) => user.UseCard(card, Card.UseMode.Play, position)));
                    }

                    if (card.playTarget == Card.TargetType.Targetless)
                    {
                        user.UseCard(card, Card.UseMode.Play);
                    }
                }

                if (GUI.Button(new Rect(offsetX + buttonMargins.x / 2 + throwButtonOffset.x,
                                        offsetY + buttonMargins.y / 2 + throwButtonOffset.y,
                                        cardDimensions.x - buttonMargins.x,
                                        cardDimensions.y - buttonMargins.y), $"{throwText.GetLocalizedString()}", buttonStyle))
                {
                    if (debugMode)
                        Debug.Log("ey: card type is " + card.throwTarget);

                    if (card.throwTarget == Card.TargetType.Direct)
                    {
                        // Begin listening for a targetable target.
                        // If we get one, callback to a properly-parameterized UseCard call.

                        // throw projectile
                        projectileMng.throwNext(temporaryTarget, card);
                        StartCoroutine(GetTargetTargetable((targetable) =>
                        {
                            user.UseCard(card, Card.UseMode.Throw, targetable);
                        }));
                    }

                    if (card.throwTarget == Card.TargetType.Worldspace)
                    {
                        // Begin listening for a targetable target.
                        // If we get one, callback to a properly-parameterized UseCard call.
                        StartCoroutine(GetTargetVector3((position) => user.UseCard(card, Card.UseMode.Throw, position)));
                    }

                    if (card.throwTarget == Card.TargetType.Targetless)
                    {
                        if (debugMode)
                            Debug.Log("ey: throw at TARGETLESS");

                        projectileMng.throwNext(temporaryTarget, card);
                        user.UseCard(card, Card.UseMode.Throw);
                    }
                }
            }

        }
        lastMousePos = Event.current.mousePosition;
        // ================
        // Dis cards
        // ================
        GUI.Box(new Rect(w - discardOffset.x - discardDimensions.x,
                         h - discardOffset.y - discardDimensions.y - pileLabelHeight - 10,
                         discardDimensions.x,
                         pileLabelHeight), $"{discardPileText.GetLocalizedString()}", pileLabelStyle);
        GUI.Box(new Rect(w - discardOffset.x - discardDimensions.x,
                         h - discardOffset.y - discardDimensions.y,
                         discardDimensions.x,
                         discardDimensions.y), $"{user.discardPile.Count}", pileStyle);
    }

    private void Start() 
    {
        hoverSound = AudioManager.instance.CreateEventInstance(FMODEvents.instance.CardOnHover);
    }

    private void TriggerHoverSFX()
    {
        PLAYBACK_STATE playbackState;
        hoverSound.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            hoverSound.start();
        }
    }

    private IEnumerator GetTargetTargetable(System.Action<Targetable> action)
    {
        // Awaits a Targetable target, provided by mouseclick. When a target is
        // obtained, calls the supplied action with that target as argument.
        // ================

        canPlay = false;
        Targetable target = null;

        if (debugMode)
            Debug.Log("ey: waiting for a click (L for select / R for escape)");
        while (target == null)
        {
            if (Input.GetMouseButtonDown(0))        // select target
            {
                Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                List<Targetable> targetables = new();

                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.TryGetComponent<Targetable>(out Targetable newTarget))
                    {
                        if (newTarget.affiliation != affiliation)
                        {
                            if (debugMode)
                                Debug.Log("ey, ADDED NEW TARGET");
                            targetables.Add(newTarget);
                        }
                    }
                }

                if (targetables.Count > 0)
                {
                    target = targetables[0];
                }

                // Break whether or not we found a target. If we clicked on a nontarget, 
                // then our action is effectively canceled.
                if (debugMode)
                    Debug.Log("ey: break");
                break;
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (debugMode)
                    Debug.Log("ey: break");
                break; // cancel action
            }

            yield return null;
        }

        if (debugMode)
            Debug.Log("EY: exit while loop");

        canPlay = true;
        // If we got a target (ie if the action wasn't canceled)...
        if (target != null) action.Invoke(target);

    }

    private IEnumerator GetTargetVector3(System.Action<Vector3> action)
    {
        // Awaits a Vector3 target, provided by mouseclick. When a target is
        // obtained, calls the supplied action with that target as argument.
        // ================

        canPlay = false;
        Vector3 min = new(float.MinValue, float.MinValue);
        Vector3 target = min;

        while (target == min)
        {
            if (Input.GetMouseButtonDown(0))        // select target
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
                break;
            }

            if (Input.GetMouseButtonDown(1)) break; // cancel action

            yield return null;
        }

        canPlay = true;
        // If we got a target (ie if the action wasn't canceled)...
        if (target != min) action.Invoke(target);
    }
}


