using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRectScript : MonoBehaviour
{
    RectTransform rectTransform;
    Rect rect;
    
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rect = rectTransform.rect;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Input.mousePosition;
        float posx = Mathf.Clamp(mousepos.x, 0, 1920 - rect.width);
        float posy = Mathf.Clamp(mousepos.y, 0, 1080 - rect.height);
        
        rectTransform.position = new Vector3(posx,posy,mousepos.z);
    }
}
