using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;


//Drag start-end event detection
public class DragUI : MonoBehaviour, IDragHandler
{

    public Canvas canvas;
    private RectTransform rectTransform;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

}


    

