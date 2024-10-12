using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AddClothes : MonoBehaviour
{
    public SceneItem sceneItem;

    [YarnCommand("AddToInventoryC")]
    public void AddPlease()
    {
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

    
