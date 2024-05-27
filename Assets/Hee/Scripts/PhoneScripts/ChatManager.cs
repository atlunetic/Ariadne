using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;
    
    public Sprite[] profileSprite;
    public Dictionary<string, Sprite> profileImage = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if(instance == null)
            instance = this;
        profileImage.Add("건우 오빠", profileSprite[0]);
        profileImage.Add("의사 선생님", profileSprite[1]);
        profileImage.Add("해솔", profileSprite[2]);
    }
    struct chat{
        public bool JisooSaying;  // 지수의 대사이면 true
        public string text;  // 대사
        public string image;  // 이미지라면 이름
        public chat (bool JisooSaying, string text){
            this.JisooSaying = JisooSaying;
            this.text = text;
            image = null;
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

    [YarnCommand("StartPhoneChat")]
    public void StartChat(int i)  // yarn에서 호출
    {
        SetChat(i);
        PhoneController.instance.ActivePhone();

        if(chatting.IsDgram){
             PhoneController.instance.ActiveDgram();
             DgramController.instance.ActiveDChatRoom(chatting.name);
        }
        else {
            PhoneController.instance.ActiveChocoTalk();
            ChocoTalkController.instance.ActiveChatRoom(chatting.name);
        }
        
        StartCoroutine("UpdatingChat");
        GameManager.instance.ChattingLog.Add(i);
    } 

    public IEnumerator UpdatingChat(){
        
        foreach(chat c in chatting.chatList){   
            GenerateChat(c);
            yield return new WaitForSeconds(2f);
        }
        if(chatting.NextDialogue!=null){
            var runner = FindObjectOfType<DialogueRunner>();
            runner.StartDialogue(chatting.NextDialogue);
        }
    }

    public void PrintChat(int i){  // 저장 후 로드 시 호출
        chatting = ChattingList[i];
        SetChat(i);
        foreach(chat c in chatting.chatList){
            if(c.image is not null){
                Instantiate(Resources.Load<GameObject>(c.image), Chatroom.content.transform);
                continue;
            }
            GameObject ChatBox;
            if(c.JisooSaying) ChatBox = Instantiate(ChatBox_Me, Chatroom.content.transform);
            else ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
        }
    }

    private void SetChat(int i){
        chatting = ChattingList[i];
        if(chatting.IsDgram){
            Chatroom = DgramController.instance.getScrollrectof(chatting.name);
            ChatBox_Me = DgramChatBox_Me;
            ChatBox_Opponent = DgramChatBox_Opponent;
            ChatBox_Opponent.transform.GetChild(1).GetComponent<TMP_Text>().text = chatting.name;
        }
        else {
            Chatroom = ChocoTalkController.instance.getScrollrectof(chatting.name);
            ChatBox_Me = ChocoChatBox_Me;
            ChatBox_Opponent = ChocoChatBox_Opponent;
            ChatBox_Opponent.transform.GetChild(1).GetComponent<Image>().sprite = profileImage[chatting.name];
            ChatBox_Opponent.transform.GetChild(2).GetComponent<TMP_Text>().text = chatting.name;
        }
    }

    private void GenerateChat(chat c){
        GameObject ChatBox;
        if(c.image is not null){
                Instantiate(Resources.Load<GameObject>(c.image), Chatroom.content.transform);
        }
        else if(c.JisooSaying){
            ChatBox = Instantiate(ChatBox_Me, Chatroom.content.transform);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
        }
        else{
            ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
            ChatBox.GetComponent<AudioSource>().Play();
        }

        // 말풍선의 content size fitter 동작 보장 -> 불필요, 삭제
        // LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());

        // 채팅방의 content size fitter 동작 보장
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);

        Chatroom.verticalNormalizedPosition = 0f;
    }

    void AddChatToLast(bool JisooSaying, string text){
        ChattingList[ChattingList.Count-1].chatList.Add(new chat(JisooSaying, text));
    }

    void AddimageToLast(bool JisooSaying, string image){
        chat c = new chat(JisooSaying, "");
        c.image = image;
        ChattingList[ChattingList.Count-1].chatList.Add(c);
    }

    void Start(){
       
        ChattingList.Add(new Chatting(false, "건우 오빠", "AfterGeonwooChatAsJiwon"));  // 0
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

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal1"));  // 3
        AddChatToLast(true, "안녕하세요");

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal2"));  // 4
        AddChatToLast(true, "오픈 카톡으로 연락 드렸던 사람인데요.");

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal4"));  // 5
        AddChatToLast(true, "네");
        AddChatToLast(false, "      == M E N U ==\n        *아 이 스*\n    완벽하고 놀라운 술\n새로운 경험하게 해드립니다\n\n    대만        멕시코\n   0.5G=35.    0.5G=40.\n\n   1.0G=55.    1.0G=60.\n\n	브 액\n	캔 디\n\n         코 카 인\n\n      전 지 역 가 능\n     비 밀 대 화 필 수\n     안 전 좌 표 발 송\n'안전'과 '신뢰'를 바탕으로\n    퀄리티만 생각합니다\n\n아이스 ice 차가운 술 얼음 대마 대마초 마리화나 위드 weed 떨 떨액 브액 허브 캔디 코카인 사탕 엑스터시 케이 케이타민 작대기 크리스탈");
        
        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal5"));  // 6
        AddChatToLast(false, "메뉴 말해주세요");
        AddChatToLast(false, "그리고 xxx-xxxxxx-xxxx로 입금하시면 됩니다");

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal7"));  // 7
        AddChatToLast(true, "x지수 이름으로 입금했어요");
        AddChatToLast(false, "확인했습니다.");
        AddimageToLast(false, "Fromdrgg24");
        AddChatToLast(false, "이 근처 공중전화에 뒀습니다. 1시간 안에 가져가세요.");

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal8"));  // 8
        AddChatToLast(false, "좋은 거 구매하시는 거예요.");
        AddChatToLast(false, "즐거운 시간 되시길 ^^ ~");
        


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

        ChattingList.Add(new Chatting(false, "건우 오빠", "AfterGeonwooChatAsHerself"));  // 2
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
        AddimageToLast(true, "ToDoctor");
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

        ChattingList.Add(new Chatting(true, "아리아드네", "Chatlist"));
        AddChatToLast(false, "지수.");
        AddChatToLast(false, "중요한 걸 잊어버리지 않았어?");

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal3"));  // 4
        AddChatToLast(false, "술 필요하신 거 맞죠?");

        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal6"));  // 4
        AddChatToLast(false, "거래 안하심?");

    }
}