using System.Collections;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TypingEffect : MonoBehaviour
{

    public TMP_Text textComponent;
    public string fullText;
    public float typingSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (textComponent != null && !string.IsNullOrEmpty(fullText))
        {
            StartCoroutine(TypeTextEffect(fullText));
        }
    }

    IEnumerator TypeTextEffect(string text)
    {
        textComponent.text = string.Empty;

        StringBuilder stringBuilder = new StringBuilder();

        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            textComponent.text = stringBuilder.ToString();
            yield return new WaitForSeconds(typingSpeed);

            /*
            if (i == text.Length - 1)
            {
                yield return new WaitForSeconds(typingSpeed);
                SceneManager.LoadScene("S0");
            }
            */
        }
    }
}
