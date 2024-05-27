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
    public GameObject CurrPoint;
    public Button Homebutton;
    public Button Parkbutton;
    public Button BarStreetbutton;
    public Button Hospitalbutton;
    public Button Officetelbutton;
    public Button Clubbutton;

    public Button CurrLocationbutton;
    void Start()
    {
        
    }

    [YarnCommand("GoPark")]
    public void GoPark(){
        // 씬 바꾸기
        CurrPoint.transform.position = new Vector2(2,0);
        DisableButton(Parkbutton);
    }

    [YarnCommand("GoBarStreet")]
    public void GoBarStreet(){
        // 씬 바꾸기
        CurrPoint.transform.position = new Vector2(2,0);
        DisableButton(BarStreetbutton);
    }

    [YarnCommand("GoHospital")]
    public void GoHospital(){
        // 씬 바꾸기
        CurrPoint.transform.position = new Vector2(2,0);
        DisableButton(Hospitalbutton);
    }

    [YarnCommand("GoOfficetel")]
    public void GoOfficetel(){
        // 씬 바꾸기
        CurrPoint.transform.position = new Vector2(2,0);
        DisableButton(Officetelbutton);
    }

    public void DisableButton(Button Next){
        CurrLocationbutton.enabled = true;
        Next.enabled = false;
        CurrLocationbutton = Next;
    }
}
