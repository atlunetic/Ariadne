using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnSceneChange : MonoBehaviour
{
    [YarnCommand("ChangeSceneTo")]
    public void ChangeSceneTo(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
