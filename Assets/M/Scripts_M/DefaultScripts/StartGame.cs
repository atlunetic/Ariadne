using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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

    public void EndGame(){
        Application.Quit();
    }

    public void OpenLoadOnly(){

        SaveAndLoad.instance.SavePanel.SetActive(true);
        SaveAndLoad.instance.SavePanel.transform.GetChild(1).gameObject.SetActive(false);
        foreach(GameObject i in SaveAndLoad.instance.SaveFile){
            i.transform.GetChild(1).gameObject.SetActive(false);
        }

        UnityAction a = null;
        a = () => {
            foreach(GameObject i in SaveAndLoad.instance.SaveFile)
                i.transform.GetChild(1).gameObject.SetActive(true);
            SaveAndLoad.instance.SavePanel.transform.GetChild(1).gameObject.SetActive(true);
            SaveAndLoad.instance.SavePanel.GetComponent<Button>().onClick.RemoveListener(a);
        };
        SaveAndLoad.instance.SavePanel.GetComponent<Button>().onClick.AddListener(a);
        foreach(GameObject i in SaveAndLoad.instance.SaveFile){
            i.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(a);
        }
    }
}
