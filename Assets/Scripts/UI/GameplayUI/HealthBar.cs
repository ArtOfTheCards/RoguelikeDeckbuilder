using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class HealthBar : MonoBehaviour
{
    [SerializeField, Tooltip("")]
    private Damagable damagable;
    [SerializeField, Tooltip("")]
    private Image mainBar;
    [SerializeField, Tooltip("")]
    public Image ghostBar;

    [Header("Animation Parameters")]
    [SerializeField, Tooltip("How long it takes to animate a health change.\n\nDefault: 0.5")]
    float lerpTime = 0.5f;
    [Foldout("Main Colors"), SerializeField, Tooltip("The color of the mainBar at max health.")]
    private Color maxMainColor = Color.green;
    [Foldout("Main Colors"), SerializeField, Tooltip("The color of the mainBar at min health.")]
    private Color minMainColor = Color.red;
    [Foldout("Ghost Colors"), SerializeField, Tooltip("The color of the ghostBar at max health.")]
    private Color maxGhostColor = Color.red;
    [Foldout("Ghost Colors"), SerializeField, Tooltip("The color of the ghostBar at min health.")]
    private Color minGhostColor = Color.black;
    
    private int lastHealth;


    private void Start()
    {
        lastHealth = damagable.MaxHealth;
        mainBar.fillAmount = ghostBar.fillAmount = 1;
    }

    private void Update()
    {
        // Edge detection for when our damagable's health changes.
        // ================

        if (lastHealth != damagable.CurrentHealth)
        {
            StopCoroutine(nameof(AnimateChange));
            StartCoroutine(AnimateChange(lastHealth, damagable.CurrentHealth));
            lastHealth = damagable.CurrentHealth;
        }
    }

    private IEnumerator AnimateChange(int before, int after)
    {
        // Given an initial and final health, animate the mainBar and ghostBar.
        // ================

        Image first, second;
        Color firstMax, firstMin, secondMax, secondMin;

        if (after < before) // if losing health
        {
            first = mainBar;
            firstMax = maxMainColor;
            firstMin = minMainColor;
            second = ghostBar;
            secondMax = maxGhostColor;
            secondMin = minGhostColor;
        }
        else // if gaining health
        {
            first = ghostBar;
            firstMax = maxGhostColor;
            firstMin = minGhostColor;
            second = mainBar;
            secondMax = maxMainColor;
            secondMin = minMainColor;
        }

        float lerpBefore = HealthToLerp(before);
        float lerpAfter = HealthToLerp(after);

        // ================
        // First!
        // ================
        float elapsed = 0;
        while (elapsed < lerpTime)
        {
            first.fillAmount = Mathf.Lerp(lerpBefore, lerpAfter, LerpKit.EaseOut(elapsed/lerpTime));
            first.color = Color.Lerp(firstMin, firstMax, first.fillAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ================
        // Second!
        // ================
        elapsed = 0;
        while (elapsed < lerpTime)
        {
            second.fillAmount = Mathf.Lerp(lerpBefore, lerpAfter, LerpKit.EaseOut(elapsed/lerpTime));
            second.color = Color.Lerp(secondMin, secondMax, second.fillAmount);
            elapsed += Time.deltaTime;
            yield return null;
        }

        first.fillAmount = second.fillAmount = HealthToLerp(after);
    }

    private float HealthToLerp(int health) { return health/(float)damagable.MaxHealth; }

}