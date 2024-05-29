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

    public List<int> ChattingLog = new List<int>(); 
    public Dictionary<string, bool> ObjectState = new Dictionary<string, bool>();
}
