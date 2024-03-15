using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EffectIcon : MonoBehaviour
{
    private Image image;
    private TMP_Text text;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }

    public void Initialize(Sprite sprite, int stacks)
    {
        image.sprite = sprite;
        text.text = stacks.ToString();
    }
}