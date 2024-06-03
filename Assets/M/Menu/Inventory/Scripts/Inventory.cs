using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<item> items = new List<item>();
    private item usingitem;


    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion


    public bool Additem(item _item)
    {
        
        items.Add(_item);
        //onChangeItem.Invoke();
        return true;
       
    }

    public void UseItem(string itemName)
    {
        usingitem = items.Find(i => i.itemName == itemName);
        if (usingitem != null)
        {
            Debug.Log("Using item: " + usingitem.itemName);
            usingitem.Use();
        }
        else
        {
            Debug.LogWarning("Item not found in inventory: " + itemName);
        }
    }

    public item GetUsingItem()
    {
        return usingitem;
    }

    public void ClearUsingItem()
    {
        usingitem = null;
    }
}


