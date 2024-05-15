using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public item Item;
    public Image itemIcon;
    public SceneItem WaterBottleSceneItemObj;
    public Inventory inventory;
    public GameObject WrongCombineAlertPanel;


    public void UpdateSlotUI()
    {
        itemIcon.sprite = Item.itemImage;
        itemIcon.gameObject.SetActive(true);
        GetComponent<Button>().interactable = true;
    }

    public void RemoveSlot()
    {
        Item = null;
        itemIcon.gameObject.SetActive(false);
        GetComponent<Button>().interactable = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Draggable draggableItem = dropped.GetComponent<Draggable>();
        if (draggableItem.currentSlot.Item.itemName == "Meds" && Item.itemName == "Diary") 
        {
            Inventory.instance.items.Remove(Item);
            Inventory.instance.items.Remove(draggableItem.currentSlot.Item);
            inventory.Additem(WaterBottleSceneItemObj.GetItem());

        }

        else if (draggableItem.currentSlot.Item.itemName == "Diary" && Item.itemName == "Meds")
        {
            Inventory.instance.items.Remove(Item);
            Inventory.instance.items.Remove(draggableItem.currentSlot.Item);
            inventory.Additem(WaterBottleSceneItemObj.GetItem());
        }

        else
        {
            WrongCombineAlertPanel.SetActive(true);
            Invoke("CloseWrongAlertPanel", 1.5f);
        }
    }

    public void CloseWrongAlertPanel()
    {
        WrongCombineAlertPanel.SetActive(false);
    }

}
