using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using Yarn.Unity;

public class Inventory : MonoBehaviour
{

    public List<item> items = new List<item>();
    private item usingitem;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;


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
        items = GameManager.instance.items;
    }

    void Start()
    {
        foreach(item i in items)
            onChangeItem.Invoke();
    }
    #endregion
 

    public bool Additem(item _item)
    {
        
        items.Add(_item);
        onChangeItem.Invoke();
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

    [YarnCommand("DeleteItem")]
    public void DeleteItem(string ItemName)
    {
        if (usingitem != null) { ClearUsingItem();}
        item FinishedItem = items.Find(i => i.itemName == ItemName);
        items.Remove(FinishedItem);
    }
}


