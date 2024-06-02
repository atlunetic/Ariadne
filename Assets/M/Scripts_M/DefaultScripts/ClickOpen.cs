using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOpen : MonoBehaviour
{

    public GameObject ObjectToOpen;
    // Start is called before the first frame update
    public void OnMouseDown()
    {
        ObjectToOpen.SetActive(true);
    }
}
