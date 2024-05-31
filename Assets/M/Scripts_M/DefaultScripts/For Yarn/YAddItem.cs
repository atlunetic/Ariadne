using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YAddItem : MonoBehaviour
{
    public SceneItem sceneItem;

    [YarnCommand("AddItem")]
    public void AddNewItem()
    {
        if (Inventory.instance != null && sceneItem != null)
        {
            item newItem = sceneItem.GetItem();

            Inventory.instance.Additem(newItem);

            sceneItem.DestroyItem();
        }
    }
}
