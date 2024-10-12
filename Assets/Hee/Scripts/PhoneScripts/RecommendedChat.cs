using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecommendedChat : MonoBehaviour
{
    void Start(){
        GetComponent<Button>().onClick.AddListener(()=>{ChatManager.instance.PlusChat(int.Parse(name)); });
    }
}
