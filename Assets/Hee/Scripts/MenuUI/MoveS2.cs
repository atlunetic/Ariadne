using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveS2 : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Moveto);
    }

    private void OnEnable() {
        if(GameManager.instance.NowScene=="S2_"+gameObject.name) GetComponent<Button>().enabled = false;
    }

    public void Moveto(){
        if(GameManager.instance.NowScene=="S2_"+gameObject.name) return;
        SceneManager.LoadScene("S2_"+gameObject.name);
        Menu.instance.UI_off();
    }

}
