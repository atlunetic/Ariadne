using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueandAdd : MonoBehaviour
{
    public string DialogueTitle;
    public SceneItem sceneItem;

    private void OnMouseDown()
    {

        if (Menu.instance.BlockClick) { Debug.Log("StartDialogue BlockClick"); return; }
        //DialogueParent.SetActive(true);
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue(DialogueTitle);

        if (Inventory.instance != null && sceneItem != null)
        {
            // Get the item from the SceneItem script
            item newItem = sceneItem.GetItem();

            // Add the item to the inventory
            Inventory.instance.Additem(newItem);

            Debug.Log(sceneItem + " added to inventory");


        }

        else
        {
            Debug.Log("cannot add");
        }

    }

    
}
