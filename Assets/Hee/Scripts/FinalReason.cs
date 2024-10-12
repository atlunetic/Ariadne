using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Yarn.Unity;

public class FinalReason : MonoBehaviour
{
    public static FinalReason instance;
    public SceneInfo scenemanager;
    public GameObject confirmB;
    public GameObject perfectImg;
    public GameObject perfectVid;
    public GameObject[] polaroid;  // 폴라로이드 안 이미지
    public string[] Answer = new string[5];
    public bool[] IsFilled = new bool[]{false, false, false, false, false};  // 폴라로이드 안 이미지

    public Queue<int> rightanswer = new Queue<int>();
    public string ending;
    private string[] cluenames = new string[]{"Letter","Meds","Laptop","VIPlist","BrokenGlasses","ToiletPaper","GeonwooCard","1342","Photo","StudentID"};

    void Awake(){
        instance = this;
    }
    
    void Start()
    {
        PhoneController.instance.Phone.GetComponent<RectTransform>().transform.localPosition = new Vector3(-200,10,0);
        
        foreach(GameObject img in GalleryController.instance.nonClues)
            img.SetActive(false);
        foreach(GameObject img in GalleryController.instance.originImages)
            img.SetActive(false);
        foreach(GameObject img in GalleryController.instance.Clues){
            GameObject pic = GalleryController.instance.Pictures[img.name];
            pic.transform.GetChild(1).gameObject.SetActive(true);
            pic.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(()=>StartCoroutine(scenemanager.PlayMemory(getcluename(pic.name))));
            pic.transform.GetChild(2).gameObject.SetActive(true);
            pic.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(()=>PickAnswer(img, pic));
        }

        PhoneController.instance.Phone.SetActive(true);
        PhoneController.instance.Gallery.SetActive(true);
        perfectVid.GetComponent<VideoPlayer>().loopPointReached += (VideoPlayer vp) => {vidioplay.instance.breakmemory();};
    }

    void PickAnswer(GameObject img, GameObject pic){
        for(int i = 0; i < 5; i++){
            if(IsFilled[i]) continue;
            polaroid[i].GetComponent<Image>().sprite = img.GetComponent<Image>().sprite;
            polaroid[i].transform.GetChild(0).GetComponent<Button>().enabled = true;
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
        confirmB.SetActive(false);
        PhoneController.instance.Phone.SetActive(false);
        int score = 0;
        if(getcluename(Answer[0])=="StudentID") {score++; rightanswer.Enqueue(1);}
        if(getcluename(Answer[1])=="Meds") {score++; rightanswer.Enqueue(2);}
        if(getcluename(Answer[2])=="Photo") {score++; rightanswer.Enqueue(3);}
        if(getcluename(Answer[3])=="Letter") {score++; rightanswer.Enqueue(4);}
        if(getcluename(Answer[4])=="BrokenGlasses") {score++; rightanswer.Enqueue(5);}

        switch(score) {
            case 0: ending = "ending_bad"; break;
            case 1: 
            case 2: ending = "ending_merrybad"; break;
            case 3:
            case 4: ending = "ending_normal"; break;
            case 5: ending = "realmemory_6"; StartCoroutine("perfect"); return;
            default: print("error"); break;
        }

        vidioplay.instance.breakmemory();
    }

    IEnumerator perfect(){
        perfectImg.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        perfectImg.SetActive(true);
        perfectImg.GetComponent<Image>().CrossFadeAlpha(1.0f,2.0f,false);
        yield return new WaitForSeconds(2f);
        perfectVid.SetActive(true);
        // perfectVid.GetComponent<VideoPlayer>().Play();
        vidioplay.instance.FinalreasonCanvas.SetActive(false);
        perfectImg.SetActive(false);

    }

    public string getcluename(string picname){
        foreach(string cluename in cluenames){
            if(picname.StartsWith(cluename)) return cluename;
        }
        return "";
    }
}
