using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<item> items = new List<item>();

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
    }
    #endregion


    public bool Additem(item _item)
    {
        
        items.Add(_item);
        //onChangeItem.Invoke();
        onChangeItem?.Invoke();
        return true;
        
        /*
        // Iterate through the slots
        for (int i = 0; i < items.Count; i++)
        {
            // Check if the slot is empty
            if (items[i] == null)
            {
                // Add the item to the empty slot
                items[i] = _item;
                // Invoke the onChangeItem event
                onChangeItem?.Invoke();
                return true;
            }
        }
        
        // If no empty slots were found
        Debug.LogWarning("Inventory is full. Cannot add " + _item.itemName);
        return false;
        */
    }
}


