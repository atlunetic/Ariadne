using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    void Awake() {

        StartCoroutine("init");

        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    IEnumerator init(){
        SceneManager.LoadScene("SampleScene");
        yield return new WaitForSeconds(0.1f);
    }
}
