using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager{
    public string savetime;
    public bool IsLoad = false;  // 불러오기 시 true
    public string NowScene;
    public int NumOfScreenShots = 1;  // 불러오기 시 더 큰 수로 적용
    public int GeonWooScore = 0;
    public int SaengSoo = 0;
    public List<int> ChattingLog = new List<int>();
    public List<string> PhotoList = new List<string>();
    public List<item> items = new List<item>();  // 저장할때 Inventory 에서 받아오기
    public HashSet<string> FindedObjects = new HashSet<string>();
    public HashSet<string> FindedClues = new HashSet<string>();
    public HashSet<string> FinishedDialogues = new HashSet<string>();
    public HashSet<string> GottenPage = new HashSet<string>();
    public HashSet<string> RecommendedFriends = new HashSet<string>();

    public int visited;
    public bool IsAriadneHintOn = false;
    public List<string> S1HintList;

}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            DestroyImmediate(gameObject);
        }
        ChattingLog.Add(19);
        ChattingLog.Add(18);
        ChattingLog.Add(25);
    }
    // RandomN 불러오기 시 실행
    public bool IsLoad = false;  // 불러오기 시 true
    public string NowScene;
    public int NumOfScreenShots = 1;  // 불러오기 시 더 큰 수로 적용
    public int GeonWooScore = 0;
    public int SaengSoo = 0;
    public bool StaffroomOpen = false;
    public List<int> ChattingLog = new List<int>();
    public List<string> PhotoList = new List<string>();
    public List<item> items = new List<item>();  // 저장할때 Inventory 에서 받아오기
    public HashSet<string> FindedObjects = new HashSet<string>();
    public HashSet<string> FindedClues = new HashSet<string>();
    public HashSet<string> FinishedDialogues = new HashSet<string>();
    public HashSet<string> GottenPage = new HashSet<string>();
    public HashSet<string> RecommendedFriends = new HashSet<string>();


    public int visited;
    public bool IsAriadneHintOn = false;
    public List<string> S1HintList = new List<string> {"Laptop", "Meds", "Letter", "Drawer_Hairpin", "DrawerR_Wallet", "ChooseChatName1", "Books_Diary", "visitPark", "visitBarStreet", "visitHospital"};

    public bool S1Ended(){
        return FindedClues.Contains("Laptop") && FindedClues.Contains("Meds") && FindedClues.Contains("Letter") &&
               FindedObjects.Contains("Drawer_Hairpin") && FindedObjects.Contains("DrawerR_Wallet") &&
               FinishedDialogues.Contains("ChooseChatName1") &&
               FinishedDialogues.Contains("Chatlist") && FindedObjects.Contains("Books_Diary") &&
               visited == 7;
    }

    public bool StaffroomEnded(){
        return FindedClues.Contains("VIPlist") && FindedObjects.Contains("InStaffroom_VIPlist") &&
        FindedObjects.Contains("InStaffroom_cctv") && FindedObjects.Contains("InStaffroom_Key") &&
        FindedObjects.Contains("InStaffroom_Radio");
    }

    public SaveGameManager Convert(){
        SaveGameManager saveGameManager = new SaveGameManager();
        saveGameManager.IsLoad = instance.IsLoad;
        saveGameManager.NowScene = instance.NowScene;
        saveGameManager.NumOfScreenShots = instance.NumOfScreenShots;
        saveGameManager.GeonWooScore = instance.GeonWooScore;
        saveGameManager.SaengSoo = instance.SaengSoo;
        saveGameManager.ChattingLog = instance.ChattingLog;
        saveGameManager.PhotoList = instance.PhotoList;
        saveGameManager.items = instance.items;
        saveGameManager.FindedClues = instance.FindedClues;
        saveGameManager.FindedObjects = instance.FindedObjects;
        saveGameManager.FinishedDialogues = instance.FinishedDialogues;
        saveGameManager.GottenPage = instance.GottenPage;
        saveGameManager.RecommendedFriends = instance.RecommendedFriends;
        saveGameManager.visited = instance.visited;
        saveGameManager.IsAriadneHintOn = instance.IsAriadneHintOn;
        saveGameManager.S1HintList = instance.S1HintList;

        return saveGameManager;
    }

    public void reConvert(SaveGameManager saveGameManager){

        instance.IsLoad = saveGameManager.IsLoad;
        instance.NowScene = saveGameManager.NowScene;
        instance.NumOfScreenShots = saveGameManager.NumOfScreenShots;
        instance.GeonWooScore = saveGameManager.GeonWooScore;
        instance.SaengSoo = saveGameManager.SaengSoo;
        instance.ChattingLog = saveGameManager.ChattingLog;
        instance.PhotoList = saveGameManager.PhotoList;
        instance.items = saveGameManager.items;
        instance.FindedClues = saveGameManager.FindedClues;
        instance.FindedObjects = saveGameManager.FindedObjects;
        instance.FinishedDialogues = saveGameManager.FinishedDialogues;
        instance.GottenPage = saveGameManager.GottenPage;
        instance.visited = saveGameManager.visited;
        instance.RecommendedFriends = saveGameManager.RecommendedFriends;
        instance.IsAriadneHintOn = saveGameManager.IsAriadneHintOn;
        instance.S1HintList = saveGameManager.S1HintList;
    }
}
