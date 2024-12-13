using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ForceDialogue2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        if (runner != null) { Debug.Log("Dialogue Runner Found."); }
        runner.StartDialogue("Table_Geonwoo3");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
