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
            //SceneManager.LoadScene(sceneName2, LoadSceneMode.Additive);
        }
    }

    [YarnCommand("SetActive")]
    public void SetActiveObject(string ObjectName)
    {
        GameObject ObjectToOpen = GameObject.Find(ObjectName);
        ObjectToOpen.SetActive(true);
        Debug.Log(ObjectToOpen + "SetActive by Yarn");
    }


    public GameObject DialogueCanvas;

    [YarnCommand("SetCanvasTo")]
    public void SetDialogueCanvas(bool command)
    {
        DialogueCanvas.SetActive(command);
    }


    public SceneItem sceneItem; // Reference to the SceneItem script attached to the item GameObject

    [YarnCommand("AddToInventory")]
    public void AddItemForY()
    {
        if (Inventory.instance != null && sceneItem != null)
        {
            // Get the item from the SceneItem script
            item newItem = sceneItem.GetItem();

            // Add the item to the inventory
            Inventory.instance.Additem(newItem);



            //GameManager.instance.FindedObjects.Add(obj.name);
        }
    }




}
