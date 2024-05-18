using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveCanvas : MonoBehaviour
{
     private GameObject canvas;

     private void OnMouseDown() {
        
        if(canvas==null) canvas = GameObject.Find("Canvas");
        if(canvas.activeSelf)
            canvas.SetActive(false);
        else
            canvas.SetActive(true);
     }
}
