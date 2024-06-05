using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Menu : MonoBehaviour  // DontDestroyOnLoad 적용
{
    public static Menu instance;
    void Awake() {

        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            DestroyImmediate(gameObject);
        }
    }

    public GameObject UIMode;
    public GameObject PhoneButton;
    public GameObject InventoryButton;
    public GameObject DiaryButton;
    public GameObject UIButtons;

    public GameObject Phone;
    public GameObject Inventory;
    public GameObject Diary;

    public bool BlockClick = false;

    public void UI_on(){
        BlockClick = true;
        UIMode.SetActive(true);
        UIButtons.SetActive(false);
    }
    
    [YarnCommand("UI_off")]
    public void UI_off(){
        BlockClick = false;
        UIMode.SetActive(false);
        UIButtons.SetActive(true);
        Phone.SetActive(false);
        Inventory.SetActive(false);
        Diary.SetActive(false);
    }


    void Start(){
        PhoneButton.GetComponent<Button>().onClick.AddListener(where);
    }
    public void where(){
        if(!GameManager.instance.S1Ended()) return;
        PhoneButton.GetComponent<Button>().onClick.RemoveListener(where);
        if(GameManager.instance.FinishedDialogues.Contains("Where")) return;
        var runner = FindObjectOfType<DialogueRunner>();
        if(runner is not null && runner.NodeExists("Where")){
            runner.StartDialogue("Where");
            GameManager.instance.FinishedDialogues.Add("Where");
        }
    }

    [YarnCommand("ActivePI")]
    public void ActivePI(){
        PhoneButton.SetActive(true);
        InventoryButton.SetActive(true);
    }
    [YarnCommand("ActiveD")]
    public void ActiveD(){
        DiaryButton.SetActive(true);
    }
}
