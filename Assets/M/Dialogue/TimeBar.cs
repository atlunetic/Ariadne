using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class TimeBar : MonoBehaviour
{
    public GameObject timeSprites;
    public Image timeBar;
    public float timeLimit;
    private float timeLeft;
    private string targetScene;
    private bool isPaused = true;

    [YarnCommand("ShowTB")]
    public void ShowTimeBar(bool status)
    {
        timeSprites.SetActive(status);
        Debug.Log("TimeBar set " + status);
    }

    [YarnCommand("InitTB")]
    public void InitializeTimeBar(string sceneName)
    {
        timeLeft = timeLimit;
        targetScene = sceneName;
        isPaused = true;

    }

    [YarnCommand("PauseTB")]
    public void PauseTimeBar()
    {
        isPaused = true;
        Debug.Log("isPaused changed to " + isPaused);
    }

    [YarnCommand("StartTB")]
    public void ResumeTimeBar()
    {
        isPaused = false;
        Debug.Log("isPaused changed to " + isPaused);
    }

    private void Start()
    {
        isPaused = true;
        Debug.Log("isPaused is currently " + isPaused);
    }
    void Update()
    {
        if (!isPaused)
        {
            //Debug.Log("Timer Start");
            //Debug.Log("isPaused is currently " + isPaused);
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timeBar.fillAmount = timeLeft / timeLimit;
            }

            else
            {
                HandleTimeOut();
            }
        }
        else
        {
            //Debug.Log("Blocked by isPaused");
        }
    }

    private void HandleTimeOut()
    {
        var runner = FindObjectOfType<DialogueRunner>();
        runner.Stop();
        SceneManager.LoadScene(targetScene);
    }




}
