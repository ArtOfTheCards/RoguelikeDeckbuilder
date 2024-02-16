using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    // ================================================================
    // Parameters
    // ================================================================


    [SerializeField, Tooltip("The lifetime of this object.")]
    float destroyDelay = 3f;

    [Header("Position")]
    [SerializeField, Tooltip("The BASE X-Y position offset.\n\nDefault: (0,0)")]
    private Vector2 baseOffset = new();
    [SerializeField, Tooltip("The magnitude of the RANDOM X-Y position offset.\n\nDefault: (0.25,0.25)")]
    private Vector2 randomOffsetMagnitude = new(0.25f, 0.25f);
    
    [Header("Velocity")]
    [SerializeField, Tooltip("The (min, max) magnitude range of this indicator's velocity.\n\nDefault: (1,3)")]
    private Vector2 velocityMagnitudeRange = new(1, 3);
    [SerializeField, Tooltip("The (min, max) angle range of this indicator's velocity.\nIMPORTANT: 0 is straight up.\n\nDefault: (-60,60)")]
    private Vector2 angleRange = new(-60, 60);
    [SerializeField, Tooltip("Our final speed as a proportion of our initial speed.\n\nDefault: 0.5 (aka, we end with half the speed).")]
    private float finalSpeedFactor = 0.5f;

    // ================================================================
    // Internal variables
    // ================================================================

    private Vector2 velocity, baseVelocity, finalVelocity;
    private TMP_Text tmp_Text;
    private Color baseColor, baseClear;
    private float elapsed = 0;

    // ================================================================
    // Initializer and update methods
    // ================================================================

    public void Initialize(int value, Vector3 worldpoint)
    {
        if (TryGetComponent<TMP_Text>(out tmp_Text))
        {
            tmp_Text.text = $"{value}";
            baseColor = tmp_Text.color;
            baseClear = new(baseColor.r, baseColor.g, baseColor.b, 0);
        }

        transform.position = worldpoint 
                           + new Vector3(Random.Range(-randomOffsetMagnitude.x, randomOffsetMagnitude.x), 
                                         Random.Range(-randomOffsetMagnitude.y, randomOffsetMagnitude.y))
                           + (Vector3)baseOffset;

        float magnitude = Random.Range(velocityMagnitudeRange.x, velocityMagnitudeRange.y);
        float angle = Random.Range(angleRange.x, angleRange.y);
        baseVelocity = velocity = magnitude * AngleToVector(angle);
        finalVelocity = baseVelocity * finalSpeedFactor;

        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
        if (tmp_Text != null)
        {
            tmp_Text.color = Color.Lerp(baseColor, baseClear, elapsed/destroyDelay);
            velocity = Vector2.Lerp(baseVelocity, finalVelocity, elapsed/destroyDelay);
            elapsed += Time.deltaTime;
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    // ================================================================
    // Helper methods
    // ================================================================

    private Vector2 AngleToVector(float D)
    {
        // Returns the Vector2 representation of a unit vector pointing up, rotated by D degrees.
        // ================

        float radians = Mathf.PI*D/180f;

        float x = (float)Mathf.Cos(radians + (Mathf.PI/2f));
        float y = (float)Mathf.Sin(radians + (Mathf.PI/2f));
        return new(x,y);
        
    }
}