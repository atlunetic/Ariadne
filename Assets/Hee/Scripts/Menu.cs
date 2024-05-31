using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    void Awake() {

        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public GameObject UIMode;
    public GameObject PhoneButton;
    public GameObject InventoryButton;
    public GameObject DiaryButton;

    public GameObject Phone;
    public GameObject Inventory;
    public GameObject Diary;

    public void UI_on(){
        UIMode.SetActive(true);
        PhoneButton.SetActive(false);
        InventoryButton.SetActive(false);
        DiaryButton.SetActive(false);
    }
    
    public void UI_off(){
        UIMode.SetActive(false);
        PhoneButton.SetActive(true);
        InventoryButton.SetActive(true);
        DiaryButton.SetActive(true);
        Phone.SetActive(false);
        Inventory.SetActive(false);
        Diary.SetActive(false);
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