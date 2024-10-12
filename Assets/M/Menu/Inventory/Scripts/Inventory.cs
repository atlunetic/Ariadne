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

    private int clickCount = 0;

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
        print("inventoryLog");
        items = GameManager.instance.items;
    }

    void Start()
    {
        foreach(item i in items)
            onChangeItem.Invoke();
    }
    #endregion
    //Check Item Usage

    private void Update()
    {
        if (usingitem != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;

                if (clickCount >= 1) //clickCount >= 2
                {
                    Debug.Log("First Click Ended");
                    string nomore = usingitem.itemName;
                    ClearUsingItem();
                    Debug.Log(nomore + " is no longer being used");
                    clickCount = 0;

                }
            }
        }
    }



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
        onChangeItem.Invoke();
    }
}


