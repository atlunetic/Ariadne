using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class EndingCreditVideo : MonoBehaviour
{

    public VideoPlayer videoPlayer;


    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;

    }

    private void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("S0");
    }
}
