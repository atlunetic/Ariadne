using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AriadneHint : MonoBehaviour
{

    List<string> S1HintList;
    List<string> S2HintList;
    List<string> StaffroomHintList;

    [SerializeField]
    RectTransform viewport;
    GameObject Hint;
    Button HintButton;
    
    public static AriadneHint instance;

    void Awake(){
        if(instance == null)
            instance = this;
    }
    void Start(){
        Hint = this.gameObject;
        HintButton = Hint.GetComponent<Button>();
        HintButton.onClick.AddListener(giveHint);
        if(GameManager.instance.IsAriadneHintOn) On();
        else Off();

        S1HintList = GameManager.instance.S1HintList;
        S2HintList = GameManager.instance.S2HintList;
        StaffroomHintList = GameManager.instance.StaffroomHintList;
    }

    public void On(){
        Debug.Log("Hint On");
        GameManager.instance.IsAriadneHintOn = true;
        viewport.anchoredPosition = new Vector2(0, 47);
        viewport.sizeDelta = new Vector2(viewport.sizeDelta.x, 611);
        Hint.SetActive(true);
    }

    public void Off(){
        Debug.Log("Hint Off");
        GameManager.instance.IsAriadneHintOn = false;
        viewport.anchoredPosition = new Vector2(0, 11.8f);
        viewport.sizeDelta = new Vector2(viewport.sizeDelta.x, 682);
        Hint.SetActive(false);
    }

    private void giveHint(){
        if(GameManager.instance.NowScene.StartsWith("S1"))
        {
            if(giveHintFromList(S1HintList)) return;
            
            if(!GameManager.instance.S1Ended())
                ChatManager.instance.StartChat(48);
            else if(!GameManager.instance.FinishedDialogues.Contains("New711"))
                ChatManager.instance.StartChat(45);
            else if(!GameManager.instance.FinishedDialogues.Contains("SevenEleven"))
                ChatManager.instance.StartChat(46);
            else
                ChatManager.instance.StartChat(47);
        }
        else if(GameManager.instance.NowScene.StartsWith("S2"))
        {
            if(GameManager.instance.NowScene.StartsWith("S2_4_1_StaffRoom"))
            {
                if(giveHintFromList(StaffroomHintList)) return;
                
                if(!GameManager.instance.StaffroomEnded())
                    ChatManager.instance.StartChat(48);
                else  
                    ChatManager.instance.StartChat(59);
            }
            else
            {
                if(giveHintFromList(S2HintList)) return;

                if(!GameManager.instance.S2Ended())
                    ChatManager.instance.StartChat(48);
                else if(!GameManager.instance.FinishedDialogues.Contains("club_viproom_entry"))
                    ChatManager.instance.StartChat(61);
                else
                    ChatManager.instance.StartChat(62);
            }
        }
    }

    bool giveHintFromList(List<string> HintList){  // 리스트에 남은 힌트가 없을 시 false 반환
        check(HintList);
        if(HintList.Count > 0){
            string hint = HintList[Random.Range(0, HintList.Count)];
            ChatManager.instance.StartChat(getMapping(hint));
            HintList.Remove(hint);
            return true;
        }
        return false;
    }

    void check(List<string> HintList){  // 힌트 리스트를 게임 진행 상황과 동기화
        if(GameManager.instance.FindedObjects.Contains("ClubTable_Geonwoo")){
            if(!GameManager.instance.FindedObjects.Contains("InStaffroom_Key") && !GameManager.instance.ChattingLog.Contains(50) && !HintList.Contains("GoStaffroom"))
                HintList.Add("GoStaffroom");
            if(!GameManager.instance.FindedClues.Contains("StudentID") && !GameManager.instance.ChattingLog.Contains(51) && !HintList.Contains("StudentID"))
                HintList.Add("StudentID");
        }
        if(GameManager.instance.FindedObjects.Contains("InStaffroom_Key") && !GameManager.instance.FindedObjects.Contains("Locker") && !GameManager.instance.ChattingLog.Contains(60) && !HintList.Contains("Locker"))
            HintList.Add("Locker");
            
        for(int i = HintList.Count - 1; i >= 0; i--){
            string hint = HintList[i];
            switch(hint){
                case "Laptop":
                case "Meds":
                case "Letter":
                case "StudentID":
                case "ToiletPaper":
                case "VIPlist":
                    if(GameManager.instance.FindedClues.Contains(hint))
                        HintList.Remove(hint);
                    break;
                case "Drawer_Hairpin":
                case "DrawerR_Wallet":
                case "Books_Diary":
                case "Toilet_costomerF":
                case "InStaffroom_VIPlist":
                case "InStaffroom_cctv":
                case "InStaffroom_Key":
                case "InStaffroom_Radio":
                case "ClubTable_Geonwoo":
                case "Locker":
                    if(GameManager.instance.FindedObjects.Contains(hint))
                        HintList.Remove(hint);
                    break;
                case "ChooseChatName1":
                    if(GameManager.instance.FinishedDialogues.Contains(hint))
                        HintList.Remove(hint);
                    break;
                case "visitPark":
                    if((GameManager.instance.visited & 1) != 0)
                        HintList.Remove(hint);
                    break;
                case "visitBarStreet":
                    if((GameManager.instance.visited & 2) != 0)
                        HintList.Remove(hint);
                    break;
                case "visitHospital":
                    if((GameManager.instance.visited & 4) != 0)
                        HintList.Remove(hint);
                    break;
                case "GoStaffroom":
                    if(GameManager.instance.FindedObjects.Contains("InStaffroom_Key"))
                        HintList.Remove(hint);
                    break;
                default:
                    Debug.Log("HintList name error : "+hint);
                    break;
            }
        }
    }

    int getMapping(string hint){
        switch(hint){
            case "Laptop": return 35;
            case "Meds": return 36;
            case "Letter": return 37;
            case "Drawer_Hairpin": return 38;
            case "DrawerR_Wallet": return 39;
            case "Books_Diary": return 41;
            case "ChooseChatName1": return 40;
            case "visitPark": return 42;
            case "visitBarStreet": return 43;
            case "visitHospital": return 44;
            case "ClubTable_Geonwoo": return 49;
            case "GoStaffroom": return 50;
            case "Toilet_costomerF": return 53;
            case "StudentID": return 51;
            case "ToiletPaper": return 52;
            case "Locker": return 60;
            case "VIPlist": return 54;
            case "InStaffroom_VIPlist": return 55;
            case "InStaffroom_cctv": return 56;
            case "InStaffroom_Key": return 57;
            case "InStaffroom_Radio": return 58;
            default:
                Debug.Log("HintList name error : "+hint);
                return -1;
        }
    }
}
