using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Change()
    {
        if(SceneManager.GetActiveScene().name == "Test1")
        {
            SceneManager.LoadScene("Test2");
        }
        else
        {
            SceneManager.LoadScene("Test1");
        }
        
    }
}
