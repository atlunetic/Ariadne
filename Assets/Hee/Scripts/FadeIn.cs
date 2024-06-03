using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image image;
    private CanvasGroup imageGroup;
    void Awake(){
        image = GetComponent<Image>();
        if(image == null){
            imageGroup = GetComponent<CanvasGroup>();
            imageGroup.alpha = 0f;
        }
        else image.canvasRenderer.SetAlpha(0.0f);
    }
    void OnEnable() {
        if(image == null) StartCoroutine("GroupFadeIn");
        else image.CrossFadeAlpha(1.0f, 0.6f, false);
    }
    IEnumerator GroupFadeIn()
    {
        while (imageGroup.alpha < 1)
        {
            imageGroup.alpha += Time.deltaTime / 0.8f;
            yield return null;
        }
    }

}
