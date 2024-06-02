using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DgramController : MonoBehaviour
{
    public static DgramController instance;
    private Dictionary<string, GameObject> ChatMap = new Dictionary<string, GameObject>();
    private ScrollRect ChatScroll;
    [SerializeField]
    private GameObject PChatButton;
    [SerializeField]
    private GameObject PChatRoom; 
    [SerializeField]
    private RectTransform ChatRooms;

    public GameObject AriadneChatroom; 
    public GameObject work8282Chatroom; 
    public GameObject IceiceChatroom; 
    public GameObject drgg24Chatroom; 

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start(){
        ChatScroll = PhoneController.instance.Dgram.transform.GetChild(0).GetComponent<ScrollRect>();
        ChatMap.Add("아리아드네", AriadneChatroom);
        ChatMap.Add("work8282", work8282Chatroom);
        ChatMap.Add("Iceice", IceiceChatroom);
        ChatMap.Add("drgg24", drgg24Chatroom);
    }

    public void CreateDChatRoom(string name){  // 채팅방 생성
        GameObject chatroom;
        chatroom = Instantiate(PChatRoom, ChatRooms);
        // 이하 두줄 나중에 삭제!!
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatroom.GetComponent<ScrollRect>().content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatroom.GetComponent<ScrollRect>().content);
        chatroom.SetActive(false);
        chatroom.name = name;

        GameObject chatbutton;
        chatbutton = Instantiate(PChatButton, ChatScroll.content);
        chatbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = name;
        chatbutton.name = name;

        ChatMap.Add(name, chatroom);
    }
    public void ActiveDChatRoom(string name)  // 채팅방 켜기
    {
        PhoneController.instance.ActiveTab(ChatMap[name]);
    }

    public ScrollRect getScrollrectof(string name)
    {
        return ChatMap[name].GetComponent<ScrollRect>();
    }
}
