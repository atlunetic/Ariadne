using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartScene()
    {
          SceneManager.LoadScene("S1");
          SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
}
