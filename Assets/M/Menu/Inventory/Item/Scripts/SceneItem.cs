using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneItem : MonoBehaviour
{
    public static SceneItem instance;
    public item newItem;
    public SpriteRenderer image;

    public void SetItem(item _item)
    {
        newItem.itemName = _item.itemName;
        newItem.ItemType = _item.ItemType;

        image.sprite = Resources.Load<Sprite>("ItemImage/" + _item.itemName);
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
