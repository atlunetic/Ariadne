using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LogoVideoController : MonoBehaviour
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
        SceneManager.LoadScene("SWarning");
    }
}
