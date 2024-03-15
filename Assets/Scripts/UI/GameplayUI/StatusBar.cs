using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField, Tooltip("The prefab containing a status icon.")]
    private GameObject iconPrefab = null;
    [SerializeField, Tooltip("The amount of horizontal space between icons on the bar.")]
    private float iconXPadding;


    private Image background = null;
    private float iconWidth = 0;
    private Effectable effectable = null;
    private List<EffectIcon> icons = new();


    private void Awake()
    {
        background = GetComponent<Image>();
        iconWidth = iconPrefab.GetComponent<RectTransform>().rect.width;
    }

    public void Initialize(Effectable _effectable)
    {
        effectable = _effectable;
    }

    private void Update()
    {
        if (effectable.changedSinceLastFrame)
        {
            ClearIcons();

            float currentX = -(background.rectTransform.rect.center.x - background.rectTransform.rect.xMin + (iconWidth/2f));
            foreach (StatusInstance instance in effectable.statuses.ToArray())
            {
                GameObject iconObj = Instantiate(iconPrefab, transform);
                iconObj.transform.localPosition = new(currentX, 0);
                EffectIcon icon = iconObj.GetComponent<EffectIcon>();
                icon.Initialize(instance.GetStatusData().icon, instance.currentStacks);

                icons.Add(icon);
                currentX = currentX + iconWidth + iconXPadding;
            }
            effectable.MarkChanged();
        }
    }

    private void ClearIcons()
    {
        foreach (EffectIcon icon in icons.ToArray())
        {
            if (icon != null) Destroy(icon.gameObject);
        }
    }
}