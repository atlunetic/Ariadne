using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickToAdd : MonoBehaviour
{
    public SceneItem sceneItem; // Reference to the SceneItem script attached to the item GameObject
    //public Inventory inventory; // Reference to the InventoryObject

    private void OnMouseDown()
    {
        if (Menu.instance.BlockClick) { return; }

        if (Inventory.instance != null && sceneItem != null)
        {
            // Get the item from the SceneItem script
            item newItem = sceneItem.GetItem();

            // Add the item to the inventory
            Inventory.instance.Additem(newItem);

            Debug.Log(sceneItem + "added to inventory");


        }

        else
        {
            Debug.Log("cannot add");
        }
    }
}
