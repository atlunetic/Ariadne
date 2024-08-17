using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AriadneHint : MonoBehaviour
{

    List<string> S1HintList;

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
        if(GameManager.instance.NowScene.StartsWith("S1")){
            check(S1HintList);
            if(S1HintList.Count > 0){
                string hint = S1HintList[Random.Range(0, S1HintList.Count)];
                ChatManager.instance.StartChat(getMapping(hint));
                S1HintList.Remove(hint);
            }
            else{
                if(!GameManager.instance.S1Ended())
                    ChatManager.instance.StartChat(48);
                else if(!GameManager.instance.FinishedDialogues.Contains("New711"))
                    ChatManager.instance.StartChat(45);
                else if(!GameManager.instance.FinishedDialogues.Contains("SevenEleven"))
                    ChatManager.instance.StartChat(46);
                else
                    ChatManager.instance.StartChat(47);
            }
        }
    }

    void check(List<string> HintList){
        for(int i = HintList.Count - 1; i >= 0; i--){
            string hint = HintList[i];
            switch(hint){
                case "Laptop":
                case "Meds":
                case "Letter":
                    if(GameManager.instance.FindedClues.Contains(hint))
                        S1HintList.Remove(hint);
                    break;
                case "Drawer_Hairpin":
                case "DrawerR_Wallet":
                case "Books_Diary":
                    if(GameManager.instance.FindedObjects.Contains(hint))
                        S1HintList.Remove(hint);
                    break;
                case "ChooseChatName1":
                    if(GameManager.instance.FinishedDialogues.Contains(hint))
                        S1HintList.Remove(hint);
                    break;
                case "visitPark":
                    if((GameManager.instance.visited & 1) != 0)
                        S1HintList.Remove(hint);
                    break;
                case "visitBarStreet":
                    if((GameManager.instance.visited & 2) != 0)
                        S1HintList.Remove(hint);
                    break;
                case "visitHospital":
                    if((GameManager.instance.visited & 4) != 0)
                        S1HintList.Remove(hint);
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
            default:
                Debug.Log("HintList name error : "+hint);
                return -1;
        }
    }
}
