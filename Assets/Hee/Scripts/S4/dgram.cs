using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Unity;

public class dgram : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(IsClicked);
        if(GameManager.instance.FindedObjects.Contains(gameObject.name+"_real"))
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    private void IsClicked(){
        GameManager.instance.FindedObjects.Add(gameObject.name+"_real");
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        if(GameManager.instance.FindedObjects.Contains("realdrgg24_real") &&
            GameManager.instance.FindedObjects.Contains("eoak123_real") &&
            GameManager.instance.FindedObjects.Contains("lsdlsdlsd_real"))
            callYarn("dgram_end");
    }
    private void callYarn(string dialogname){
        if(GameManager.instance.FinishedDialogues.Contains(dialogname)) return;

        var runner = FindObjectOfType<DialogueRunner>();
        if(runner is not null && runner.NodeExists(dialogname)){
            runner.StartDialogue(dialogname);
            GameManager.instance.FinishedDialogues.Add(dialogname);
        }
    }
}
