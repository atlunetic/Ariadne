using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class DialogueCanvasManager : MonoBehaviour
{
    public GameObject DialogueCanvas;

    [YarnCommand("SetCanvasTo")]
    public void CloseDialogueCanvas(bool command)
    {
        DialogueCanvas.SetActive(command);
    }
}
