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
        profileImage.Add("지수", profileSprite[3]);
        profileImage.Add("마약퇴치운동본부", profileSprite[4]);
        profileImage.Add("NeverEverDrug", profileSprite[5]);
        profileImage.Add("Team ARIADNE", profileSprite[6]);
        profileImage.Add("은재", profileSprite[7]);
    }
    public struct chat{
        public bool JisooSaying;  // 지수의 대사이면 true
        public string text;  // 대사
        public string image;  // 이미지라면 이름
        public chat (bool JisooSaying, string text){
            this.JisooSaying = JisooSaying;
            this.text = text;
            image = null;
        }
    }
    public struct Chatting
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
    public List<Chatting> ChattingList = new List<Chatting>();

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
            yield return new WaitForSeconds(1.2f);
        }
        if(chatting.NextDialogue!=null){
            var runner = FindObjectOfType<DialogueRunner>();
            GameManager.instance.FinishedDialogues.Add(chatting.NextDialogue);
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
    public void PlusChat(int i){  
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
            LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);
        LayoutRebuilder.ForceRebuildLayoutImmediate(Chatroom.content);

        Chatroom.verticalNormalizedPosition = 0f;
        GameManager.instance.ChattingLog.Add(i);
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
            LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());
        }
        else{
            ChatBox = Instantiate(ChatBox_Opponent, Chatroom.content.transform);
            ChatBox.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = c.text;
            ChatBox.GetComponent<AudioSource>().Play();
            LayoutRebuilder.ForceRebuildLayoutImmediate(ChatBox.GetComponent<RectTransform>());
        }

        // 말풍선의 content size fitter 동작 보장 -> 불필요, 삭제
        

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
        Invoke("LateStart", 1f);
    }

    void LateStart(){
       
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

        ChattingList.Add(new Chatting(false, "해솔", "Haesol2"));  // 8
        AddChatToLast(true, "해솔아. 뭐해?");
        AddChatToLast(false, "나 지금 홍대 쪽이야");
        AddChatToLast(false, "근데 너 얼마전에 클럽 갔어?");

        ChattingList.Add(new Chatting(false, "해솔", "AfterHaesolChat")); // 9
        AddChatToLast(true, "왜?");
        AddChatToLast(false, "윤아가 클럽에서 너 봤다길래");
        AddChatToLast(false, "너 요즘 클럽 엄청 자주 간다?");
        AddChatToLast(false, "그런 데 관심도 없다더니.");
        AddChatToLast(true, "아...");

        ChattingList.Add(new Chatting(false, "해솔", "HaesolSuspicious"));  // 10
        AddChatToLast(false, "근데 너 혹시 지수야?");
        AddChatToLast(false, "말투가 지원이가 아닌데...");

        ChattingList.Add(new Chatting(false, "해솔", null));  // 11
        AddChatToLast(false, "지원이가 휴대폰을 두고 사라졌다고?");
        AddChatToLast(false, "그럴 애가 아닌데...");
        AddChatToLast(false, "나도 찾아보고 연락해줄게.");

        ChattingList.Add(new Chatting(false, "건우 오빠", "AfterGeonwooChatAsHerself"));  // 12
        AddChatToLast(true, "오빠. 저 지수인데요");
        AddChatToLast(true, "지원이 어디 있는지 아세요?");
        AddChatToLast(false, "아... 글쎄?");
        AddChatToLast(false, "어제 클럽에서 마주치긴 했는데.");
        AddChatToLast(false, "근데 왜 지원이걸로 톡하냐?");
        AddChatToLast(false, "아무튼…. 바빠서 나중에 연락할게.");

        ChattingList.Add(new Chatting(false, "해솔", "JustOffCanvas"));  // 13
        AddChatToLast(true, "해솔아. 나 지수인데.");
        AddChatToLast(true, "혹시 최근에 지원이 만났어?");
        AddChatToLast(false, "연락은 자주 하는데 못본지는 좀 됐지. 왜?");
        AddChatToLast(true, "휴대폰을 놓고 나갔는데. 연락할 방법이 없어서");
        AddChatToLast(false, "그래?");
        AddChatToLast(false, "어제 홍대 쪽 클럽 간다던데?");
        AddChatToLast(false, "그러고 나선 연락이 안돼서 모르겠어.");
        AddChatToLast(false, "요즘 맨날 클럽 가는 것 같던데 거기 있는 거 아냐?");

        ChattingList.Add(new Chatting(false, "해솔", "HaesolDropperCaution"));  // 14
        AddChatToLast(true, "해솔아. 혹시 드랍퍼가 뭔지 알아?");
        AddChatToLast(false, "어? 갑자기 그건 왜...?");
        AddChatToLast(true, "아... 지원이 폰에 드랍퍼 할 생각 있냐는 메시지가 와 있길래.");
        AddChatToLast(true, "너는 뭔지 아나 싶어서 물어봤어.");
        AddChatToLast(false, "뭐...?");
        AddChatToLast(false, "그 계정 빨리 차단해.");
        AddChatToLast(false, "드랍퍼 그거 마약 운반책 말하는 거야.");
        
        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal3"));  // 15
        AddChatToLast(false, "술 필요하신 거 맞죠?");
        
        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal6"));  // 16
        AddChatToLast(false, "거래 안하심?");
        
        ChattingList.Add(new Chatting(true, "drgg24", "DrugDeal8"));  // 17
        AddChatToLast(false, "좋은 거 구매하시는 거예요.");
        AddChatToLast(false, "즐거운 시간 되시길 ^^ ~");

        ChattingList.Add(new Chatting(false, "의사 선생님", "null"));  // 18
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

        ChattingList.Add(new Chatting(true, "아리아드네", "null"));  // 19
        AddChatToLast(false, "지수.");
        AddChatToLast(false, "중요한 걸 잊어버리지 않았어?");
        AddChatToLast(false, "내가 도와줄게.");

        ChattingList.Add(new Chatting(true, "아리아드네", "chocotalk_cctv"));  // 20
        AddimageToLast(true, "ToAriadne");
        AddChatToLast(true, "혹시 이 CCTV 화면... 알아볼 수 있어요?");
        AddChatToLast(true, "찾아야하는 사람이 있는데... 실루엣이 너무 비슷해서요.");
        AddChatToLast(true, "화질이 너무 안 좋아서 구분이 안 돼요.");
        AddimageToLast(false, "FromAriadne");
        AddChatToLast(false, "이 정도면 알아볼 수 있어?");

        ChattingList.Add(new Chatting(true, "아리아드네", "event_staffroom"));  // 21
        AddimageToLast(true, "ToAriadne2");  // 소리 이미지
        AddChatToLast(true, "혹시 이게 뭔지 알아요?");
        AddChatToLast(false, "잘 모르겠네...");
        AddChatToLast(false, "이건 네가 알아봐야 할 것 같아.");

        ChattingList.Add(new Chatting(false, "건우 오빠", "stairchocotalk1"));  // 22
        AddChatToLast(false, "뭐라도 알아냈어?");
        AddChatToLast(true, "오피스텔 주소를 얻었어요.");
        AddChatToLast(true, "제일 수상해 보여서... 여기를 조사해보려구요.");

        ChattingList.Add(new Chatting(false, "건우 오빠", "stairchocotalk2"));  // 23
        AddChatToLast(true, "그런데 젤리는 어떻게 했어요?");
        AddChatToLast(false, "...");
        AddChatToLast(false, "마침 나도 곰젤리가 있어서.");
        AddChatToLast(false, "마약젤리 준다고 하면서 그거 줬어");
        AddChatToLast(false, "이미 약에 취한건지...");
        AddChatToLast(false, "구분을 못하던데?");

        ChattingList.Add(new Chatting(true, "아리아드네", "chocotalk_ariadne_customerg"));  // 24
        AddChatToLast(false, "일이 잘 안풀려?");
        AddChatToLast(false, "클럽 고객G는 가족 얘기에 약하대.");

        ChattingList.Add(new Chatting(false, "지수", null));  // 25
        AddChatToLast(false, "야, 미안. 앞으로 잘할게");
        AddChatToLast(false, "배고프다~ 같이 맛있는 거 먹자.");

        ChattingList.Add(new Chatting(false, "마약퇴치운동본부", null));  // 26
        AddChatToLast(true, "24시 마약류 전화상담센터 1342");
        AddChatToLast(false, "한국마약퇴치운동본부 산하 ‘24시 마약류 전화상담센터’에서는, 당신의 일상(13) 24시간 사이(42) 모든 순간 함께하겠다”는 의미를 담은 대표 전화번호 1342를 운영합니다.\n\n마약류 중독, 치료 관련 안내와 중독심리상담, 오남용 예방 상담, 중독재활센터 연계 안내 등 다양한 상담을 제공하므로 도움이 필요하다면 언제든 전화주세요.\n\n출처: 마약퇴치운동본부");

        ChattingList.Add(new Chatting(false, "마약퇴치운동본부", null));  // 27
        AddChatToLast(true, "마약류 오남용 예방법");
        AddChatToLast(false, "<해외직구 시 유의사항>\n\n1. 해외사이트에서 홍보하는 약을 구매하려는 경우\n\n유통경로나 정확한 용법 확인이 어려운 경우 불법의약품일 수 있어 주의를 요합니다.\n\n2. 지인이 해외직구로 구입한 약을 대신 받아달라고 할 경우\n\n지인의 부탁이더라도 의도를 의심해야 하며, 절대로 남의 물건을 대신 전달하거나 받아서는 안됩니다.\n\n3. 지인이 해외 직구로 구매한 약이 마약류로 의심될 경우\n\n경찰청 112, 검찰청 1301, 관세청 125로 신고가 필요합니다. 마약류 중독이 의심될 경우 한국마약퇴치운동본부(1899-0893)의 도움을 받을 수 있습니다.\n\n");
        AddChatToLast(false, "<약물 처방 시 유의사항>\n\n4. 친구가 약을 대신 처방받아 달라고 할 경우\n\n대리처방은 불법! 처방의약품은 본인이 직접 의사와 상담을 통해 약국에서 구매합시다.\n\n5. 병원에서 처방받은 식욕억제제가 남아서 중고장터에 판매하고자 하는 경우\n\n의약품을 판매, 구매하는 모든 거래 행위는 불법입니다. 먹고 남은 약은 약국이나 보건소의 ‘폐의약품 수거함’에 버려야 합니다.\n\n6. 지인, 가족에게 식욕억제제를 나눠주고자 하는 경우\n\n병원에서 처방받은 의약품은 가족이라도 나누어 복용하면 안 됩니다.\n\n");
        AddChatToLast(false, "<누군가 음식물을 건넬 때 유의사항>\n\n7. 누군가 거리에서 친절하게 음료수를 건네주는 경우\n\n수면제나 마약류 등을 넣은 음료수, 음식일 수 있습니다. 본인이 구입하지 않은 음료수, 음식은 먹으면 안됩니다.\n\n8. 집중력을 높이는 약을 권유받을 경우\n\n마약류라면, 모르고 복용했을 시에도 처벌받기 때문에 본인이 처방 받은 약이 아니라면 복용하면 안됩니다.\n\n9. 동남아 야시장 점원이 녹색 음료를 건네며 ‘Special’하다고 권할 경우\n\n동남아 국가의 대마 합법화로 다양한 대마 제품이 시중에 판매되고 있습니다. 외국에서 모르고 대마초를 먹었을 경우도 불법으로 처벌 가능하기 때문에 녹색 단풍잎 모양, 메뉴명에 카나비스, 마리화나, 위드, 그래스, 깐차, 깐총이 등이 적혀 있다면 주문하면 안됩니다.\n\n10. 해외여행 중 식당 바닥에 떨어진 1달러 지폐를 발견할 경우\n\n해외에서 바닥에 떨어진 지폐에서 펜타닐, 필로폰 등 마약류 성분이 발견되는 일이 발생하고 있으며, 현지 경찰들은 바닥에 떨어진 돈을 줍지 말라고 당부하고 있습니다. 바닥에 떨어진 돈을 줍지 않는 것이 안전합니다.\n\n"
        + "출처: http://www.drugfree.or.kr/webzine/magazine/21/post-344.html");

        ChattingList.Add(new Chatting(false, "마약퇴치운동본부", null));  // 28
        AddChatToLast(true, "마약류를 권유받았다면?");
        AddChatToLast(false, "“Just say NO!”\n\n마약류 권유 받았을 때 최선의 대응은, 단호하게 거절 의사를 밝히는 것입니다.\n협박, 회유 등의 일이 발생할 경우 단호하게 거절 의사를 밝힌 뒤 그 자리를 뜨는 것이 좋습니다. 마약류는 처음부터 시작하지 않는 것이 가장 효과적인 예방법입니다. \n\n출처: 마약퇴치운동본부");

        ChattingList.Add(new Chatting(false, "NeverEverDrug", null));  // 29
        AddChatToLast(true, "중독관리통합지원센터");
        AddChatToLast(false, "지역사회 중심의 통합적인 중독관리 체계 구축을 통해 중독자 조기발견·상담·치료·재활 및 사회복귀를 지원하는 중독관리통합지원센터입니다.\n\n지역사회 내 알코올 및 기타 중독(마약, 인터넷 게임, 도박)에 문제가 있는 자 및 가족, 주민을 대상으로 하며, 전화상담, 센터방문 등을 통하여 서비스 이용 가능합니다. \n\n관련 전화번호, 홈페이지는 아래 링크에서 확인 가능합니다.\n\nhttps://www.mohw.go.kr/menu.es?mid=a10706040400");

        ChattingList.Add(new Chatting(false, "NeverEverDrug", null));  // 30
        AddChatToLast(true, "인천 참사랑병원");
        AddChatToLast(false, "인천 참사랑병원에서는 중독가족 프로그램, 중독재활 및 중독상담 진행 등 마약류 중독 치료를 위한 다양한 프로그램을 마련하고 있습니다.\n\n도움이 필요하다면 아래 링크를 방문해보세요.\n\nhttp://www.clh.co.kr/");

        ChattingList.Add(new Chatting(true, "아리아드네", "AfterDChat3"));  // 31
        AddChatToLast(true, "아리아드네");
        AddChatToLast(true, "혹시 XX 오피스텔 현관 비밀번호 알아?");
        AddChatToLast(false, "등잔 밑이 어두운 법이지.");
        AddChatToLast(false, "주머니는 뒤져봤어?");

        ChattingList.Add(new Chatting(false, "Team ARIADNE", null));  // 32
        AddChatToLast(true, "ARIADNE 팀 소개");
        AddChatToLast(false, "팀 아리아드네는 이화여자대학교 도전 학기제 수행을 위해 조직된 팀으로, 프로젝트 완료 이후에도 지속적인 활동을 이어가고 있습니다.\n\n아리아드네라는 팀명은 그리스 로마 신화에 등장하는 미노타우르스의 이야기에서 영감을 받았습니다, 테세우스가 미궁에서 빠져나올 수 있게 도움을 준 아리아드네처럼 마약이라는 출구 없는 미로로부터 개인뿐 아니라 우리 사회를 구출하고자 하는 목표를 달성하려는 강한 의지를 담고 싶어 정하게 된 이름입니다.");

        ChattingList.Add(new Chatting(false, "Team ARIADNE", null));  // 33
        AddChatToLast(true, "'Exit from NO EXIT' 출시 소감");
        AddChatToLast(false, "게임을 완성하기에 다소 짧은 시간이었던 반년 남짓, 일상을 제쳐두고 공들여 작업했던 ‘Exit from NO EXIT’이 드디어 출시를 앞두고 있습니다.\n\n나의 일이라 생각하지 않았으니 제삼자의 시선에서 “왜 마약을 할까, 그냥 안하면 그만 아닐까”라고 생각하기도 했었고, 제작 초기에 관련 조사를 하면서도 “시작을 하지 않으면 되는 것 아닌가?“라는 생각도 했었습니다. 어쩌면 이 게임을 플레이하고 계신 여러분도 한번쯤은 그런 생각을 해보셨을지도 모릅니다. 전부 틀린 말은 아니지만, 게임을 제작하면서 저희는 마약 문제가 의지와 노력으로 극복할 수 있는 것이 아님을, 무엇보다 사회적 차원의 관심과 논의가 절실하게 필요한 문제임을 깨달았습니다.\n\n그런 깨달음을 가지고 당장 자신의 일이 아니더라도 관심을 가졌으면 하는 마음으로, 한명이라도 더 미로에 들어가지 않았으면 하는 마음으로 게임을 제작하였습니다. 게임을 플레이하시는 분들이 마약 문제에 대해 조금이나마 생각하고 고민을 하셨다면 뿌듯할 것 같습니다.\n\n‘Exit from NO EXIT’은 아리아드네 팀만의 노력으로 만들어진 것은 아닙니다. 자문을 포함한 다양한 분야에서 게임 제작에 도움을 주신 분들께 감사드린다는 말씀을 전합니다.\n\n우리 모두가 마약이란 미로 속에서 헤매지 않길, 그에 이 게임이 도움이 되길 바랍니다.");

        ChattingList.Add(new Chatting(false, "Team ARIADNE", null));  // 34
        AddChatToLast(true, "ARIADNE 팀 연락망");
        AddChatToLast(false, "팀 아리아드네 인스타그램\n@team_ariadne.e\n\n팀 아리아드네 이메일\nariadnewithyou@gmail.com");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 35
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "노트북을 촬영해보면 어때?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 36
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "약통을 촬영해보면 어때?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 37
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "편지를 촬영해봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 38
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "책상 밑의 서랍장 두 번째 칸을 확인해 봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 39
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "약통이 올려진 서랍장을 열어 봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 40
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "건우랑은 연락 해봤어?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 41
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "책상에 일기장이라도 있지 않을까?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 42
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "바쁠수록 돌아가라. 공원에서 산책이라도 하는 건 어떨까?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 43
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "술집 거리가 요즘 떠오르는 핫플레이스라던데.");
        
        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 44
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "약은 처방 받았어?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 45
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "지원이의 지갑 안을 한번 확인해봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 46
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "지갑 안의 영수증에 적혀 있는 장소가 분명 편의점이었지.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 47
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "편의점 옆에 있는 클럽으로 가봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 48
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "음? 내가 전에 말해준 것들 중에 아직 안 한게 있는 것 같은데?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 49
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "건우가 무언가 알고 있지 않을까? 찾아서 말을 걸어보자.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 50
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "CCTV를 확인하려면 어디로 가야 할까?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 51
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "건우가 뭔가 흘리고 갔어. 촬영해보면 기억이 돌아올지도 몰라.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 52
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "화장실 휴지가 이상한 위치에 있던데... 혹시 중요한 증거는 아닐까?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 53
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "화장실에 있는 취객을 도와줘보면 어때? 착한 일을 하면 복이 온다잖아.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 54
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "VIP 고객 명단을 촬영해놓자. 혹시 나중에 증거로 쓰일지도 모르잖아?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 55
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "책상 왼쪽 구석에 있는 서류뭉치를 확인해봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 56
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "지원이가 클럽에 왔다면 CCTV에 찍혀있을텐데. 확인해봐.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 57
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "책상 밑 박스를 열어볼래?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 58
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "책상 위에 무전기가 있어. 직원들이 무슨 얘기를 하고 있는 것 같은데...");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 59
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "Staff Room엔 더 둘러볼 게 없는 것 같은데. 다른 곳으로 가볼래?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 60
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "열쇠로 잠겨있던 곳이…. 화장실 Staff Only칸에 있었던가?");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 61
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "저기 어떤 직원이 테이블 쪽으로 가는 것 같아. 한번 따라가보자.");

        ChattingList.Add(new Chatting(true, "아리아드네", null));  // 62
        AddChatToLast(true, "뭘 해야 할지 모르겠어. 도와줘");
        AddChatToLast(false, "VIP 룸에 가보는 건 어때? 뜻밖의 정보를 얻을 수 있을지도 몰라.");

        foreach (int i in GameManager.instance.ChattingLog)
            PrintChat(i);
    }
}