using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public GameObject EndingIlurst;
    public int EndingNum;

    void Start (){
        GetComponent<Button>().onClick.AddListener(()=>{EndingIlurst.SetActive(true); });
        CheckCondition();
    }
    public void CheckCondition(){
        if(SaveAndLoad.instance.endings.endingNums.Contains(EndingNum)){
            gameObject.SetActive(true);
        }
    }
}
