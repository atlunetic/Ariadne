using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class BackToOfficetel : MonoBehaviour
{
    public void BackToO()
    {
        SceneManager.LoadScene("S3_1_OfficetelEntrance");
    }
}
