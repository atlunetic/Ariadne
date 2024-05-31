using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Unity;

public class CallYarn : MonoBehaviour
{
    Dictionary<string, bool> IsDialogShowed;
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
        if(runner is not null && runner.NodeExists(dialogname)){
            runner.StartDialogue(dialogname);
            GameManager.instance.FinishedDialogues.Add(dialogname);
        }
    }

    // 버튼 클릭 이벤트로 얀 다이얼로그를 한 번만 부를 때 사용
    void Callbybutton (Button button, string dialogname){
        if(GameManager.instance.FinishedDialogues.Contains(dialogname)) return;
        UnityAction myAction = null;
        myAction = () => {callYarn(dialogname); button.onClick.RemoveListener(myAction);};
        button.onClick.AddListener(myAction);
    }

    IEnumerator LateStart(){
        yield return new WaitForSeconds(2f);
        HashSet<string> FinishedDialogues = GameManager.instance.FinishedDialogues;
        Haesolchatbutton = ChocoTalkController.instance.chatbuttons["해솔"];

        Callbybutton(gallerybutton, "Gallery");  // 갤러리 켰을 때
        Callbybutton(camerabutton, "Camera");  // 카메라 켰을 때
        Callbybutton(chocotalkbutton,"Chocotalk1st");  // 초코톡 켰을 때
        Callbybutton(openchatDbutton,"OpenChat");  // 오픈채팅 켰을 때

        Callbybutton(ChocoTalkController.instance.chatbuttons["건우 오빠"], "ChooseChatName1");  // 건우 채팅창 켰을 때
        Callbybutton(Haesolchatbutton, "ChooseChatName2");  // 해솔 채팅창 켰을 때

        UnityAction drgg24 = null;  // D 채팅창 켰을 때
        drgg24 = () => {Callbybutton(IDSearchbutton, "DrugDeal");
                        openchatDbutton.onClick.RemoveListener(drgg24);};
        openchatDbutton.onClick.AddListener(drgg24);

        Callbybutton(work8282chatbutton,"DropperRecruit");  // work8282 채팅창 켰을 때
        UnityAction work8282 = null;
        work8282 = () => {ChatManager.Chatting c = ChatManager.instance.ChattingList[13];
                          c.NextDialogue = "AfterDropperChat";
                          ChatManager.instance.ChattingList[13] = c;
                          work8282chatbutton.transform.GetChild(2).gameObject.SetActive(false);
                          work8282chatbutton.onClick.RemoveListener(work8282);};
        if(FinishedDialogues.Contains("DropperRecruit")) work8282.Invoke();
        else work8282chatbutton.onClick.AddListener(work8282);

        Callbybutton(Iceicechatbutton, "IceiceChat");  // Iceice 채팅창 켰을 때
        UnityAction Iceice = null;
        Iceice = () => {Iceicechatbutton.transform.GetChild(2).gameObject.SetActive(false);
                        Iceicechatbutton.onClick.RemoveListener(Iceice);};

        Callbybutton(dgrambutton,"DGram1st");  // Dgram 켰을 때

        Callbybutton(Mapbutton,"Map_1");  // Map 켰을 때

        Callbybutton(MapController.instance.Parkbutton,"Park");  // 공원 눌렀을 때
        UnityAction park = null;
        park = () => {MapController.instance.Parkbutton.onClick.AddListener(MapController.instance.GoPark);
                      MapController.instance.Parkbutton.onClick.RemoveListener(park);};
        MapController.instance.Parkbutton.onClick.AddListener(park);

        Callbybutton(MapController.instance.BarStreetbutton,"BarStreet");  // 술집거리 눌렀을 때
        UnityAction barstreet = null;
        barstreet = () => {MapController.instance.BarStreetbutton.onClick.AddListener(MapController.instance.GoBarStreet);
                      MapController.instance.BarStreetbutton.onClick.RemoveListener(barstreet);};
        MapController.instance.BarStreetbutton.onClick.AddListener(barstreet);

        MapController.instance.Officetelbutton.onClick.AddListener(()=>callYarn("Officetel"));  // 3장 진입전 오피스텔 눌렀을 때

        MapController.instance.Hospitalbutton.onClick.AddListener(()=>callYarn("Hospital"));  // 병원 눌렀을 때: Persistent!!

    }

    public void Searchdrgg24(){
        IDSearchbutton.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DestroySearch(){
        IDSearchbutton.gameObject.SetActive(false);
        drgg24chatbutton.gameObject.SetActive(true);
    }
    
    [YarnCommand("DgramAlarm")]
    public void DgramAlarm(){
        GameObject dgramDot = dgrambutton.transform.GetChild(0).gameObject;
        dgramDot.SetActive(true);
        UnityAction deactiveDot = null;
        deactiveDot = () => {dgramDot.SetActive(false);
                             dgrambutton.onClick.RemoveListener(deactiveDot);};
        dgrambutton.onClick.AddListener(deactiveDot);
    }

    [YarnCommand("New711")]
    public void New711(){
        Callbybutton(MapController.instance.편의점711button,"SevenEleven");
        UnityAction ClubActive = null;
        ClubActive = () => {Callbybutton(MapController.instance.Clubbutton,"Club_S1End");
                            MapController.instance.편의점711button.onClick.RemoveListener(ClubActive);};
        MapController.instance.편의점711button.onClick.AddListener(ClubActive);
    }
}
