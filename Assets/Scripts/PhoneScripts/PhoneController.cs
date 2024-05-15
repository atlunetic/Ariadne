using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    public static PhoneController instance;
    public GameObject Phone;
    public GameObject ChocoTalk;
    public GameObject Gallery;
    public GameObject Map;
    public GameObject Dgram;
    private Stack<GameObject> TabStack;
    public int NumOfScreenShots = 1;  
    // GameManager로 옮기기, 세이브 시 같이 저장하되 로드 시 원래 값과 비교하여 더 큰 수로 적용!!

    void Awake(){
        if(instance == null){
            instance = this;
            TabStack = new Stack<GameObject>();
        }
    }

    public void ActivePhone(){
        Phone.SetActive(true);
        // UI 숨기기
    }
    public void ActiveTab(GameObject tab){
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
