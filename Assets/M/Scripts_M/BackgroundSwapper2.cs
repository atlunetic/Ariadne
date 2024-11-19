using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class BackgroundSwapper2 : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public Sprite[] backgrounds;
    public float swapInterval = 1f;

    private int currentIndex = 0;
    private Coroutine swapCoroutine;



    void Start()
    {
        StartBackgroundLoop();

    }


    [YarnCommand("StartBackgroundLoop2")]
    public void StartBackgroundLoop()
    {
        if (swapCoroutine == null)
        {
            swapCoroutine = StartCoroutine(SwapBackgrounds());
        }
    }

    [YarnCommand("StopBackgroundLoop2")]
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
