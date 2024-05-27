using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Yarn.Unity;

public class CallYarn : MonoBehaviour
{
    public Button gallerybutton;
    public Button camerabutton;
    public Button chocotalkbutton;
    public Button openchatDbutton;

    void Start()
    {
        Callbybutton(gallerybutton, "Gallery");
        Callbybutton(camerabutton, "Camera");
        Callbybutton(chocotalkbutton,"Chocotalk1st");
        Callbybutton(openchatDbutton,"OpenChat");
    }

    private void callYarn(string dialogname){
        var runner = FindObjectOfType<DialogueRunner>();
        if(runner.NodeExists(dialogname))
            runner.StartDialogue(dialogname);
    }

    // 버튼 클릭 이벤트로 얀 다이얼로그를 한 번만 부를 때 사용
    void Callbybutton (Button button, string dialogname){
        UnityAction myAction = null;
        myAction = () => {callYarn(dialogname); button.onClick.RemoveListener(myAction);};
        button.onClick.AddListener(myAction);
    }
}
