using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class StartYarnbyName : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartYarn);
    }

    public void StartYarn(){
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue(gameObject.name);
    }

}
