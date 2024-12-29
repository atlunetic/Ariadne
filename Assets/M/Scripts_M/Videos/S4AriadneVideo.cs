using System.Collections;
using UnityEngine.Video;
using UnityEngine;
using Yarn.Unity;

public class S4AriadneVideo : MonoBehaviour
{

    public GameObject ariadne1;
    public GameObject ariadne2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [YarnCommand("StartAriadne")]
    public void StartAriadne()
    {
        ariadne1.SetActive(true);

        ariadne1.GetComponent<VideoPlayer>().loopPointReached += (VideoPlayer vp) => { ariadne1.SetActive(false); ariadne2.SetActive(true); };
    }


    [YarnCommand("EndAriadne")]
    public void EndAriadne()
    {
        ariadne1.SetActive(false);
        ariadne2.SetActive(false);
    }
}
