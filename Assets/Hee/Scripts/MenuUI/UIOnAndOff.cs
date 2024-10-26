using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnAndOff : MonoBehaviour
{
    public GameObject obj;

    void OnMouseDown(){
        if(Menu.instance.BlockClick) return;
        on_obj();
    }

    public void on_obj(){
        obj.SetActive(true);
        Menu.instance.UI_on();
    }
    public void off_obj(){
        obj.SetActive(false);
        Menu.instance.UI_off();
    }
}
