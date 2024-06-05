using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour
{
    public item newItem;
    public SpriteRenderer image;

    public void SetItem(item _item)
    {
        _item.itemImage = Resources.Load<Sprite>("ItemImage/" + _item.itemName + "_0");
        newItem.itemName = _item.itemName;
        newItem.itemImage = _item.itemImage;
        newItem.ItemType = _item.ItemType;

        image.sprite = _item.itemImage;
    }

    public item GetItem()
    {
        return newItem;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    private static List<SceneItem> sceneItems = new List<SceneItem>();

    private void Awake()
    {
        // Ensure this SceneItem is added to the static list when instantiated
        sceneItems.Add(this);
    }

    public static SceneItem Find(string ItemName)
    {
        foreach (SceneItem item in sceneItems)
        {
            if (item.newItem.itemName == ItemName)
            {
                return item;
            }
        }
        return null;
    }


}
