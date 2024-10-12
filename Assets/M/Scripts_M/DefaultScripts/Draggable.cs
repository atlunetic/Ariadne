using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Image image;

    [HideInInspector] public Transform parentAfterDrag; // Save the original parent of the object (to set to the original parent after drag)

    // to set the drag to be only possible when trying to combine
    public bool isDraggable = false;

    public void SetDraggable(bool command)
    {
        isDraggable = command;
    }

    public Slot currentSlot;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable) {
            Debug.Log("Begin drag");
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
        else { Debug.Log("Cannot Drag, Press Combine"); }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable) {
            Debug.Log("Dragging");
            transform.position = Input.mousePosition;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Debug.Log("End drag");
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
            isDraggable = false;
            Debug.Log("isDraggable is" + isDraggable);
        }
       

    }



}

/*
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    private Camera mainCamera;
    private Canvas targetCanvas;
    private RectTransform rectTransform;
    [HideInInspector] public Transform parentAfterDrag; // Save the original parent of the object (to set to the original parent after drag)

    // to set the drag to be only possible when trying to combine
    private bool isDraggable = false;

    public Slot currentSlot;

    void Start()
    {
        mainCamera = Camera.main;
        // Find the canvas named "MenuUI" in the current scene context
        GameObject canvasObj = GameObject.Find("MenuUI");
        if (canvasObj != null)
        {
            targetCanvas = canvasObj.GetComponent<Canvas>();
            Debug.Log("Target canvas found: " + targetCanvas.name);
        }
        else
        {
            Debug.LogError("MenuUI canvas not found. Make sure there is a Canvas named 'MenuUI' in the scene.");
        }
        rectTransform = GetComponent<RectTransform>();
    }

    // Method to enable or disable dragging
    public void SetDraggable(bool draggable)
    {
        isDraggable = draggable;
        Debug.Log("Draggable set to: " + isDraggable);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            Debug.Log("Draggable is false. Dragging is not allowed.");
            return;
        }
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        Debug.Log("Parent before drag: " + parentAfterDrag.name);

        if (targetCanvas != null)
        {
            transform.SetParent(targetCanvas.transform, true); // Keep the object's position relative to the canvas during the drag
            transform.SetAsLastSibling(); // Ensure it is the topmost item
            Debug.Log("Parent during drag: " + transform.parent.name);
            image.raycastTarget = false;
        }
        else
        {
            Debug.LogError("Target canvas is null. Dragging will not be performed correctly.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            Debug.Log("Draggable is false. Dragging is not allowed.");
            return;
        }
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(targetCanvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
        {
            rectTransform.anchoredPosition = localPointerPosition;
        }
        else
        {
            Debug.LogWarning("Failed to convert screen point to local point.");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            Debug.Log("Draggable is false. Dragging is not allowed.");
            return;
        }
}  */

