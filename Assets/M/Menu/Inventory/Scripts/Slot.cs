using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditorInternal.Profiling.Memory.Experimental;

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
        SceneItem WaterCup = SceneItem.Find("WaterCup");
        if (draggableItem.currentSlot.Item.itemName == "Cup" && Item.itemName == "WaterBottle") 
        {
            Inventory.instance.items.Remove(Item);
            Inventory.instance.items.Remove(draggableItem.currentSlot.Item);
            Inventory.instance.Additem(WaterCup.GetItem());


        }

        else if (draggableItem.currentSlot.Item.itemName == "WaterBottle" && Item.itemName == "Cup")
        {
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
