using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Yarn.Unity;

public class vidioplay : MonoBehaviour
{
    public GameObject ariadne1;
    public GameObject ariadne2;
    public GameObject FinalreasonController;
    public GameObject FinalreasonCanvas;
    void Start()
    {
        ariadne1.SetActive(true);
        ariadne1.GetComponent<VideoPlayer>().loopPointReached += (VideoPlayer vp) => {ariadne2.SetActive(true);
                                                                                      ariadne1.SetActive(false);};
    }

    [YarnCommand("Final")]
    void Fianl(){
        ariadne2.SetActive(false);
        FinalreasonController.SetActive(true);
        FinalreasonCanvas.SetActive(true);
    }

}
