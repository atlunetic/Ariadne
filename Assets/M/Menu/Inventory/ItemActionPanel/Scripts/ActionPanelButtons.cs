using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.SceneManagement;

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
        var runner = FindObjectOfType<DialogueRunner>();
        Scene currentscene = SceneManager.GetSceneAt(0);//Scene With story checked
        item connectedItem = currentSlot.Item;
        Inventory.instance.UseItem(connectedItem.itemName);
        Debug.Log(connectedItem.itemName + " is currently being used");


        if(connectedItem.itemName == "WaterCup" && currentscene.name == "S2_2_Table")
        {
            runner.StartDialogue("UsedWaterCup");
        }

        if(connectedItem.itemName == "Clothes")
        {
            runner.StartDialogue("ClothesUsed");
        }

        if(currentscene.name == "S2_7_Stairs")
        {
            if (connectedItem.itemName == "DrugJelly")
            {
                runner.StartDialogue("inventory_drugjelly");
            }

            else
            {
                runner.StartDialogue("inventory_notjelly");
            }
        }

        if(connectedItem.itemName == "DrugJelly")

        if(connectedItem.itemName == "Wallet" && GameManager.instance.S1Ended() == false)
        {
            runner.StartDialogue("WalletX");
        }
        if(connectedItem.itemName == "Wallet" && GameManager.instance.S1Ended() == true)
        {
            runner.StartDialogue("WalletO");
            Inventory.instance.items.Remove(connectedItem);
            currentSlot.RemoveSlot();
        }
        else
        {

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
