using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class FinalReason : MonoBehaviour
{
    public static FinalReason instance;
    public SceneInfo scenemanager;
    public GameObject confirmB;
    public GameObject[] polaroid;  // 폴라로이드 안 이미지
    public string[] Answer = new string[5];
    public bool[] IsFilled = new bool[]{false, false, false, false, false};  // 폴라로이드 안 이미지

    void Awake(){
        instance = this;
    }
    
    void Start()
    {
        PhoneController.instance.Phone.GetComponent<RectTransform>().transform.localPosition = new Vector3(-200,10,0);
        
        foreach(GameObject img in GalleryController.instance.nonClues)
            img.SetActive(false);
        foreach(GameObject img in GalleryController.instance.Clues){
            GameObject pic = GalleryController.instance.Pictures[img.name];
            pic.transform.GetChild(1).gameObject.SetActive(true);
            pic.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>StartCoroutine(scenemanager.PlayMemory(pic.name)));
            pic.transform.GetChild(2).gameObject.SetActive(true);
            pic.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(()=>PickAnswer(img, pic));
        }
        
        PhoneController.instance.Phone.SetActive(true);
        PhoneController.instance.Gallery.SetActive(true);
    }

    void PickAnswer(GameObject img, GameObject pic){
        for(int i = 0; i < 5; i++){
            if(IsFilled[i]) continue;
            polaroid[i].GetComponent<Image>().sprite = img.GetComponent<Image>().sprite;
            Answer[i] = pic.name;
            img.SetActive(false);
            pic.SetActive(false);
            IsFilled[i] = true;
            foreach(bool b in IsFilled) if(!b) return;
            confirmB.SetActive(true);
            break;
        }
    }

    public void CheckAnswer(){
        int score = 0;
        if(Answer[0]=="StudentID") score++;
        if(Answer[1]=="Meds") score++;
        if(Answer[2]=="Photo") score++;
        if(Answer[3]=="Letter") score++;
        if(Answer[4]=="BrokenGlasses") score++;

        switch(score) {
            case 0: print("사망엔딩"); break;
            case 1: 
            case 2: print("루프엔딩"); break;
            case 3:
            case 4: print("두통엔딩"); break;
            case 5: print("진엔딩"); break;
            default: print("error"); break;
        }
    }
}
