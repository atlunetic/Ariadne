using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecommendFriendButton : MonoBehaviour
{
    public GameObject Friend;
    public GameObject FriendChatButton;
    void Start()
    {
        if(GameManager.instance.RecommendedFriends.Contains(name)){
            Friend.SetActive(true);
            FriendChatButton.SetActive(true);
        }else{
            GetComponent<Button>().onClick.AddListener(()=>{ActiveIt(); GameManager.instance.RecommendedFriends.Add(name);});
        }
    }
    void ActiveIt(){
        Friend.SetActive(true);
        FriendChatButton.SetActive(true);
    }

}
