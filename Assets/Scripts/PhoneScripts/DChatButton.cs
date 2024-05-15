using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DChatButton : MonoBehaviour
{
    void Start(){
        GetComponent<Button>().onClick.AddListener(IsClicked);
    }
    public void IsClicked(){
        DgramController.instance.ActiveDChatRoom(gameObject.name);
    }
}
