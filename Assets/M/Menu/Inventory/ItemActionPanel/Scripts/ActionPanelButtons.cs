using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

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



        if(connectedItem.itemName == "Wallet" && GameManager.instance.S1Ended == false)
        {
            GameObject dialogueCanvas = GameObject.Find("Dialogue Canvas");
            dialogueCanvas.SetActive(true);
            var runner = FindObjectOfType<DialogueRunner>();
            runner.StartDialogue("WalletX");
        }

        if(connectedItem.itemName == "Wallet" && GameManager.instance.S1Ended == true)
        {
            GameObject dialogueCanvas = GameObject.Find("Dialogue Canvas");
            dialogueCanvas.SetActive(true);
            var runner = FindObjectOfType<DialogueRunner>();
            runner.StartDialogue("WalletO");
            Inventory.instance.items.Remove(connectedItem);
            currentSlot.RemoveSlot();
        }

        else
        {
            GameObject dialogueCanvas = GameObject.Find("Dialogue Canvas");
            dialogueCanvas.SetActive(true);
            var runner = FindObjectOfType<DialogueRunner>();
            runner.StartDialogue("WrongItem");
        }






        //connectedItem.Use();
        //Inventory.instance.items.Remove(connectedItem);
        //currentSlot.RemoveSlot();

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