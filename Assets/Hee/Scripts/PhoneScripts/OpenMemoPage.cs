using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMemoPage : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(openPage);
    }

    void openPage()
    {
        MemoController.instance.NowOpenedPage = MemoController.instance.MemoPages[gameObject.name];
        PhoneController.instance.ActiveTab(MemoController.instance.MemoPages[gameObject.name]);
    }
}
