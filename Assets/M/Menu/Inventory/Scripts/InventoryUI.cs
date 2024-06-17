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

        RedrawSlotUI();
    }

    void RedrawSlotUI()
    {
        /*
        int numItems = Mathf.Min(inven.items.Count, slots.Length);
        for (int i =0; i < numItems;i++)
        {
            slots[i].Item = inven.items[i];
            Debug.Log(slots[i].Item.itemName);
            slots[i].UpdateSlotUI();
        }
        */

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.items.Count)
            {
                slots[i].Item = inven.items[i];
                slots[i].UpdateSlotUI();
            }
            else
            {
                slots[i].RemoveSlot(); // Clear the slot if there are no items
            }
        }
    }
}
