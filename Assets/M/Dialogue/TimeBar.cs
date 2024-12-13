using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using System.Collections;

public class TimeBar : MonoBehaviour
{
    public GameObject timeSprites; 
    public Image timeBar;
    public float timeLimit;
    private float timeLeft;
    private string targetScene;
    private bool isPaused = true;
    private DialogueRunner dialogueRunner; 
    public OptionsListView optionsListView;

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

        // Ensure the DialogueRunner reference is set
        if (dialogueRunner == null)
        {
            dialogueRunner = FindObjectOfType<DialogueRunner>();
        }

        // Ensure the OptionsListView reference is set
        if (optionsListView == null)
        {
            optionsListView = FindObjectOfType<OptionsListView>();
        }
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
    }

    private void HandleTimeOut()
    {
        dialogueRunner.Stop();
        Debug.Log("Dialogue Stopped");
        StartCoroutine(HideOptionsView()); Debug.Log("Options view manually hidden.");
        ShowTimeBar(false); Debug.Log("Timebar Hidden by script");

        var dialogueViews = dialogueRunner.dialogueViews;
        foreach (var view in dialogueViews)
        {
            if (view is OptionsListView optionsView)
            {
                optionsView.DialogueComplete();
                Debug.Log("OptionsListView DialogueComplete called.");
            }
        }


        SceneManager.LoadScene(targetScene);
    }

    private IEnumerator HideOptionsView()
    {
        if (optionsListView != null)
        {
            var canvasGroup = optionsListView.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                yield return StartCoroutine(Effects.FadeAlpha(canvasGroup, canvasGroup.alpha, 0, 0.1f));
            }

            // Disable all option views
            foreach (var optionView in optionsListView.GetComponentsInChildren<OptionView>())
            {
                optionView.gameObject.SetActive(false);
            }

            Debug.Log("Options view hidden.");
        }
    }
}
