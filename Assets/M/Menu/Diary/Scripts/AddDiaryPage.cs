using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class AddDiaryPage : MonoBehaviour
{
    public string menuSceneName = "Menu";

    private string diaryRootPath = "MenuUI/Diary/Diary/EmptyDiary";
    private GameObject[] pages;

    private void Start()
    {
        StartCoroutine(FindDiaryPages());
    }

    private IEnumerator FindDiaryPages()
    {

        Scene menuScene = SceneManager.GetSceneByName(menuSceneName);
        foreach (GameObject obj in menuScene.GetRootGameObjects())
        {
            Transform diaryTransform = obj.transform.Find(diaryRootPath);
            if (diaryTransform != null)
            {
                pages = new GameObject[10];
                for (int i = 0; i < pages.Length; i++)
                {
                    Transform pageTransform = diaryTransform.Find("Page" + (i + 1));
                    if (pageTransform != null)
                    {
                        pages[i] = pageTransform.gameObject;
                    }
                }
                break;
            }
        }
        yield return null;
    }

    public void RevealPage(int pageNumber)
    {
        if (pages == null || pages.Length < pageNumber || pageNumber <= 0)
        {
            Debug.LogError($"Page {pageNumber} not found or not properly initialized.");
            return;
        }

        // Activate the specified page
        if (pages[pageNumber - 1] != null)
        {
            pages[pageNumber - 1].SetActive(true);
        }
        else
        {
            Debug.LogError($"Page {pageNumber} GameObject is not assigned.");
        }
    }
}
