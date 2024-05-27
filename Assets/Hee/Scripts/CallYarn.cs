using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Unity;

public class CallYarn : MonoBehaviour
{
    public Button gallerybutton;
    public Button camerabutton;
    public Button chocotalkbutton;
    public Button openchatDbutton;
    public Button dgrambutton;
    public Button Haesolchatbutton;
    public Button work8282chatbutton;
    public Button Iceicechatbutton;
    public Button drgg24chatbutton;
    public Button IDSearchbutton;
    public Button Mapbutton;

    void Start()
    {
        StartCoroutine("LateStart");
    }

    private void callYarn(string dialogname){
        var runner = FindObjectOfType<DialogueRunner>();
        if(runner.NodeExists(dialogname))
            runner.StartDialogue(dialogname);
    }

    // 버튼 클릭 이벤트로 얀 다이얼로그를 한 번만 부를 때 사용
    void Callbybutton (Button button, string dialogname){
        UnityAction myAction = null;
        myAction = () => {callYarn(dialogname); button.onClick.RemoveListener(myAction);};
        button.onClick.AddListener(myAction);
    }

    IEnumerator LateStart(){
        yield return new WaitForSeconds(2f);
        Haesolchatbutton = ChocoTalkController.instance.chatbuttons["해솔"];

        Callbybutton(gallerybutton, "Gallery");
        Callbybutton(camerabutton, "Camera");
        Callbybutton(chocotalkbutton,"Chocotalk1st");
        Callbybutton(openchatDbutton,"OpenChat");
        UnityAction drgg24 = null;
        drgg24 = () => {Callbybutton(IDSearchbutton, "DrugDeal");
                        openchatDbutton.onClick.RemoveListener(drgg24);};
        openchatDbutton.onClick.AddListener(drgg24);
        Callbybutton(work8282chatbutton,"DropperRecruit");
        UnityAction work8282 = null;
        work8282 = () => {Callbybutton(Haesolchatbutton, "AfterDropperChat"); 
                          work8282chatbutton.transform.GetChild(2).gameObject.SetActive(false);
                          work8282chatbutton.onClick.RemoveListener(work8282);};
        work8282chatbutton.onClick.AddListener(work8282);
        Callbybutton(Iceicechatbutton, "IceiceChat");
        UnityAction Iceice = null;
        Iceice = () => {Iceicechatbutton.transform.GetChild(2).gameObject.SetActive(false);
                        Iceicechatbutton.onClick.RemoveListener(Iceice);};
        Callbybutton(dgrambutton,"DGram1st");
        Callbybutton(Mapbutton,"Map_1");
        Callbybutton(MapController.instance.Parkbutton,"Park");
        UnityAction park = null;
        park = () => {MapController.instance.Parkbutton.onClick.AddListener(MapController.instance.GoPark);
                      Iceicechatbutton.onClick.RemoveListener(park);};
        MapController.instance.Parkbutton.onClick.AddListener(park);
        Callbybutton(MapController.instance.BarStreetbutton,"BarStreet");
    }

    public void Searchdrgg24(){
        IDSearchbutton.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DestroySearch(){
        IDSearchbutton.gameObject.SetActive(false);
        drgg24chatbutton.gameObject.SetActive(true);
    }
}
