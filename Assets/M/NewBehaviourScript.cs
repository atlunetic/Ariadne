using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NewBehaviourScript : MonoBehaviour
{
    public VideoPlayer Vplayer;
    public Animator animator;
    public string animationTriggerName;


    void Start()
    {
        // Subscribe to the loopPointReached event
        //Vplayer.loopPointReached += OnVideoEnd;
    }
    public void StartMemory()
    {
        Vplayer.Play();

        animator.SetTrigger(animationTriggerName);
    }
    /*
    // Event handler to close the video when it ends
    void OnVideoEnd(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false); // Deactivate the GameObject containing the VideoPlayer
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the script is destroyed
        Vplayer.loopPointReached -= OnVideoEnd;
    }
    */
}
