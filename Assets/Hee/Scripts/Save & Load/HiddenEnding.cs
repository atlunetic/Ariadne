using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class HiddenEnding : MonoBehaviour
{
    public string DialogName;
    public int[] conditions;

    void Start (){
        GetComponent<Button>().onClick.AddListener(()=>{callYarn(DialogName);});
        CheckConditions();
    }
    public void CheckConditions(){
        foreach(int i in conditions){
            if(!SaveAndLoad.instance.endings.HiddenConditions[i]){
                gameObject.SetActive(false);
                return;
            }
        }
        gameObject.SetActive(true);
    }
    public void callYarn(string dialogname){
        var runner = FindObjectOfType<DialogueRunner>();
        if(runner is not null && runner.NodeExists(dialogname)){
            runner.StartDialogue(dialogname);
            GameManager.instance.FinishedDialogues.Add(dialogname);
        }
    }
}
