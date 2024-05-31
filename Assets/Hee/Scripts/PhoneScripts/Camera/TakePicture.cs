using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour  // 모든 Main Camera에 부착
{
    private bool _willTakeScreenShot = false;
    Texture2D screenTex;
    Rect rect;

    SceneInfo sceneInfo;

    void Start(){
        sceneInfo = GameObject.Find("SceneManager").GetComponent<SceneInfo>();
    }

    public void ScreenShot(RectTransform rectTransform){
        rect = rectTransform.rect;
        screenTex = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        rect.x = rectTransform.position.x;
        rect.y = rectTransform.position.y;
        _willTakeScreenShot = true;
    }

    private void OnPostRender(){ 
        if (_willTakeScreenShot)
        {
            _willTakeScreenShot = false;
            screenTex.ReadPixels(rect, 0, 0);

            string clue = sceneInfo.FindClue(rect);
            
            if(clue is not null)
                CameraController.instance.SaveImmediate(clue, screenTex.EncodeToPNG()); 
            else
                CameraController.instance.SaveTemporary(screenTex.EncodeToPNG());
            
        }
    }
}
