using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 증거 촬영시 나오는 배경들(각 씬의 Canvas_ForMemories에 존재)과 이미지 그룹(GalleryController에 존재)에 붙여주면 됨 */
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
        else {
            image.canvasRenderer.SetAlpha(0.0f);
            image.CrossFadeAlpha(1.0f, 0.6f, false);
        }
    }
    IEnumerator GroupFadeIn()
    {
        imageGroup.alpha = 0f;
        while (imageGroup.alpha < 1)
        {
            imageGroup.alpha += Time.deltaTime / 0.8f;
            yield return null;
        }
    }

}
