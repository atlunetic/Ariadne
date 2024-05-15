using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject Panel;

    public void Open()
    {
        if(Panel != null)
        {
            Panel.SetActive(true);
        }
    }
}
