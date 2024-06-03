using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class AddDiaryPage : MonoBehaviour
{
    public GameObject Page1;
    public GameObject Page2;
    public GameObject Page3;
    public GameObject Page4;
    public GameObject Page5;
    public GameObject Page6;
    public GameObject Page7;
    public GameObject Page8;
    public GameObject Page9;
    public GameObject Page10;

    [YarnCommand("RevealDiary")]
    public void RevealPage1(string PageNum)
    {
        if(PageNum == "Page1")
        {
            Page1.SetActive(true);
        }

        else if(PageNum == "Page2")
        {
            Page2.SetActive(true);
        }

        else if (PageNum == "Page3")
        {
            Page3.SetActive(true);
        }

        else if (PageNum == "Page4")
        {
            Page4.SetActive(true);
        }

        else if (PageNum == "Page5")
        {
            Page5.SetActive(true);
        }

        else if (PageNum == "Page6")
        {
            Page6.SetActive(true);
        }

        else if (PageNum == "Page7")
        {
            Page7.SetActive(true);
        }

        else if (PageNum == "Page8")
        {
            Page8.SetActive(true);
        }

        else if (PageNum == "Page9")
        {
            Page9.SetActive(true);
        }

        else if (PageNum == "Page10")
        {
            Page10.SetActive(true);
        }

        else
        {
            Debug.Log("Wrong Page Number In Yarn");
        }

    }

}
