using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Yarn.Unity;

public class vidioplay : MonoBehaviour
{
    public GameObject ariadne1;
    public GameObject ariadne2;
    public GameObject FinalreasonController;
    public GameObject FinalreasonCanvas;
    public GameObject[] breakglass;
    public GameObject[] memoryBG;
    public DialogueRunner runner;
    
    public static vidioplay instance;
    void Awake(){instance = this;}
    void Start()
    {
        runner = FindAnyObjectByType<DialogueRunner>();
        runner.StartDialogue("lastevent_intro");
        ariadne1.SetActive(true);
        ariadne1.GetComponent<VideoPlayer>().loopPointReached += (VideoPlayer vp) => {ariadne2.SetActive(true);ariadne1.SetActive(false);};                                                                                      
    }

    [YarnCommand("Final")]
    public void Fianl(){
        ariadne2.SetActive(false);
        FinalreasonController.SetActive(true);
        FinalreasonCanvas.SetActive(true);
    }

    [YarnCommand("Break")]
    public void breakmemory(){
        if(FinalReason.instance.rightanswer.Count == 0) runner.StartDialogue(FinalReason.instance.ending);
        int i = FinalReason.instance.rightanswer.Dequeue();
        StartCoroutine(breakglasses(i));
    }

    int before;
    IEnumerator breakglasses(int next){
        for(int i = 1; i<13; i++){
            yield return new WaitForSeconds(0.09f);
            breakglass[i-1].SetActive(false);
            breakglass[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        breakglass[12].GetComponent<Image>().CrossFadeAlpha(0f, 1.5f, false);
        memoryBG[before].SetActive(false);
        before = next-1;
        memoryBG[next-1].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        breakglass[12].SetActive(false);
        breakglass[12].GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
        
        runner.StartDialogue("realmemory_"+next.ToString());
    }

}
