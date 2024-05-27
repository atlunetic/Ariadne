using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDiary : MonoBehaviour
{
    public GameObject TurnPageTo;
    public GameObject CurrentPage;
    public void TurnPage()
    {
        CurrentPage.SetActive(false);
        TurnPageTo.SetActive(true);
    }
}
