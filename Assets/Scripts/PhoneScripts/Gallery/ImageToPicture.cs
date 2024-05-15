using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToPicture : MonoBehaviour
{
    void Start(){
        GetComponent<Button>().onClick.AddListener(Isclicked);
    }
    public void Isclicked(){
        GalleryController.instance.ActiveThePicture(gameObject.name);
    }
}
