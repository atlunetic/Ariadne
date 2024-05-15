using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelButtons : MonoBehaviour
{
    public Slot currentSlot;

    //COMBINE
    public Draggable draggableObject;
    public GameObject actionPanel;
    public GameObject AlertPanel;


    //USE

    public void UseItem()
    {
        item connectedItem = currentSlot.Item;
        Debug.Log( connectedItem.itemName + " is currently being used");
        //connectedItem.Use();
        Inventory.instance.items.Remove(connectedItem);
        currentSlot.RemoveSlot();

        // After performing functionality, hide action panel
        HideActionPanel();

    }

    //COMBINE

    public void ToggleDraggable()
    {
        draggableObject.SetDraggable(true);
        Debug.Log("Draggable enabled by Combine script.");

        AlertPanel.SetActive(true);
        Invoke("CloseAlertPanel", 1.5f);

        // After performing functionality, hide action panel
        HideActionPanel();

    }

    // Method to hide the action panel
    public void HideActionPanel()
    {
        actionPanel.SetActive(false); // Hide the action panel
        Debug.Log("Action panel hidden.");
    }

    public void CloseAlertPanel()
    {
        AlertPanel.SetActive(false);
    }

}
