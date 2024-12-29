using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PhoneController : MonoBehaviour
{
    public static PhoneController instance;
    public GameObject Phone;
    public GameObject ChocoTalk;
    public GameObject Gallery;
    public GameObject Map;
    public GameObject Dgram;
    public GameObject Memo;
    private Stack<GameObject> TabStack;

    void Awake(){
        if(instance == null){
            instance = this;
            TabStack = new Stack<GameObject>();
        }
    }
    void Start(){Menu.instance.PhoneButton.GetComponent<Button>().onClick.AddListener(ActivePhone);
                 TabStack = new Stack<GameObject>();
                }
    
    [YarnCommand("ActivePhone")]
    public void ActivePhone(){
        Home();
        Phone.SetActive(true);
        Menu.instance.UI_on();
    }

    public void ActiveTab(GameObject tab){
        if(tab.activeSelf) return;
        TabStack.Push(tab);
        tab.SetActive(true);
    }
    public void Backward(){
        if(TabStack.Count!=0)
            TabStack.Pop().SetActive(false);
    }

    public void Home(){
        while(TabStack.Count!=0){
            TabStack.Pop().SetActive(false);
        }
    }

    public void OpenSettings(){
        Settings.instance.SettingsPanel.SetActive(true);
    }
    
    public void ActiveChocoTalk(){
        ActiveTab(ChocoTalk);
    }

    public void ActiveGallery(){
        ActiveTab(Gallery);
    }
    public void ActivMap(){
        ActiveTab(Map);
    }
    public void ActivMemo(){
        ActiveTab(Memo);
    }
    public void ActiveDgram(){
        ActiveTab(Dgram);
    }

    [YarnCommand("AriadneHintOn")]
    public void AriadneHintOn(){
        AriadneHint.instance.On();
    }

    [YarnCommand("AriadneHintOff")]
    public void AriadneHintOff(){
        AriadneHint.instance.Off();
    }

}
