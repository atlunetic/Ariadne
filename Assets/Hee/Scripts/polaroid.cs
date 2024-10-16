using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class polaroid : MonoBehaviour
{
    int num;
    public Sprite origin;
    void Start()
    {
        num = int.Parse(name);
        
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(unpick);
        transform.GetChild(0).GetComponent<Button>().enabled = false;
    }

    void unpick(){
        FinalReason.instance.confirmB.SetActive(false);
        FinalReason.instance.IsFilled[num] = false;
        GetComponent<Image>().sprite = origin;
        foreach(GameObject img in GalleryController.instance.Clues){
            if(img.name == FinalReason.instance.Answer[num]+"Img") img.SetActive(true);
        }
    }
}
