using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryController : MonoBehaviour, IDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    public Vector2 originalPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition; // Store the original position
    }

    public void ToggleInventory()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (!gameObject.activeSelf)
        {
            // Save the current position if the inventory is being closed
            originalPosition = rectTransform.anchoredPosition;
        }
        else
        {
            // Restore the original position if the inventory is being opened
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // Move the inventory while dragging
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
