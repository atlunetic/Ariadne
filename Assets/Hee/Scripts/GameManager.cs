using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}
    void Awake(){
        if(instance == null)
            instance = this;
    }

    public string NowScene;
    public int NumOfScreenShots = 1;
    public List<int> ChattingLog = new List<int>();
    public List<string> PhotoList = new List<string>();
    public HashSet<string> FindedObjects = new HashSet<string>();
    public HashSet<string> FindedClues = new HashSet<string>();
    public HashSet<string> FinishedDialogues = new HashSet<string>();
}
