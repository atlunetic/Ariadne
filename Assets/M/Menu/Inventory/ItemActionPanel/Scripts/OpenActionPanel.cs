using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenActionPanel : MonoBehaviour
{

    public GameObject ActionPanel;
    public Slot CurrentSlot;


    public void Open()
    {
        if (ActionPanel != null && CurrentSlot.Item.itemName != "")
        {
            ActionPanel.SetActive(true);
        }
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the click position is outside the panel and not on any of the buttons
            if (ActionPanel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(ActionPanel.GetComponent<RectTransform>(), Input.mousePosition) && !IsPointerOverButton())
            {
                // Close the panel
                ActionPanel.SetActive(false);
            }
        }
    }

    private bool IsPointerOverButton()
    {
        // Check if the pointer is over any of the buttons inside the action panel
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("ActionButtons")) // Assuming the buttons have a tag "ActionButton"
            {
                return true;
            }
        }

        return false;
    }
}

