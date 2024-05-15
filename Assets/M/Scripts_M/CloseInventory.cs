using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class ClosePanelInventory : MonoBehaviour
{
    public GameObject Panel;
    public InventoryController inventoryController;

    public void Start()
    {
        inventoryController = GameObject.FindObjectOfType<InventoryController>();
    }

    public void Close()
    {
        if (Panel != null && inventoryController != null)
        {
            Panel.SetActive(false);
            Panel.GetComponent<RectTransform>().anchoredPosition = inventoryController.originalPosition;
        }

        /*
        if(Panel.transform.position != initialPosition)
        {
            Panel.transform.position = localPositionCheck.originalPosition;
            initialPosition = Panel.transform.position;
        }
        */
    }
}
