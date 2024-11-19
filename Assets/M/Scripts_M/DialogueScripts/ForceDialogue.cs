using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class ForceDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue("club_staffroom_front");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
