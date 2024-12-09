using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public string sceneName;
    
    // Start is called before the first frame update
    void ChangeSceneAfterDelay()
    {
        Invoke("NewScene", 1f);
    }
    
    void NewScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    /* Another case: change scene when player reaches a certain point
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has a tag "Player"
        {
            SceneManager.LoadScene(sceneName);
        }
    }*/
}
