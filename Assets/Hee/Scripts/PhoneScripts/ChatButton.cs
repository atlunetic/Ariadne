using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatButton : MonoBehaviour
{
    void Start(){
        GetComponent<Button>().onClick.AddListener(IsClicked);
    }
    public void IsClicked(){
        ChocoTalkController.instance.ActiveChatRoom(gameObject.name);
    }
}
