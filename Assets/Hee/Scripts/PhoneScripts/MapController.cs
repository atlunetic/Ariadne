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
    public Button Bakerybutton;
    public Button AnotherClubbutton;
    public Button Schoolbutton;

    public Button CurrLocationbutton;

    [YarnCommand("GoHome")]
    public void GoHome(){
        CurrPoint.anchoredPosition = new Vector2(-254,156);
        Menu.instance.UI_off();
        DisableButton(Homebutton);
        SceneManager.LoadScene("S1_2_JiwonRoom");
    }

    [YarnCommand("GoPark")]
    public void GoPark(){     
        CurrPoint.anchoredPosition = new Vector2(-201,-8);
        Menu.instance.UI_off();
        DisableButton(Parkbutton);
        SceneManager.LoadScene("S1_Park");
        GameManager.instance.visited |= 1;
    }

    [YarnCommand("GoBarStreet")]
    public void GoBarStreet(){
        SceneManager.LoadScene("S1_Street");
        CurrPoint.anchoredPosition = new Vector2(27,251);
        Menu.instance.UI_off();
        DisableButton(BarStreetbutton);
        GameManager.instance.visited |= 2;
    }

    [YarnCommand("GoHospital")]
    public void GoHospital(){
        SceneManager.LoadScene("S1_Hospital");
        CurrPoint.anchoredPosition = new Vector2(273,32);
        Menu.instance.UI_off();
        DisableButton(Hospitalbutton);
        GameManager.instance.visited |= 4;
    }

    [YarnCommand("GoOfficetel")]
    public void GoOfficetel(){  // 3장
        // 씬 바꾸기
        CurrPoint.anchoredPosition = new Vector2(715,204);
        Menu.instance.UI_off();
        CallYarn.instance.InS3();
    }

    [YarnCommand("GoClub")]
    public void GoClub(){  // 2장으로 넘어가기
        SceneManager.LoadScene("S1");
        CurrPoint.anchoredPosition = new Vector2(108,-61);
        Menu.instance.UI_off();
        CallYarn.instance.InS2();
    }
    

    public void DisableButton(Button Next){
        CurrLocationbutton.enabled = true;
        Next.enabled = false;
        CurrLocationbutton = Next;
    }
}
