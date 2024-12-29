using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OpeningVideoController : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public Button skipButton;
    public float skipButtonDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        skipButton.gameObject.SetActive(false);
        StartCoroutine(ShowSkipButtonAfterDelay());
        skipButton.onClick.AddListener(SkipVideo);
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;
        //subscribe to the video and event

    }

    private IEnumerator ShowSkipButtonAfterDelay()
    {
        //wait for delay
        yield return new WaitForSeconds(skipButtonDelay);
        skipButton.gameObject.SetActive(true);

    }

    private void SkipVideo()
    {
        videoPlayer.Stop();
        SceneManager.LoadScene("S0");
    }

    private void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("S0");
    }
}
