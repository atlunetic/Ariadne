using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnMFunctions : MonoBehaviour
{
    [YarnCommand("ChangeSceneTo")]
    public void ChangeSceneTo(string sceneName1, string sceneName2 = "")
    {
        // Load the first scene normally
        SceneManager.LoadScene(sceneName1);
        Debug.Log(sceneName1 + "loaded by Yarn");

        // If a second scene name is provided, load it additively
        if (!string.IsNullOrEmpty(sceneName2))
        {
            SceneManager.LoadScene(sceneName2, LoadSceneMode.Additive);
        }
    }

    [YarnCommand("SetActiveFalse")]
    public void SetActiveObject(string ObjectName)
    {
        GameObject Object = GameObject.Find(ObjectName);
        if(Object is not null) Object.SetActive(false);
        Debug.Log(Object + "Set Active False by Yarn");
    }


    public GameObject DialogueCanvas;
    

    [YarnCommand("SetCanvasTo")]
    public void SetDialogueCanvas(bool command)
    {
        DialogueCanvas.SetActive(command);
        if (command == true) { Menu.instance.BlockClick = true; }
        else {  Menu.instance.BlockClick = false; }
        
       
    }

    [YarnCommand("AddToInventory")]
    public void AddItemForY(string ItemName)
    {

        SceneItem ItemToAdd = SceneItem.Find(ItemName);
        if (ItemToAdd != null)
        {
            Debug.Log(ItemToAdd.newItem.itemName + " found.");
            item newItem = ItemToAdd.GetItem();
            Inventory.instance.Additem(newItem);
        }
        else
        {
            Debug.LogWarning("Item not found: " + ItemName);
        }
    }

    [YarnCommand("AddToInventory2")]
    public void AddItemForY2(string ItemName)
    {

        SceneItem ItemToAdd = SceneItem.Find(ItemName);
        if (ItemToAdd != null)
        {
            Debug.Log(ItemToAdd.newItem.itemName + " found.");
            item newItem = ItemToAdd.GetItem();
            Inventory.instance.Additem(newItem);
        }
        else
        {
            Debug.LogWarning("Item not found: " + ItemName);
        }
    }

    [YarnCommand("FinishedObj")]
    public void FinishedObjects(string ObjectName)
    {
        GameManager.instance.FindedObjects.Add(ObjectName);
    }

    [YarnCommand("FinishedDialogue")]
    public void FinishedDialogue(string DialogueTitle)
    {
        GameManager.instance.FinishedDialogues.Add(DialogueTitle);
    }


    [YarnCommand("PersuadeScore")]
    public void PersuadeScore()
    {
        InMemoryVariableStorage variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();

        float score;
        variableStorage.TryGetValue("$persuade", out score);
        GameManager.instance.GeonWooScore = (int)score;
    }

    [YarnCommand("getWaterbottle")]
    public void getWaterbottle(){
        InMemoryVariableStorage variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        variableStorage.SetValue("$WaterBottle", GameManager.instance.SaengSoo);
    }
    [YarnCommand("setWaterbottle")]
    public void setWaterbottle(){
        InMemoryVariableStorage variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();

        float bottles;
        variableStorage.TryGetValue("$WaterBottle", out bottles);
        GameManager.instance.SaengSoo = (int)bottles;
    }
}
