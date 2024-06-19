using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;
using System;
using Yarn.Unity;

public class SaveAndLoad : MonoBehaviour
{

    public static SaveAndLoad instance;

    public GameObject[] SaveFile;
    public GameObject tempCam;
    public GameObject nowLoading;
    public GameObject SavePanel;
    public GameObject CollectedEndingsPanel;
    public GameObject CollectedEndings;
    CollectedEndings endings;
    public GameObject endingImgPrefab;
    private string FolderPath;
    void Awake(){
        if(instance == null) instance=this;
    #if UNITY_EDITOR
        FolderPath = $"{Application.dataPath}/Saves/";
    #else
        FolderPath = $"{Application.persistentDataPath}/Saves/";
    #endif
    }
    void Start(){
        SavePanel.GetComponent<Button>().onClick.AddListener(()=>{SavePanel.SetActive(false);});
        for(int i=0;i<5;i++)
            AddSaveFile(i);
        LoadCollectedEndings();
    }
    public void Save(int i){
        if (Directory.Exists(FolderPath) == false){
            Directory.CreateDirectory(FolderPath);
        }

        GameManager.instance.items = Inventory.instance.items;

        SaveGameManager save = GameManager.instance.Convert();
        save.savetime = DateTime.Now.ToString(("yyyy/MM/dd HH:mm:ss tt"));
        string json = JsonConvert.SerializeObject(save, Formatting.Indented);

        string Totalpath = FolderPath+"Save"+i.ToString();
        
        File.WriteAllText(Totalpath, json);
        AddSaveFile(i);
        Debug.Log(Totalpath + " 에 저장완료");
    }

    public void Load(int i){
        SavePanel.SetActive(false);
        DestroyImmediate(GameObject.Find("MenuUI"));
        DestroyImmediate(GameObject.Find("VisualNovelPrefab"));
        string json = File.ReadAllText(FolderPath+"Save"+i.ToString());

        int num = GameManager.instance.NumOfScreenShots;

        SaveGameManager save = JsonConvert.DeserializeObject<SaveGameManager>(json);
        GameManager.instance.reConvert(save);
        GameManager.instance.IsLoad = true;
        GameManager.instance.NumOfScreenShots = num > GameManager.instance.NumOfScreenShots ? num : GameManager.instance.NumOfScreenShots;

        tempCam.SetActive(true);
        nowLoading.SetActive(true);
        SceneManager.LoadScene("Menu");
        SceneManager.LoadScene(GameManager.instance.NowScene);
        Invoke("EndLoading", 2.5f);
    }

    void AddSaveFile(int i){
        string Totalpath = FolderPath+"Save"+i.ToString();
        if(!File.Exists(Totalpath)) {
            SaveFile[i].transform.GetChild(2).GetComponent<Button>().enabled = false;
        }else{
            string json = File.ReadAllText(Totalpath);
            SaveGameManager save = JsonConvert.DeserializeObject<SaveGameManager>(json);
        
            SaveFile[i].transform.GetChild(2).GetComponent<Button>().enabled = true;
            SaveFile[i].transform.GetChild(0).GetComponent<TMP_Text>().text = Location(save.NowScene);
            SaveFile[i].transform.GetChild(3).GetComponent<TMP_Text>().text = save.savetime;
            SaveFile[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(()=>Load(i));
        }

        SaveFile[i].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>Save(i));
    }  
    void LoadCollectedEndings(){
        if (Directory.Exists(FolderPath) == false){
            Directory.CreateDirectory(FolderPath);
        }
        if(File.Exists(FolderPath+"Endings") == false){
            CollectedEndingsPanel.transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        string json = File.ReadAllText(FolderPath+"Endings");
        endings = JsonConvert.DeserializeObject<CollectedEndings>(json);
        
        for(int i=0;i<endings.endingDialogue.Count;i++){
            Sprite sprite = Resources.Load<Sprite>(endings.endingSprites[i]);
            GameObject endingImg = Instantiate(endingImgPrefab, CollectedEndings.transform);
            endingImg.GetComponent<Image>().sprite = sprite;
            endingImg.GetComponent<Button>().onClick.AddListener(()=>CallYarn.instance.callYarn(endings.endingDialogue[i]));
        }
    }

    [YarnCommand("CollectEnding")]
    public void CollectEnding(string endingDialogue, string spritename){
        endings.endingDialogue.Add(endingDialogue);
        endings.endingSprites.Add(spritename);
        string json = JsonConvert.SerializeObject(endings, Formatting.Indented);
        File.WriteAllText(FolderPath+"Endings", json);
    }

    string Location(string scene){
        switch(scene){
            case "S1_2_JiwonRoom": return "지원의 방";
            case "S1_Park": return "동네 공원";
            case "S1_Street": return "술집 거리";
            case "S1_Hospital": return "병원";
            case "S2_1_ClubCounter_Bar": return "카운터 & 바";
            case "S2_2_Table": return "테이블";
            case "S2_3_0_RestRoom": return "화장실";
            case "S2_3_1_StaffOnlyLocker": return "Staff only 칸";
            case "S2_4_0_StaffRoomEntrance": return "Staff Room 앞";
            case "S2_4_1_StaffRoom": return "Staff Room 안";
            case "S2_5_VipRoomEntrance": return "VIP Room";
            case "S2_6_CounterBar": return "이상한 카운터 & 바";
            case "S2_6_ReVip": return "이상한 VIP Room 앞";
            case "S2_7_Stairs":
            case "S2_10_Stairs": return "계단";
            case "S2_8_ParkingLot":
            case "S2_8_ParkingLot2": return "클럽 뒷문 주차장";
            case "S2_9_Car": return "조직원의 차 내부";
            case "S3_2_Officetel1stFloor": return "오피스텔 1층";
            case "S3_3_Officetel1503": return "오피스텔 1503호";
            case "S3_4_Officetel14th": return "오피스텔 14층";
            case "S3_1_1_Door":
            case "S3_1_2_PhoneBooth":
            case "S3_1_OfficetelEntrance": return "오피스텔 입구";
            case "S4": return "아리아드네";
            default: return "???";
        }
    }
    public void EndLoading(){
        tempCam.SetActive(false);
        nowLoading.SetActive(false);
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue("RandomN");
    }

    [YarnCommand("BackToMainScene")]
    public void BackToMainScene(){
        SavePanel.SetActive(false);
        DestroyImmediate(GameObject.Find("GameManager"));
        DestroyImmediate(GameObject.Find("MenuUI"));
        DestroyImmediate(GameObject.Find("VisualNovelPrefab"));
        SceneManager.LoadScene("Menu");
        SceneManager.LoadScene("S0");
    }

}

class CollectedEndings{
    public List<string> endingSprites = new List<string>();
    public List<string> endingDialogue = new List<string>();
}
