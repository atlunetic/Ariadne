using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class ChocoTalkController : MonoBehaviour
{
    public static ChocoTalkController instance;
    private Dictionary<string, GameObject> ChatMap = new Dictionary<string, GameObject>();
    public Dictionary<string, Button> chatbuttons = new Dictionary<string, Button>();

    [SerializeField]
    private GameObject PChatButton;
    [SerializeField]
    private GameObject PChatRoom;
    [SerializeField]
    private RectTransform ChatRooms;

    public GameObject friendsTab;
    public GameObject ChatTab;
    private ScrollRect ChatScroll;
    public GameObject OPChatTab;
    public GameObject TrendsTab;
    public Button[] Buttons;
    public TMP_Text TXT;
    public GameObject D;
    public GameObject jisoo;
    public GameObject trend1;
    public GameObject trend2;
    public GameObject[] RecommendedFriends;
    public GameObject ClubCustomerE;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start(){
        ChatScroll = ChatTab.transform.GetChild(0).GetComponent<ScrollRect>();
        CreateChatRoom("건우 오빠");
        CreateChatRoom("의사 선생님");
        CreateChatRoom("해솔");
        ChatMap.Add("지수", jisoo);
        ChatMap.Add("D", D);
        ChatMap.Add("마약퇴치운동본부", RecommendedFriends[0]);
        ChatMap.Add("NeverEverDrug", RecommendedFriends[1]);
        ChatMap.Add("Team ARIADNE", RecommendedFriends[2]);
        CreateChatRoom("은재");
        if(!GameManager.instance.FinishedDialogues.Contains("letEknow")){
            chatbuttons["은재"].gameObject.SetActive(false);
            ClubCustomerE.SetActive(false);
        }
    }

    public void CreateChatRoom(string name){  // 채팅방 생성
        GameObject chatroom;
        chatroom = Instantiate(PChatRoom, ChatRooms);
        chatroom.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        chatroom.SetActive(false);
        chatroom.name = name;

        GameObject chatbutton;
        chatbutton = Instantiate(PChatButton, ChatScroll.content);
        chatbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        chatbutton.transform.GetChild(1).GetComponent<Image>().sprite = ChatManager.instance.profileImage[name];
        chatbutton.name = name;

        chatbuttons.Add(name, chatbutton.GetComponent<Button>());
        ChatMap.Add(name, chatroom);
    }
    public void ActiveChatRoom(string name)  // 채팅방 켜기
    {
        GameObject chatroom = ChatMap[name];
        PhoneController.instance.ActiveTab(chatroom);

        LayoutRebuilder.ForceRebuildLayoutImmediate(chatroom.GetComponent<ScrollRect>().content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatroom.GetComponent<ScrollRect>().content);
    }

    public ScrollRect getScrollrectof(string name)
    {
        return ChatMap[name].GetComponent<ScrollRect>();
    }

    public void OpenFriendsTab(){
        disablebutton(0);
        TXT.text = "친구";
        ChatTab.SetActive(false);
        OPChatTab.SetActive(false);
        friendsTab.SetActive(true);
        TrendsTab.SetActive(false);
    }
    public void OpenChatTab(){
        disablebutton(1);
        TXT.text = "채팅";
        ChatTab.SetActive(true);
        OPChatTab.SetActive(false);
        friendsTab.SetActive(false);
        TrendsTab.SetActive(false);
    }
    public void OpenOPChatTab(){
        disablebutton(2);
        TXT.text = "오픈채팅";
        ChatTab.SetActive(false);
        OPChatTab.SetActive(true);
        friendsTab.SetActive(false);
        TrendsTab.SetActive(false);
    }
    public void OpenTrendsTab(){
        disablebutton(3);
        TXT.text = "트렌드";
        ChatTab.SetActive(false);
        OPChatTab.SetActive(false);
        friendsTab.SetActive(false);
        TrendsTab.SetActive(true);
    }

    void disablebutton(int x){
        Buttons[x].interactable = false;
        for(int i = 0; i < Buttons.Length; i++){
            if(i != x) Buttons[i].interactable = true;
        } 
    }

    public void ativeTrend1(){
        PhoneController.instance.ActiveTab(trend1);
    }
    public void ativeTrend2(){
        PhoneController.instance.ActiveTab(trend2);
    }

    [YarnCommand("OpenHiddenStory")]
    public void OpenHiddenStory(){
        chatbuttons["은재"].gameObject.SetActive(true);
        ClubCustomerE.SetActive(true);
    }
}
