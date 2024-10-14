using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Fixed : MonoBehaviour
{

    public TMP_Text resolution;
    void Start(){
        Debug.Log(Screen.currentResolution);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        showResolution();
    }
    void Update()
    {
        // A 키를 눌러 전체 화면 전환
        if (Input.GetKeyDown(KeyCode.A))
        {
            Screen.fullScreen = !Screen.fullScreen;
            showResolution();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Screen.SetResolution(2560, 1440, Screen.fullScreen);
            Debug.Log(Screen.currentResolution);
            showResolution();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Screen.SetResolution(2880, 1800, Screen.fullScreen);
            Debug.Log(Screen.currentResolution);
            showResolution();
        }
    }

    void showResolution(){
        resolution.text = Screen.width + " "+ Screen.height + " "+ Screen.safeArea;
    }
}
