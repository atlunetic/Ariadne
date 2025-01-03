using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class EndingCreditVideo : MonoBehaviour
{

    public VideoPlayer videoPlayer;


    // Start is called before the first frame update
    [YarnCommand("StartCredits")]
    public void StartCredits()
    {
        videoPlayer.Play();
        Debug.Log("Ending Video Start");
        videoPlayer.loopPointReached += EndReached;

    }

    private void EndReached(VideoPlayer vp)
    {
        Debug.Log("Video ended, loading S0");
        SceneManager.LoadScene("S0");
        YarnMFunctions.instance.SetDialogueCanvas(false);
    }
}
