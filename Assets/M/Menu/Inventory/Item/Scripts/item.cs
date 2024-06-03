using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType{
    Item,
    Proof,
    Etc
}

[System.Serializable]
public class item {

    public itemType ItemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        Debug.Log(itemName + " used.");
        return true;
    }
}
  
