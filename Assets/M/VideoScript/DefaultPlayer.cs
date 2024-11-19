using UnityEngine;
using UnityEngine.Video;
using Yarn.Unity;

public class DefaultPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject VideoParent;

    [YarnCommand("PlayVideo")]
    public void PlayVideo()
    {
        VideoParent.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += EndReached;

    }

    private void EndReached(VideoPlayer vp)
    {
       
        if (Menu.instance.BlockClick) { return; }
        Debug.Log("End Reached. Starting Yarn Dialogue");
        var runner = FindObjectOfType<DialogueRunner>();
        runner.StartDialogue("Memo_VideoTape3");


    }

    [YarnCommand("CloseVideo")]
    public void CloseVideo()
    {
        VideoParent.SetActive(false);
    }
}
