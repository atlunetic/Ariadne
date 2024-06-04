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
    private Stack<GameObject> TabStack;

    void Awake(){
        if(instance == null){
            instance = this;
            TabStack = new Stack<GameObject>();
        }
    }
    
    [YarnCommand("ActivePhone")]
    public void ActivePhone(){
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
    
    public void ActiveChocoTalk(){
        ActiveTab(ChocoTalk);
    }

    public void ActiveGallery(){
        ActiveTab(Gallery);
    }
    public void ActivMap(){
        ActiveTab(Map);
    }
    public void ActiveDgram(){
        ActiveTab(Dgram);
    }

}
