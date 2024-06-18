using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public GameObject MoveS2Button;
    public GameObject UIButtons;

    public GameObject Phone;
    public GameObject Inventory;
    public GameObject Diary;
    public GameObject MoveS2_;
    public GameObject MoveStaffroom;
    public GameObject MoveViproom;
    public GameObject MoveTable;

    public bool BlockClick = false;

    [YarnCommand("UI_on")]
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
        MoveS2_.SetActive(false);
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

    public void ActiveMoveS2(){
        if(GameManager.instance.FindedObjects.Contains("ClubTable_Geonwoo")){
            if(GameManager.instance.FindedObjects.Contains("StaffRoom_staff_C"))
                CallYarn.instance.Callbybutton(MoveStaffroom.GetComponent<Button>(), "club_staffroom_nostaff");
            MoveStaffroom.SetActive(true);
        }
        if(GameManager.instance.FindedObjects.Contains("Locker") && GameManager.instance.FindedObjects.Contains("Toilet_costomerF") && GameManager.instance.FindedClues.Contains("StudentID") && GameManager.instance.FindedClues.Contains("ToiletPaper")) {
            UnityAction openVIProom = null;
            openVIProom = () => { MoveViproom.SetActive(true);
                CallYarn.instance.Callbybutton(MoveViproom.GetComponent<Button>(), "yay");
                MoveViproom.GetComponent<Button>().onClick.AddListener(()=>{deActiveM();
                                                                            UI_off();});
            };
            if(GameManager.instance.FinishedDialogues.Contains("club_viproom_entry")) openVIProom.Invoke();
            else MoveTable.GetComponent<Button>().onClick.AddListener(openVIProom);
            CallYarn.instance.Callbybutton(MoveTable.GetComponent<Button>(), "club_viproom_entry");
        }
        
        UI_on();
        MoveS2_.SetActive(true);
    }

    [YarnCommand("ActiveM")]
    public void ActiveM(){
        MoveS2Button.SetActive(true);
        GameManager.instance.FinishedDialogues.Add("ActiveM");
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
    [YarnCommand("deActiveM")]
    public void deActiveM(){
        MoveS2Button.SetActive(false);
        GameManager.instance.FinishedDialogues.Remove("ActiveM");
    }
    [YarnCommand("IfDone_getout")]
    public void IfDone_getout(){
        if(!GameManager.instance.StaffroomEnded()) return;
        GameManager.instance.FindedObjects.Add("obj_staffroomdoor");
        StartCoroutine("fordelaystart");       
    }

    IEnumerator fordelaystart(){
        yield return new WaitForSeconds(1f);
        CallYarn.instance.callYarn("letsgetout");
    }

    [YarnCommand("RemoveUI")]
    public void RemoveUI(){
        GameManager.instance.FinishedDialogues.Add("RemoveUI");
        UIButtons.SetActive(false);
    }
}
