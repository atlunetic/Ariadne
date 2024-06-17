using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Unity;

public class CallYarn : MonoBehaviour
{
    public static CallYarn instance;

    void Awake(){
        if(instance == null)
            instance = this;
    }
    Dictionary<string, bool> IsDialogShowed;
    public Button gallerybutton;
    public Button camerabutton;
    public Button chocotalkbutton;
    public Button openchatDbutton;
    public Button dgrambutton;
    public Button ariadnebutton;
    public Button Haesolchatbutton;
    public Button work8282chatbutton;
    public Button Iceicechatbutton;
    public Button drgg24chatbutton;
    public Button IDSearchbutton;
    public Button Mapbutton;
    public Button jisoobutton;
    public Button officetelPhoto;

    void Start()
    {
        StartCoroutine("LateStart");
    }

    public void callYarn(string dialogname){
        var runner = FindObjectOfType<DialogueRunner>();
        if(runner is not null && runner.NodeExists(dialogname)){
            runner.StartDialogue(dialogname);
            GameManager.instance.FinishedDialogues.Add(dialogname);
        }
    }

    // 버튼 클릭 이벤트로 얀 다이얼로그를 한 번만 부를 때 사용
    public void Callbybutton (Button button, string dialogname){
        if(GameManager.instance.FinishedDialogues.Contains(dialogname)) return;
        UnityAction myAction = null;
        myAction = () => {callYarn(dialogname); button.onClick.RemoveListener(myAction);};
        button.onClick.AddListener(myAction);
    }

    IEnumerator LateStart(){
        yield return new WaitForSeconds(2f);
        HashSet<string> FinishedDialogues = GameManager.instance.FinishedDialogues;
        Haesolchatbutton = ChocoTalkController.instance.chatbuttons["해솔"];

        if(GameManager.instance.IsLoad) Menu.instance.ActivePI();
        if(GameManager.instance.FindedObjects.Contains("Books_Diary")) Menu.instance.ActiveD();
        if(FinishedDialogues.Contains("ActiveM")) Menu.instance.ActiveM();

        Callbybutton(gallerybutton, "Gallery");  // 갤러리 켰을 때

        Callbybutton(camerabutton, "Camera");  // 카메라 켰을 때
        UnityAction camera = null;
        camera = () => {camerabutton.onClick.AddListener(CameraController.instance.ActiveCamera);
                        camerabutton.onClick.RemoveListener(camera);};
        if(FinishedDialogues.Contains("Camera")) camera.Invoke();
        else camerabutton.onClick.AddListener(camera);

        Callbybutton(chocotalkbutton,"Chocotalk1st");  // 초코톡 켰을 때
        Callbybutton(openchatDbutton,"OpenChat");  // 오픈채팅 D 채팅창 켰을 때

        Callbybutton(ChocoTalkController.instance.chatbuttons["건우 오빠"], "ChooseChatName1");  // 건우 채팅창 켰을 때
        Callbybutton(Haesolchatbutton, "ChooseChatName2");  // 해솔 채팅창 켰을 때

        UnityAction jisoo = null;  // 지수 채팅창 켰을 때
        jisoo = () => {jisoobutton.transform.GetChild(2).gameObject.SetActive(false);
                       GameManager.instance.FinishedDialogues.Add("jisoo'stalk");
                       jisoobutton.onClick.RemoveListener(jisoo);};
        if(FinishedDialogues.Contains("jisoo'stalk")) jisoo.Invoke();
        else {
            jisoobutton.onClick.AddListener(jisoo);
            GameManager.instance.FinishedDialogues.Add("ChocotalkAlarm");
        }

        if(GameManager.instance.FinishedDialogues.Contains("ChocotalkAlarm")) ChocotalkAlarm();

        UnityAction drgg24 = null;  // D 채팅창 켰을 때
        drgg24 = () => {Callbybutton(IDSearchbutton, "DrugDeal");
                        openchatDbutton.onClick.RemoveListener(drgg24);};
        openchatDbutton.onClick.AddListener(drgg24);

        Callbybutton(ariadnebutton, "Chatlist");  // 아리아드네 채팅창 켰을 때
        UnityAction ari = null;
        ari = () => {ariadnebutton.transform.GetChild(2).gameObject.SetActive(false);
                        ariadnebutton.onClick.RemoveListener(ari);};
        if(FinishedDialogues.Contains("Chatlist")) ari.Invoke();
        else ariadnebutton.onClick.AddListener(ari);

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
        if(FinishedDialogues.Contains("IceiceChat")) Iceice.Invoke();
        else Iceicechatbutton.onClick.AddListener(Iceice);

        Callbybutton(dgrambutton,"DGram1st");  // Dgram 켰을 때

        Callbybutton(Mapbutton,"Map_1");  // Map 켰을 때

        MapController.instance.Parkbutton.onClick.AddListener(()=>callYarn("Park"));
        MapController.instance.BarStreetbutton.onClick.AddListener(()=>callYarn("Barstreet"));
        MapController.instance.편의점711button.onClick.AddListener(()=>callYarn("ConvenienceStore"));
        MapController.instance.Bakerybutton.onClick.AddListener(()=>callYarn("Bakery"));
        MapController.instance.Schoolbutton.onClick.AddListener(()=>callYarn("School"));
        MapController.instance.AnotherClubbutton.onClick.AddListener(()=>callYarn("AnotherClub"));

        if(FinishedDialogues.Contains("DestroySearch")) DestroySearch();

        if(FinishedDialogues.Contains("DgramAlarm")) DgramAlarm();

        if(FinishedDialogues.Contains("ChocotalkAlarm")) ChocotalkAlarm();

        if(FinishedDialogues.Contains("New711")) New711();

        if(FinishedDialogues.Contains("InteractionFin3")) AfterD3();
        if(FinishedDialogues.Contains("Officetel1stfloor")) Officetel1503();

        MapController.instance.Homebutton.onClick.AddListener(MapController.instance.GoHome);

        MapController.instance.Officetelbutton.onClick.AddListener(()=>callYarn("Officetel"));  // 3장 진입전 오피스텔 눌렀을 때

        MapController.instance.Hospitalbutton.onClick.AddListener(()=>callYarn("Hospital"));  // 병원 눌렀을 때

        if(GameManager.instance.NowScene != string.Empty) {
            if(GameManager.instance.NowScene.StartsWith("S2")) InS2();
            else if(GameManager.instance.NowScene.StartsWith("S3")) InS3();
            print(GameManager.instance.NowScene);
        }

    }

