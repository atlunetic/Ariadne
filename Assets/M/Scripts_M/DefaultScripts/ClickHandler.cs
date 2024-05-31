using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickHandler : MonoBehaviour
{
    public PhoneClickHandler phoneClickHandler;

    private void OnMouseDown()
    {
        Debug.Log("Letter Clicked, Phone Enabled");

        phoneClickHandler.EnableClickability();
    }
}
