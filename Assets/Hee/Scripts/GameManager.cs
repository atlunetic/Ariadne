using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager{
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

    public int visited;

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

    public int visited;

    public bool S1Ended(){
        return FindedClues.Contains("Laptop") && FindedClues.Contains("Meds") && FindedClues.Contains("Letter") &&
               FindedObjects.Contains("Drawer_Hairpin") && FindedObjects.Contains("DrawerR_Wallet") &&
               (FinishedDialogues.Contains("AfterGeonwooChatAsJiwon") || FinishedDialogues.Contains("AfterGeonwooChatAsHerself")) &&
               FinishedDialogues.Contains("Chatlist") &&
               visited == 7;
    }

    public SaveGameManager Convert(){
        SaveGameManager saveGameManager = new SaveGameManager();
        saveGameManager.IsLoad = instance.IsLoad;
        saveGameManager.NowScene = instance.NowScene;
        saveGameManager.NumOfScreenShots = instance.NumOfScreenShots;
        saveGameManager.GeonWooScore = instance.GeonWooScore;
        saveGameManager.SaengSoo = instance.SaengSoo;
        saveGameManager.StaffroomOpen = instance.StaffroomOpen;
        saveGameManager.ChattingLog = instance.ChattingLog;
        saveGameManager.PhotoList = instance.PhotoList;
        saveGameManager.items = instance.items;
        saveGameManager.FindedClues = instance.FindedClues;
        saveGameManager.FindedObjects = instance.FindedObjects;
        saveGameManager.FinishedDialogues = instance.FinishedDialogues;
        saveGameManager.GottenPage = instance.GottenPage;
        saveGameManager.visited = instance.visited;

        return saveGameManager;
    }

    public void reConvert(SaveGameManager saveGameManager){

        instance.IsLoad = saveGameManager.IsLoad;
        instance.NowScene = saveGameManager.NowScene;
        instance.NumOfScreenShots = saveGameManager.NumOfScreenShots;
        instance.GeonWooScore = saveGameManager.GeonWooScore;
        instance.SaengSoo = saveGameManager.SaengSoo;
        instance.StaffroomOpen = saveGameManager.StaffroomOpen;
        instance.ChattingLog = saveGameManager.ChattingLog;
        instance.PhotoList = saveGameManager.PhotoList;
        instance.items = saveGameManager.items;
        instance.FindedClues = saveGameManager.FindedClues;
        instance.FindedObjects = saveGameManager.FindedObjects;
        instance.FinishedDialogues = saveGameManager.FinishedDialogues;
        instance.GottenPage = saveGameManager.GottenPage;
        instance.visited = saveGameManager.visited;
    }
}