    public void InS2(){
        MapController.instance.CurrPoint.anchoredPosition = new Vector2(108,-61);
        MapController.instance.Homebutton.onClick.RemoveAllListeners();
        MapController.instance.Parkbutton.onClick.RemoveAllListeners();
        MapController.instance.Officetelbutton.onClick.RemoveAllListeners();
        MapController.instance.BarStreetbutton.onClick.RemoveAllListeners();
        MapController.instance.Hospitalbutton.onClick.RemoveAllListeners();
        MapController.instance.Bakerybutton.onClick.RemoveAllListeners();
        MapController.instance.AnotherClubbutton.onClick.RemoveAllListeners();
        MapController.instance.Schoolbutton.onClick.RemoveAllListeners();
        MapController.instance.편의점711button.onClick.RemoveAllListeners();

        UnityAction wrongplace = () => callYarn("wrongplaceS2");
        MapController.instance.Homebutton.onClick.AddListener(wrongplace);
        MapController.instance.Parkbutton.onClick.AddListener(wrongplace);
        MapController.instance.Officetelbutton.onClick.AddListener(wrongplace);
        MapController.instance.BarStreetbutton.onClick.AddListener(wrongplace);
        MapController.instance.Hospitalbutton.onClick.AddListener(wrongplace);
        MapController.instance.Bakerybutton.onClick.AddListener(wrongplace);
        MapController.instance.AnotherClubbutton.onClick.AddListener(wrongplace);
        MapController.instance.Schoolbutton.onClick.AddListener(wrongplace);
        MapController.instance.편의점711button.onClick.AddListener(wrongplace);

        MapController.instance.Clubbutton.enabled = false;
    }
    public void InS3(){
        MapController.instance.CurrPoint.anchoredPosition = new Vector2(715,204);
        MapController.instance.Homebutton.onClick.RemoveAllListeners();
        MapController.instance.Parkbutton.onClick.RemoveAllListeners();
        MapController.instance.Clubbutton.onClick.RemoveAllListeners();
        MapController.instance.BarStreetbutton.onClick.RemoveAllListeners();
        MapController.instance.Hospitalbutton.onClick.RemoveAllListeners();
        MapController.instance.Bakerybutton.onClick.RemoveAllListeners();
        MapController.instance.AnotherClubbutton.onClick.RemoveAllListeners();
        MapController.instance.Schoolbutton.onClick.RemoveAllListeners();
        MapController.instance.편의점711button.onClick.RemoveAllListeners();


        UnityAction wrongplace = () => callYarn("wrongplaceS2");
        MapController.instance.Homebutton.onClick.AddListener(wrongplace);
        MapController.instance.Parkbutton.onClick.AddListener(wrongplace);
        MapController.instance.Clubbutton.onClick.AddListener(wrongplace);
        MapController.instance.BarStreetbutton.onClick.AddListener(wrongplace);
        MapController.instance.Hospitalbutton.onClick.AddListener(wrongplace);
        MapController.instance.Bakerybutton.onClick.AddListener(wrongplace);
        MapController.instance.AnotherClubbutton.onClick.AddListener(wrongplace);
        MapController.instance.Schoolbutton.onClick.AddListener(wrongplace);
        MapController.instance.편의점711button.onClick.AddListener(wrongplace);

        MapController.instance.Officetelbutton.enabled = false;
    }

