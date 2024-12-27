using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class TestScripts : MonoBehaviour
{
    public GameObject TestButton;
    public void TestEscapeGame()
    {
        SceneManager.LoadScene("ForTest");
        if (Menu.instance.BlockClick) { return; }
        //DialogueParent.SetActive(true);
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue("JiwonIn1403");
        TestButton.SetActive(false);
    }
}
