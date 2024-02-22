using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardThrowMulti : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    public float hoverScaleX, hoverScaleY, originalScaleX, originalScaleY;
    public float detectionRadius;
    private void Awake()
    {
        transform.localScale = new Vector2(originalScaleX, originalScaleY);
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector2(hoverScaleX, hoverScaleY);
        isDragging = true;

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.localScale = new Vector2(originalScaleX, originalScaleY);

        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D c in results)
        {
            if (c.gameObject.name.Contains("Target") && c.gameObject != gameObject)
            {
                Debug.Log("MultiTargets: " + c.gameObject.name);
            }
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }
    }
}