    [YarnCommand("Searchdrgg24")]
    public void Searchdrgg24(){
        IDSearchbutton.transform.GetChild(0).gameObject.SetActive(true);
    }
    
    [YarnCommand("DestroySearch")]
    public void DestroySearch(){
        GameManager.instance.FinishedDialogues.Add("DestroySearch");
        IDSearchbutton.gameObject.SetActive(false);
        drgg24chatbutton.gameObject.SetActive(true);
    }
    
    [YarnCommand("DgramAlarm")]
    public void DgramAlarm(){
        GameManager.instance.FinishedDialogues.Add("DgramAlarm");
        GameObject dgramDot = dgrambutton.transform.GetChild(0).gameObject;
        dgramDot.SetActive(true);
        UnityAction deactiveDot = null;
        deactiveDot = () => {GameManager.instance.FinishedDialogues.Remove("DgramAlarm");
                             dgramDot.SetActive(false);
                             dgrambutton.onClick.RemoveListener(deactiveDot);};
        dgrambutton.onClick.AddListener(deactiveDot);
    }

    [YarnCommand("ChocotalkAlarm")]
    public void ChocotalkAlarm(){
        GameObject ChocotalkDot = chocotalkbutton.transform.GetChild(0).gameObject;
        GameManager.instance.FinishedDialogues.Add("ChocotalkAlarm");
        ChocotalkDot.SetActive(true);
        UnityAction deactiveDot = null;
        deactiveDot = () => {GameManager.instance.FinishedDialogues.Remove("ChocotalkAlarm");
                             ChocotalkDot.SetActive(false);
                             chocotalkbutton.onClick.RemoveListener(deactiveDot);};
        chocotalkbutton.onClick.AddListener(deactiveDot);
    }

    [YarnCommand("New711")]
    public void New711(){
        GameManager.instance.FinishedDialogues.Add("New711");
        MapController.instance.편의점711button.onClick.RemoveAllListeners();
        Callbybutton(MapController.instance.편의점711button,"SevenEleven");
        UnityAction ClubActive = null;
        ClubActive = () => {Callbybutton(MapController.instance.Clubbutton,"Club_S1End");
                            MapController.instance.Clubbutton.gameObject.SetActive(true);
                            MapController.instance.편의점711button.onClick.RemoveListener(ClubActive);};
        MapController.instance.편의점711button.onClick.AddListener(ClubActive);
        if(GameManager.instance.FinishedDialogues.Contains("SevenEleven")) ClubActive.Invoke();
    }

    [YarnCommand("AfterD3")]
    public void AfterD3(){
        GameManager.instance.FinishedDialogues.Add("InteractionFin3");
        Callbybutton(ariadnebutton,"AfterD3");
    }

    [YarnCommand("Officetel1503")]
    public void Officetel1503(){
        GameManager.instance.FinishedDialogues.Add("Officetel1stfloor");
        officetelPhoto.onClick.RemoveAllListeners();
        Callbybutton(officetelPhoto,"Officetel1503");
    }
}
