using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDiaryPage : MonoBehaviour
{
    public int pageNumber; // The page number to reveal

    private AddDiaryPage addDiaryPage;

    private void Start()
    {
        // Find the AddDiaryPage script
        addDiaryPage = FindObjectOfType<AddDiaryPage>();
    }

    private void OnMouseDown()
    {
        if (addDiaryPage != null)
        {
            addDiaryPage.RevealPage(pageNumber);
        }
        else
        {
            Debug.LogError("AddDiaryPage script not found in the scene.");
        }
    }
}
