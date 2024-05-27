using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour
{
    public item newItem;
    public SpriteRenderer image;

    public void SetItem(item _item)
    {
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


}
