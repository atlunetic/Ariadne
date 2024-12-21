using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueOnStart : MonoBehaviour
{
    public string DialogueTitle;

    // Start is called before the first frame update
    void Start()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue(DialogueTitle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
