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

    public void Moveto(){
        SceneManager.LoadScene("S2_"+gameObject.name);
    }

}
