using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class StartDialogue: MonoBehaviour
{
    public string DialogueTitle;

    private void OnMouseDown()
    {

        if (Menu.instance.BlockClick) return;
        //DialogueParent.SetActive(true);
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue(DialogueTitle);

    }
}
