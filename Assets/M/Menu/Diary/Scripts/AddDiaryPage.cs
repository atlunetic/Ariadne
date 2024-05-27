using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class AddDiaryPage : MonoBehaviour
{
    public GameObject UnopenedPage;

    public void AddNewPage()
    {
        UnopenedPage.SetActive(true);
    }
}
