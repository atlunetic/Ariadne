using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    private Camera mainCamera;
    [HideInInspector] public Transform parentAfterDrag; // Save the original parent of the object (to set to the original parent after drag)
    
    // to set the drag to be only possible when trying to combine
    private bool isDraggable = false;

    public Slot currentSlot;

    void Start()
    {
        mainCamera = Camera.main;

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
        transform.SetParent(transform.root); //Find the canvas and keep it as the parent during the drag
        transform.SetAsLastSibling(); // to see it at the top of the view
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!isDraggable) {
            Debug.Log("Draggable is false. Dragging is not allowed.");
            return;
        }
        Debug.Log("Dragging");
        //transform.position = Input.mousePosition;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
        Debug.Log("CheckCombine Initiated");
        //CheckCombine();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            Debug.Log("Draggable is false. Dragging is not allowed.");
            return;
        }
        Debug.Log("End drag");
        transform.SetParent(parentAfterDrag); //set the parent back
        SetDraggable(false);
        image.raycastTarget = true;
    }

}
