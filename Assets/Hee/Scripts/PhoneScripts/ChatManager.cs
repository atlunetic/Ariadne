using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour
{
    struct chat{
        public bool JisooSaying;  // 지수의 대사이면 true
        public string text;  // 대사
        public chat (bool JisooSaying, string text){
            this.JisooSaying = JisooSaying;
            this.text = text;
        }
    }
    struct Chatting
    {
        public bool IsDgram;  // 초코톡인지 디그램인지
        public string name;  // 누구와 채팅하는지 (채팅방 이름)
        public List<chat> chatList;  // 대사 리스트 
        public string NextDialogue;  // 다음 Yarn 다이얼로그, 없으면 null

        public Chatting(bool IsDgram, string name, string NextDialogue){
            this.IsDgram = IsDgram;
            this.name = name;
            chatList = new List<chat>();
            this.NextDialogue = NextDialogue;
        }
    }
    List<Chatting> ChattingList= new List<Chatting>();

    [SerializeField]
    private GameObject ChocoChatBox_Me;

    [SerializeField]
    private GameObject ChocoChatBox_Opponent;

    [SerializeField]
    private GameObject DgramChatBox_Me;

    [SerializeField]
    private GameObject DgramChatBox_Opponent;

    Chatting chatting;  // 지금 출력중인 채팅
    ScrollRect Chatroom;  // 지금 채팅을 출력중인 채팅방
    GameObject ChatBox_Me; 
    GameObject ChatBox_Opponent;

    public ScrollRect sr;
    public void StartChat(int i)  // yarn에서 호출
    {
        chatting = ChattingList[i];
        PhoneController.instance.ActivePhone();
        if(chatting.IsDgram){

            ChatBox_Me = DgramChatBox_Me;
            ChatBox_Opponent = DgramChatBox_Opponent;
        }
        else {
            ChocoTalkController.instance.ActiveChatRoom(chatting.name);
            Chatroom = ChocoTalkController.instance.getScrollrectof(chatting.name);
            ChatBox_Me = ChocoChatBox_Me;
            ChatBox_Opponent = ChocoChatBox_Opponent;
            ChatBox_Opponent.transform.GetChild(2).GetComponent<TMP_Text>().text = chatting.name;
        }
        StartCoroutine("UpdatingChat",i);
    } 

    public IEnumerator UpdatingChat(int i){
        
        foreach(chat c in chatting.chatList){   
            GenerateChat(c.JisooSaying, c.text);
            yield return new WaitForSeconds(0.5f);
        }
        // startdialog(chatting.NextDialogue);
    }

    public void PrintChatLog(){  // 저장 후 로드 시 호출

    }

    public void GenerateChat(bool me,string text){
        GameObject ChatBox;
        if(me){
            ChatBox = Instantiate(ChocoChatBox_Me, Chatroom.transform);
        }
        else{
            ChatBox = Instantiate(ChatBox_Opponent, Chatroom.transform);
        }
        ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = text;

        // 말풍선의 content size fitter 동작 보장 -> 불필요, 삭제
        // LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());

        // 채팅방의 content size fitter 동작 보장
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);

        Chatroom.verticalNormalizedPosition = 0f;
    }

    void Start(){
        // 입력예시
        // ChattingList.Add(new Chatting(false, "Haesol", "YarnDialogue"));  // 0
        // ChattingList[0].chatList.Add(new chat(false,"Im Log 0"));
    }
}