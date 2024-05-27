using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class StartDialogueButton: MonoBehaviour
{
    public string DialogueTitle;
    //public GameObject DialogueParent;

    /*
    public void CanStartDialogue()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        DialogueParent.SetActive(true);
        runner.StartDialogue(DialogueTitle);
     
    }
    */

    private void OnMouseDown()
    {
        //DialogueParent.SetActive(true);
        GameObject dialogueCanvas = GameObject.Find("Dialogue Canvas");
        dialogueCanvas.SetActive(true);
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue(DialogueTitle);

    }
}
