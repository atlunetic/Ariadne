using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class CustomCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;


   public void Awake()
    {
        dialogueRunner.AddCommandHandler<GameObject>(
            "ChangeScene", // the name of the command (in Yarn)
            ChangeSceneYnC // method to run
            );
    }

    // The method that gets called when '<<ChangeScene>>' is run
    private void ChangeSceneYnC(GameObject gameObject)
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
