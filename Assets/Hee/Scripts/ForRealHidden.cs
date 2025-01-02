using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ForRealHidden : MonoBehaviour
{
    public GameObject Braclet;
    private void OnEnable() {
        foreach(bool b in SaveAndLoad.instance.endings.HiddenConditions){
            if(b==false) {
                Braclet.SetActive(false);
                return;
            }
        }
        Braclet.SetActive(true);
    }
    public void TrueHiddenEnding(){
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue("HiddenEnding");
    }

}
