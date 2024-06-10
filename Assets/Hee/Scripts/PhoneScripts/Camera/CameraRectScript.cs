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
        
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, 
            Input.mousePosition, 
            null,
            out anchoredPosition
        );
        print(anchoredPosition);
        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0, 1920 - rect.width);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0, 1080 - rect.height);
        print(anchoredPosition);
        rectTransform.anchoredPosition = anchoredPosition;
    }
}
