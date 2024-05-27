using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public void GoPark(){
        // 씬 바꾸기
        CurrPoint.transform.position = new Vector2(2,0);
        DisableButton(Parkbutton);
    }

    public void DisableButton(Button Next){
        CurrLocationbutton.enabled = true;
        Next.enabled = false;
        CurrLocationbutton = Next;
    }
}
