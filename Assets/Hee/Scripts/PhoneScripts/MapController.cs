using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class MapController : MonoBehaviour
{
    public static MapController instance;
    void Awake(){
        if(instance == null)
            instance = this;
    }
    public RectTransform CurrPoint;
    public Button Homebutton;
    public Button Parkbutton;
    public Button BarStreetbutton;
    public Button Hospitalbutton;
    public Button Officetelbutton;
    public Button Clubbutton;
    public Button 편의점711button;

    public Button CurrLocationbutton;
    void Start()
    {
        
    }

    [YarnCommand("GoHome")]
    public void GoHome(){
        // 씬 바꾸기
        CurrPoint.transform.position = new Vector2(-254,156);
        Menu.instance.UI_off();
        DisableButton(Parkbutton);
    }

    [YarnCommand("GoPark")]
    public void GoPark(){     
        CurrPoint.anchoredPosition = new Vector2(-201,-8);
        Menu.instance.UI_off();
        DisableButton(Parkbutton);
        SceneManager.LoadScene("S1_Park");
    }

    [YarnCommand("GoBarStreet")]
    public void GoBarStreet(){
        SceneManager.LoadScene("S1_Street");
        CurrPoint.anchoredPosition = new Vector2(27,251);
        Menu.instance.UI_off();
        DisableButton(BarStreetbutton);
    }

    [YarnCommand("GoHospital")]
    public void GoHospital(){
        SceneManager.LoadScene("S1_Hospital");
        CurrPoint.anchoredPosition = new Vector2(273,32);
        Menu.instance.UI_off();
        DisableButton(Hospitalbutton);
    }

    [YarnCommand("GoOfficetel")]
    public void GoOfficetel(){
        // 씬 바꾸기
        CurrPoint.anchoredPosition = new Vector2(715,204);
        Menu.instance.UI_off();
        DisableButton(Officetelbutton);
    }

    [YarnCommand("GoClub")]
    public void GoClub(){
        // 씬 바꾸기
        CurrPoint.anchoredPosition = new Vector2(108,-61);
        Menu.instance.UI_off();
        Homebutton.onClick.RemoveAllListeners();
        Parkbutton.onClick.RemoveAllListeners();
        Clubbutton.onClick.RemoveAllListeners();
        Officetelbutton.onClick.RemoveAllListeners();
        BarStreetbutton.onClick.RemoveAllListeners();
    }

    public void DisableButton(Button Next){
        CurrLocationbutton.enabled = true;
        Next.enabled = false;
        CurrLocationbutton = Next;
    }
}
