using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnSceneChange : MonoBehaviour
{
    [YarnCommand("ChangeSceneTo")]
    public void ChangeSceneTo(string sceneName1, string sceneName2 = "")
    {
        // Load the first scene normally
        SceneManager.LoadScene(sceneName1);

        // If a second scene name is provided, load it additively
        if (!string.IsNullOrEmpty(sceneName2))
        {
            //SceneManager.LoadScene(sceneName2, LoadSceneMode.Additive);
        }
    }
}
