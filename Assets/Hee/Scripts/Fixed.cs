using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fixed : MonoBehaviour
{

    /* 해상도 설정하는 함수 */
     void Start()    {        
        var camera = GetComponent<Camera>();
        var r = camera.rect;
        print(Screen.width+" "+Screen.height);
        var scaleheight = ((float)Screen.width / Screen.height) / (16f / 9f);        
        var scalewidth = 1f / scaleheight;    
        if (scaleheight < 1f)        
        {            
            r.height = scaleheight;            
            r.y = (1f - scaleheight) / 2f;        
        }        
        else        
        {            
            r.width = scalewidth;            
            r.x = (1f - scalewidth) / 2f;        
        }         
        camera.rect = r;    
        print(Screen.width+" "+Screen.height);
    }
        void OnPreCull() => GL.Clear(true, true, Color.black);
}
