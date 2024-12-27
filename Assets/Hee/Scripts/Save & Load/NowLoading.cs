using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NowLoading : MonoBehaviour
{
    void OnEnable() {
        StartCoroutine("Loading");
    }

    IEnumerator Loading(){
        while(true){
        transform.GetChild(0).GetComponent<TMP_Text>().text = "Loading.";
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(0).GetComponent<TMP_Text>().text = "Loading..";
        yield return new WaitForSeconds(0.4f);
        transform.GetChild(0).GetComponent<TMP_Text>().text = "Loading...";
        yield return new WaitForSeconds(0.4f);
        }
    }

    void OnDisable() {
        StopCoroutine("Loading");
    }
}
