using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    Inventory inven;

    public Slot[] slots;
    public Transform slotHolder;

    
    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onChangeItem += RedrawSlotUI;
}

   void RedrawSlotUI()
    {
        int numItems = Mathf.Min(inven.items.Count, slots.Length);
        for (int i =0; i < numItems;i++)
        {
            slots[i].Item = inven.items[i];
            Debug.Log(slots[i].Item.itemName);
            slots[i].UpdateSlotUI();
        }
    }
}
