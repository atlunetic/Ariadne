using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class StartGame : MonoBehaviour
{
    public void StartScene()
    {
          var runner = FindObjectOfType<DialogueRunner>();
          runner.StartDialogue("S1_Nightmare");
          SceneManager.LoadScene("S1");
    }
}
