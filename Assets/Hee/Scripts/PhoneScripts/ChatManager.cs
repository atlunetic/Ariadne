using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    private void Awake()
    {
        instance = this;
    }
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

    [YarnCommand("StartPhoneChat")]
    public void StartChat(int i)  // yarn에서 호출
    {
        chatting = ChattingList[i];
        PhoneController.instance.ActivePhone();
        if(chatting.IsDgram){
            // DgramController 생성 후 작성
            ChatBox_Me = DgramChatBox_Me;
            ChatBox_Opponent = DgramChatBox_Opponent;
        }
        else {
            PhoneController.instance.ActiveChocoTalk();
            ChocoTalkController.instance.ActiveChatRoom(chatting.name);
            Chatroom = ChocoTalkController.instance.getScrollrectof(chatting.name);
            ChatBox_Me = ChocoChatBox_Me;
            ChatBox_Opponent = ChocoChatBox_Opponent;
            ChatBox_Opponent.transform.GetChild(2).GetComponent<TMP_Text>().text = chatting.name;
        }
        StartCoroutine("UpdatingChat");
        GameManager.Instance.ChattingLog.Add(i);
    } 

    public IEnumerator UpdatingChat(){
        
        foreach(chat c in chatting.chatList){   
            GenerateChat(c.JisooSaying, c.text);
            yield return new WaitForSeconds(2f);
        }
        if(chatting.NextDialogue!=null){
            var runner = FindObjectOfType<DialogueRunner>();
            runner.StartDialogue(chatting.NextDialogue);
        }
    }

    public void PrintChat(int i){  // 저장 후 로드 시 호출
        chatting = ChattingList[i];
        if(chatting.IsDgram){
            // DgramController 생성 후 작성
            ChatBox_Me = DgramChatBox_Me;
            ChatBox_Opponent = DgramChatBox_Opponent;
        }
        else {
            Chatroom = ChocoTalkController.instance.getScrollrectof(chatting.name);
            ChatBox_Me = ChocoChatBox_Me;
            ChatBox_Opponent = ChocoChatBox_Opponent;
            ChatBox_Opponent.transform.GetChild(2).GetComponent<TMP_Text>().text = chatting.name;
        }
        foreach(chat c in chatting.chatList){
            GameObject ChatBox;   
            if(c.JisooSaying) ChatBox = Instantiate(ChocoChatBox_Me, Chatroom.content.transform);
            else ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
        }
    }

    public void GenerateChat(bool me,string text){
        GameObject ChatBox;
        if(me){
            ChatBox = Instantiate(ChocoChatBox_Me, Chatroom.content.transform);
        }
        else{
            ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
        }
        ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = text;

        // 말풍선의 content size fitter 동작 보장 -> 불필요, 삭제
        LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());

        // 채팅방의 content size fitter 동작 보장
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);

        Chatroom.verticalNormalizedPosition = 0f;
    }

    void AddChatToLast(bool JisooSaying, string text){
        ChattingList[ChattingList.Count-1].chatList.Add(new chat(JisooSaying, text));
    }

    void Start(){
       
        ChattingList.Add(new Chatting(false, "건우오빠", "AfterGeonwooChatAsJiwon"));  // 0
        AddChatToLast(true,"오빠");
        AddChatToLast(true, "저희 언제 마지막으로 만났죠?");
        AddChatToLast(false, "뭔 소리야? 어제 클럽에서 봤잖아.");
        AddChatToLast(false, "너 그렇게 가고나서 걱정했다.");
        AddChatToLast(false, "내가 할 수 있는게 없어서.");
        AddChatToLast(false, "그나저나 휴대폰 하는 거 보면 별 일 없나보네. 다행~");

        ChattingList.Add(new Chatting(false, "해솔", "HaesolAftertruth"));  // 1
        AddChatToLast(true, "어... 맞아.");
        AddChatToLast(true, "내 휴대폰이 꺼져서 지원이 걸로 연락했어.");
        AddChatToLast(true, "지원이가 휴대폰도 두고 갑자기 사라져서...");
        AddChatToLast(true, "혹시 어딨는지 알아?");

        ChattingList.Add(new Chatting(false, "해솔", null));  // 2
        AddChatToLast(false, "범죄일 뿐만 아니라 위험하기도 하고...");
        AddChatToLast(false, "당연히 마약류에 노출될 가능성도 높지.");
        AddChatToLast(false, "아무튼... 절대 답장하지 마.");
        AddChatToLast(true, "알겠어.");
        
        // 이하 재넘버링 필요

        ChattingList.Add(new Chatting(false, "해솔", "AfterHaesolChat"));  // 2
        AddChatToLast(true, "해솔아. 뭐해?");
        AddChatToLast(false, "나 지금 홍대 쪽이야");
        AddChatToLast(false, "근데 너 얼마전에 클럽 갔어?");
        AddChatToLast(true, "왜?");
        AddChatToLast(false, "윤아가 클럽에서 너 봤다길래");
        AddChatToLast(false, "너 요즘 클럽 엄청 자주 간다?");
        AddChatToLast(false, "그런 데 관심도 없다더니.");
        AddChatToLast(true, "아...");

        ChattingList.Add(new Chatting(false, "해솔", "HaelsolSuspicious"));  // 2
        AddChatToLast(false, "근데 너 혹시 지수야?");
        AddChatToLast(false, "말투가 지원이가 아닌데...");

        ChattingList.Add(new Chatting(false, "해솔", null));  // 2
        AddChatToLast(false, "지원이가 휴대폰을 두고 사라졌다고?");
        AddChatToLast(false, "그럴 애가 아닌데...");
        AddChatToLast(false, "나도 찾아보고 연락해줄게.");

        ChattingList.Add(new Chatting(false, "건우오빠", "AfterGeonwooChatAsHerself"));  // 2
        AddChatToLast(true, "오빠. 저 지수인데요");
        AddChatToLast(true, "지원이 어디 있는지 아세요?");
        AddChatToLast(false, "아... 글쎄?");
        AddChatToLast(false, "어제 클럽에서 마주치긴 했는데.");
        AddChatToLast(false, "근데 왜 지원이걸로 톡하냐?");
        AddChatToLast(false, "아무튼…. 바빠서 나중에 연락할게.");

        ChattingList.Add(new Chatting(false, "해솔", null));  // 2
        AddChatToLast(true, "해솔아. 나 지수인데.");
        AddChatToLast(true, "혹시 최근에 지원이 만났어?");
        AddChatToLast(false, "연락은 자주 하는데 못본지는 좀 됐지. 왜?");
        AddChatToLast(true, "휴대폰을 놓고 나갔는데. 연락할 방법이 없어서");
        AddChatToLast(false, "그래?");
        AddChatToLast(false, "어제 홍대 쪽 클럽 간다던데?");
        AddChatToLast(false, "그러고 나선 연락이 안돼서 모르겠어.");
        AddChatToLast(false, "요즘 맨날 클럽 가는 것 같던데 거기 있는 거 아냐?");

        ChattingList.Add(new Chatting(false, "해솔", "HaesolDropperCaution"));
        AddChatToLast(true, "해솔아. 혹시 드랍퍼가 뭔지 알아?");
        AddChatToLast(false, "어? 갑자기 그건 왜...?");
        AddChatToLast(true, "아... 지원이 폰에 드랍퍼 할 생각 있냐는 메시지가 와 있길래.");
        AddChatToLast(true, "너는 뭔지 아나 싶어서 물어봤어.");
        AddChatToLast(false, "뭐...?");
        AddChatToLast(false, "그 계정 빨리 차단해.");
        AddChatToLast(false, "드랍퍼 그거 마약 운반책 말하는 거야.");

        ChattingList.Add(new Chatting(false, "의사 선생님", "null"));
        AddChatToLast(true, "안녕하세요. 전에 연락 드렸던 사람입니다.");
        AddChatToLast(true, "한 번에 이만큼 처방해주시나요?");
        AddChatToLast(false, "아니요.");
        AddChatToLast(false, "그 약은 마약류 관리법에 따라 향정신성의약품으로 분류되어 있습니다.");
        AddChatToLast(false, "오남용할 경우 중독될 우려가 있어요.");
        AddChatToLast(false, "시중에서 흔히들 말하는 ’공부 잘하는 약‘이 돌아다니지 않습니까.");
        AddChatToLast(false, "그렇게 판매하는 것부터가 불법입니다.");
        AddChatToLast(false, "처벌 대상이란 말입니다.");
        AddChatToLast(true, "그럼 한 번에 이만큼은 처방하지 않는다는 말씀이시죠?");
        AddChatToLast(false, "네. 그렇습니다.");
        PrintChat(ChattingList.Count-1);
        

    }
}