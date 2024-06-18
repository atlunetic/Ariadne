using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class ImageToPicture : MonoBehaviour
{
    private Button button;
    void Start(){
        button = GetComponent<Button>();
        button.onClick.AddListener(Isclicked);
        CallYarn.instance.Callbybutton(button, gameObject.name);
    }
    private void Isclicked(){
        GalleryController.instance.ActiveThePicture(gameObject.name);
    }
}
