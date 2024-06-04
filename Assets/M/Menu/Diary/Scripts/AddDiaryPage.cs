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

    public List<GameObject> Diary = new List<GameObject>();


    private void Start()
    {
        Diary.Add(Page1);
        Diary.Add(Page2);
        Diary.Add(Page3);
        Diary.Add(Page4);
        Diary.Add(Page5);
        Diary.Add(Page6);
        Diary.Add(Page7);
        Diary.Add(Page8);
        Diary.Add(Page9);
        Diary.Add(Page10);

        foreach (GameObject obj in Diary)
            obj.SetActive(GameManager.instance.GottenPage.Contains(obj.name));

    }

    [YarnCommand("RevealDiary")]
    public void RevealPage1(string PageNum)
    {
        if(PageNum == "Page1")
        {
            Page1.SetActive(true);
            GameManager.instance.GottenPage.Add("Page1");
        }

        else if(PageNum == "Page2")
        {
            Page2.SetActive(true);
            GameManager.instance.GottenPage.Add("Page2");
        }

        else if (PageNum == "Page3")
        {
            Page3.SetActive(true);
            GameManager.instance.GottenPage.Add("Page3");
        }

        else if (PageNum == "Page4")
        {
            Page4.SetActive(true);
            GameManager.instance.GottenPage.Add("Page4");
        }

        else if (PageNum == "Page5")
        {
            Page5.SetActive(true);
            GameManager.instance.GottenPage.Add("Page5");
        }

        else if (PageNum == "Page6")
        {
            Page6.SetActive(true);
            GameManager.instance.GottenPage.Add("Page6");
        }

        else if (PageNum == "Page7")
        {
            Page7.SetActive(true);
            GameManager.instance.GottenPage.Add("Page7");
        }

        else if (PageNum == "Page8")
        {
            Page8.SetActive(true);
            GameManager.instance.GottenPage.Add("Page8");
        }

        else if (PageNum == "Page9")
        {
            Page9.SetActive(true);
            GameManager.instance.GottenPage.Add("Page9");
        }

        else if (PageNum == "Page10")
        {
            Page10.SetActive(true);
            GameManager.instance.GottenPage.Add("Page10");
        }

        else
        {
            Debug.Log("Wrong Page Number In Yarn");
        }

    }

}
