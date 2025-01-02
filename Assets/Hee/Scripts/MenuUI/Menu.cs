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
    public GameObject Controllers;
    public GameObject JisooPhone;
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
        if(GameManager.instance.NowScene=="S4") return;
        UIMode.SetActive(false);
        UIButtons.transform.GetChild(0).gameObject.SetActive(true);
        UIButtons.transform.GetChild(1).gameObject.SetActive(true);
        if(GameManager.instance.FindedObjects.Contains("Books_Diary")) 
            UIButtons.transform.GetChild(2).gameObject.SetActive(true);
        UIButtons.SetActive(true);
        Phone.SetActive(false);
        Inventory.SetActive(false);
        Diary.SetActive(false);
        MoveS2_.SetActive(false);
    }

    void Start(){
        PhoneButton.GetComponent<Button>().onClick.AddListener(where);
        MoveS2Button.GetComponent<Button>().onClick.AddListener(()=>{ActiveMoveS2();});
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
        if(BlockClick){
            UI_off();
            return;
        }
        if(GameManager.instance.FinishedDialogues.Contains("event_gunwoo_begin")){
            if(GameManager.instance.FindedObjects.Contains("StaffRoom_staff_C"))
                CallYarn.instance.Callbybutton(MoveStaffroom.GetComponent<Button>(), "club_staffroom_nostaff");
            if(!GameManager.instance.FinishedDialogues.Contains("obj_keyStory"))
                if(!GameManager.instance.FinishedDialogues.Contains("StaffroomEnd~"))
                    MoveStaffroom.SetActive(true);
        }
        if(GameManager.instance.S2Ended()) {
            UnityAction openVIProom = null;
            openVIProom = () => { 
                MoveViproom.SetActive(true);
                 MoveViproom.GetComponent<Button>().onClick.RemoveAllListeners();
                MoveViproom.GetComponent<Button>().onClick.AddListener(()=>{UI_off();CallYarn.instance.callYarn("club_viproom_entry2");});
            };
            if(GameManager.instance.FinishedDialogues.Contains("club_viproom_entry1")) openVIProom.Invoke(); //club_viproom_entry에서 변경 (10.25)
            else MoveTable.GetComponent<Button>().onClick.AddListener(openVIProom);
            CallYarn.instance.Callbybutton(MoveTable.GetComponent<Button>(), "club_viproom_entry1");
        }
        
        UI_on();
        UIButtons.transform.GetChild(0).gameObject.SetActive(false);
        UIButtons.transform.GetChild(1).gameObject.SetActive(false);
        UIButtons.transform.GetChild(2).gameObject.SetActive(false);
        UIButtons.SetActive(true);
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

    [YarnCommand("CheckMore")]
    public void CheckMore(){
        var runner = FindObjectOfType<DialogueRunner>();
        MoveS2Button.GetComponent<Button>().onClick.RemoveAllListeners();
        MoveS2Button.GetComponent<Button>().onClick.AddListener(()=>{runner.StartDialogue("CheckMore");});
        GameManager.instance.FinishedDialogues.Add("CheckMore");
    }

    [YarnCommand("CheckEnd")]
    public void CheckEnd(){
        MoveS2Button.GetComponent<Button>().onClick.RemoveAllListeners();
        MoveS2Button.GetComponent<Button>().onClick.AddListener(()=>{ActiveMoveS2();});
        GameManager.instance.FinishedDialogues.Remove("CheckMore");
    }

    [YarnCommand("deActiveM")]
    public void deActiveM(){
        MoveS2Button.SetActive(false);
        GameManager.instance.FinishedDialogues.Remove("ActiveM");
    }

    [YarnCommand("IfDone_getout")]
    public void IfDone_getout(){
        if(!GameManager.instance.StaffroomEnded()) return;
        GameManager.instance.FinishedDialogues.Add("StaffroomEnd~");
        MoveStaffroom.SetActive(false);
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

    [YarnCommand("GoReality")]
    public void GoReality(){
        SceneManager.LoadScene("S4_2_R_JisooRoom");
        GameManager.instance.ChattingLog.Clear();
        GameManager.instance.FinishedDialogues.Remove("RemoveUI");
        UIButtons.SetActive(true);
        GameManager.instance.visited=0;
        LoadReality();
    }
    [YarnCommand("LoadReality")]
    public void LoadReality(){
        DestroyImmediate(Phone);
        DestroyImmediate(Controllers);
        PhoneController.instance=null;
        ChocoTalkController.instance=null;
        DgramController.instance=null;
        MapController.instance=null;
        if(GameManager.instance.NowScene!="S4")SceneManager.LoadScene(GameManager.instance.NowScene);
        ActivePI();
        ActiveD();
        Invoke("LoadReality2", 1.5f);
    }

    private void LoadReality2(){
        GameObject temp = Instantiate(JisooPhone, transform);
        temp.transform.SetSiblingIndex(1);

        Phone = temp.transform.GetChild(0).gameObject;
        Phone.SetActive(false);
    }
}
