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
        button.onClick.AddListener(callYarn);
    }
    private void Isclicked(){
        GalleryController.instance.ActiveThePicture(gameObject.name);
    }

    private void callYarn(){
        var runner = FindObjectOfType<DialogueRunner>();
        if(runner.NodeExists(gameObject.name))
            runner.StartDialogue(gameObject.name);
        button.onClick.RemoveListener(callYarn);
    }
}
