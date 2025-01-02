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
    public Sprite BlueDot;

    void Start(){
        if(GameManager.instance.NowScene.StartsWith("S4_")){
            Homebutton.onClick.AddListener(GoHome);
            BarStreetbutton.onClick.AddListener(GoBarStreet);
            Parkbutton.onClick.AddListener(GoPark);
            DisableButton(Homebutton);
            if((GameManager.instance.visited & 1) != 0) Parkbutton.GetComponent<Image>().sprite = BlueDot;
            if((GameManager.instance.visited & 2) != 0) BarStreetbutton.GetComponent<Image>().sprite = BlueDot;
            switch(GameManager.instance.NowScene){
                case "S4_2_R_JisooRoom":
                case "S4_3_R_JiwonRoom": 
                    CurrPoint.anchoredPosition = new Vector2(-254,156);
                    DisableButton(Homebutton);
                    break;
                case "S4_5_R_Park": 
                    CurrPoint.anchoredPosition = new Vector2(-201,-8);
                    DisableButton(Parkbutton);
                    break;
                case "S4_4_R_Street": 
                    CurrPoint.anchoredPosition = new Vector2(27,251);
                    DisableButton(BarStreetbutton);
                    break;
                default: Debug.Log("unknown place: "+GameManager.instance.NowScene); break;
            }
            return;
        }

        if((GameManager.instance.visited & 1) != 0) Parkbutton.GetComponent<Image>().sprite = BlueDot;
        if((GameManager.instance.visited & 2) != 0) BarStreetbutton.GetComponent<Image>().sprite = BlueDot;
        if((GameManager.instance.visited & 4) != 0) Hospitalbutton.GetComponent<Image>().sprite = BlueDot;
        if(!GameManager.instance.NowScene.StartsWith("S1")) Clubbutton.GetComponent<Image>().sprite = BlueDot;
    }

    [YarnCommand("GoHome")]
    public void GoHome(){
        CurrPoint.anchoredPosition = new Vector2(-254,156);
        Menu.instance.UI_off();
        DisableButton(Homebutton);
        if(GameManager.instance.NowScene.StartsWith("S4_"))
            SceneManager.LoadScene("S4_2_R_JisooRoom");
        else
            SceneManager.LoadScene("S1_2_JiwonRoom");
    }

    [YarnCommand("GoPark")]
    public void GoPark(){     
        CurrPoint.anchoredPosition = new Vector2(-201,-8);
        Menu.instance.UI_off();
        Parkbutton.GetComponent<Image>().sprite = BlueDot;
        DisableButton(Parkbutton);
        if(GameManager.instance.NowScene.StartsWith("S4_"))
            SceneManager.LoadScene("S4_5_R_Park");
        else 
            SceneManager.LoadScene("S1_Park");
        GameManager.instance.visited |= 1;
    }

    [YarnCommand("GoBarStreet")]
    public void GoBarStreet(){
        if(GameManager.instance.NowScene.StartsWith("S4_"))
            SceneManager.LoadScene("S4_4_R_Street");
        else 
            SceneManager.LoadScene("S1_Street");
        CurrPoint.anchoredPosition = new Vector2(27,251);
        Menu.instance.UI_off();
        BarStreetbutton.GetComponent<Image>().sprite = BlueDot;
        DisableButton(BarStreetbutton);
        GameManager.instance.visited |= 2;
    }

    [YarnCommand("GoHospital")]
    public void GoHospital(){
        SceneManager.LoadScene("S1_Hospital");
        CurrPoint.anchoredPosition = new Vector2(273,32);
        Menu.instance.UI_off();
        Hospitalbutton.GetComponent<Image>().sprite = BlueDot;
        DisableButton(Hospitalbutton);
        GameManager.instance.visited |= 4;
    }

    [YarnCommand("GoOfficetel")]
    public void GoOfficetel(){  // 3장
        // 씬 바꾸기
        CurrPoint.anchoredPosition = new Vector2(715,204);
        Menu.instance.UI_off();
        Officetelbutton.GetComponent<Image>().sprite = BlueDot;
        CallYarn.instance.InS3();
    }

    [YarnCommand("GoClub")]
    public void GoClub(){  // 2장으로 넘어가기
        SceneManager.LoadScene("S1");
        CurrPoint.anchoredPosition = new Vector2(108,-61);
        Menu.instance.UI_off();
        Clubbutton.GetComponent<Image>().sprite = BlueDot;
        CallYarn.instance.InS2();
    }
    
    /// <summary>
    /// Next 버튼을 비활성화하고 현재 위치의 버튼을 활성화함
    /// </summary>
    public void DisableButton(Button Next){
        CurrLocationbutton.enabled = true;
        Next.enabled = false;
        CurrLocationbutton = Next;
    }
}
