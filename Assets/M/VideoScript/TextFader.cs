using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextFader : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public float fadeDuration = 1f; // Duration of fade in/out
    public float initialDelay = 1.5f; // Initial delay before first fade in
    public float fadeOutDelay = 4f; // Delay before fade out
    public float fadeInThirdTextDelay = 1f; // Delay before third text fade in
    public float fadeOutDelay2 = 5f;
 
    //public Button endButton;
    //public float endButtonDelay = 2f;

    void Start()
    {

        //SetTextAlpha(text1, 0);
        //SetTextAlpha(text2, 0);
        //SetTextAlpha(text3, 0);

        //endButton.gameObject.SetActive(false);
        //StartCoroutine(ShowEndButtonAfterDelay());

        StartCoroutine(FadeTextSequence());
    }

    IEnumerator FadeTextSequence()
    {
        yield return new WaitForSeconds(initialDelay);

        StartCoroutine(FadeInText(text1));
        StartCoroutine(FadeInText(text2));

        yield return new WaitForSeconds(fadeOutDelay);

        StartCoroutine(FadeOutText(text1));
        StartCoroutine(FadeOutText(text2));


        yield return new WaitForSeconds(fadeDuration + fadeInThirdTextDelay);

        StartCoroutine(FadeInText(text3));

        yield return new WaitForSeconds(fadeOutDelay2);

        yield return StartCoroutine(FadeOutText(text3));

        SceneManager.LoadScene("SOpening");

    }

    IEnumerator FadeInText(TextMeshProUGUI text)
    {
        // Fade from transparent to opaque
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            Color newColor = text.color;
            newColor.a = Mathf.Lerp(0, 1, t);
            text.color = newColor;
            yield return null;
        }
        // Ensure full opacity at the end
        Color finalColor = text.color;
        finalColor.a = 1;
        text.color = finalColor;
    }

    IEnumerator FadeOutText(TextMeshProUGUI text)
    {
        // Fade from opaque to transparent
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            Color newColor = text.color;
            newColor.a = Mathf.Lerp(1, 0, t);
            text.color = newColor;
            yield return null;
        }
        // Ensure full transparency at the end
        Color finalColor = text.color;
        finalColor.a = 0;
        text.color = finalColor;
    }

    /*
    void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
    */

    /*
    private IEnumerator ShowEndButtonAfterDelay()
    {
        //wait for delay
        yield return new WaitForSeconds(endButtonDelay);
        endButton.gameObject.SetActive(true);

    }
    */
}
