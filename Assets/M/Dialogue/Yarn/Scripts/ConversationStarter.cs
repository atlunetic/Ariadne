/*
Yarn Spinner is licensed to you under the terms found in the file LICENSE.md.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class ConversationStarter : MonoBehaviour
{
        [SerializeField] GameObject evoker;
        string node_name;

        // Update is called once per frame
        void Update()
        {
            var runner = FindObjectOfType<DialogueRunner>();
            if (runner != null)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    
                }
            }
        }

    private void OnMouseDown()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        if (runner != null)
        {
            if (!runner.IsDialogueRunning)
            {
                evoker.SetActive(false);
                runner.StartDialogue(node_name);
            }
        }
    }
}

