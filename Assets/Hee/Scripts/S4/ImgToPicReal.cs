using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Unity;
public class ImgToPicReal : MonoBehaviour
{
    public GameObject Picture;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Isclicked);
    }

    private void Isclicked(){
        PhoneController.instance.ActiveTab(Picture);
        callYarn(gameObject.name);
    }

    private void callYarn(string dialogname){
        if(GameManager.instance.FinishedDialogues.Contains(dialogname)) return;

        var runner = FindObjectOfType<DialogueRunner>();
        if(runner is not null && runner.NodeExists(dialogname)){
            GameManager.instance.FinishedDialogues.Add(dialogname);
            runner.StartDialogue(dialogname);
        }
    }

}
