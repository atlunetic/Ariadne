using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BackgroundSwapper : MonoBehaviour
{

    public SpriteRenderer backgroundRenderer;
    public Sprite[] backgrounds;
    public float swapInterval = 1f;

    private int currentIndex = 0;
    private Coroutine swapCoroutine;

 

    void Start()
    {
        /*
        if (backgrounds.Length > 0)
        {
            backgroundRenderer.sprite = backgrounds[0];
        }
        */

    }


    [YarnCommand("StartBackgroundLoop")]
    public void StartBackgroundLoop()
    {
        if (swapCoroutine == null)
        {
            swapCoroutine = StartCoroutine(SwapBackgrounds());
        }
    }

    [YarnCommand("StopBackgroundLoop")]
    public void StopBackgroundLoop()
    {
        if (swapCoroutine != null)
        {
            StopCoroutine(swapCoroutine);
            swapCoroutine = null;
        }
    }

    private IEnumerator SwapBackgrounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(swapInterval);
            currentIndex = (currentIndex + 1) % backgrounds.Length;
            backgroundRenderer.sprite = backgrounds[currentIndex];
        }
    }
}
