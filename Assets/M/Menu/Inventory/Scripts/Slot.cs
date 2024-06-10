using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour ,IDropHandler
{
    public item Item;
    public GameObject itemIcon;
    public GameObject WrongCombineAlertPanel;


    public void UpdateSlotUI()
    {
        itemIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemImage/" + Item.itemName);
        itemIcon.SetActive(true);
        GetComponent<Button>().interactable = true;
    }

    public void RemoveSlot()
    {
        Item = null;
        itemIcon.SetActive(false);
        GetComponent<Button>().interactable = false;
    }

    public void OnDrop(PointerEventData eventData)
    {

            GameObject dropped = eventData.pointerDrag;
            Draggable draggableItem = dropped.GetComponent<Draggable>();
        //draggableItem.parentAfterDrag = transform;


        if (draggableItem.currentSlot.Item.itemName == "Cup" && Item.itemName == "WaterBottle")
            {
                SceneItem WaterCup = SceneItem.Find("WaterCup");
                Inventory.instance.items.Remove(Item);
                Inventory.instance.items.Remove(draggableItem.currentSlot.Item);
                Inventory.instance.Additem(WaterCup.GetItem());


            }

            else if (draggableItem.currentSlot.Item.itemName == "WaterBottle" && Item.itemName == "Cup")
            {
                SceneItem WaterCup = SceneItem.Find("WaterCup");
                Inventory.instance.items.Remove(Item);
                Inventory.instance.items.Remove(draggableItem.currentSlot.Item);
                Inventory.instance.Additem(WaterCup.GetItem());
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
