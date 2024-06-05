using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool IsLoad = false;  // 불러오기 시 true
    public string NowScene;
    public int NumOfScreenShots = 1;  // 불러오기 시 더 큰 수로 적용
    public int GeonWooScore = 0;
    public bool StaffroomOpen = false;
    public List<int> ChattingLog = new List<int>();
    public List<string> PhotoList = new List<string>();
    public HashSet<string> FindedObjects = new HashSet<string>();
    public HashSet<string> FindedClues = new HashSet<string>();
    public HashSet<string> FinishedDialogues = new HashSet<string>();
    public HashSet<string> GottenPage = new HashSet<string>();

    public int visited;

    public bool S1Ended(){
        // Letter, Meds, Laptop, Drawer_Hairpin, DrawerR_Wallet
        return FindedClues.Contains("Laptop") && FindedClues.Contains("Meds") && FindedClues.Contains("Letter") &&
               FindedObjects.Contains("Drawer_Hairpin") && FindedObjects.Contains("DrawerR_Wallet") &&
               visited == 7;
    }
}
