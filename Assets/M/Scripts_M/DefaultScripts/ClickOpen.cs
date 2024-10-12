using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ClickOpen : MonoBehaviour
{

    public GameObject ObjectToOpen;
    // Start is called before the first frame update
    /*
    public void OnMouseDown()
    {

        if (Menu.instance.BlockClick) { Debug.Log("StartDialogue BlockClick"); return; }
        ObjectToOpen.SetActive(true);
    }
    */

    [YarnCommand("OpenUp")]

    public void OpenObjectByD()
    {
        ObjectToOpen.SetActive(true);
    }

}
