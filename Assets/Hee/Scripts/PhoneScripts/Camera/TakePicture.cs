using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePicture : MonoBehaviour  // 모든 Main Camera에 부착
{
    private bool _willTakeScreenShot = false;
    Texture2D screenTex;
    Rect rect;

    
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

            // WorldToScreenPoint로 증거 오브젝트 인식하는 기능 추가하기 (씬매니저)
            // rect 받아서 rect크기, 위치에 증거 오브젝트가 있나 확인하는 함수
            // 없으면 null, 있으면 이름 반환 해서 조건문 SaveImmediate(이름) 

            CameraController.instance.SaveTemporary(screenTex.EncodeToPNG());
            
        }
    }
}
