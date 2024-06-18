using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TheEnd : MonoBehaviour
{
    private Animator animator;
    private bool animationEnded = false;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Ensure the Animator is assigned
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
            return;
        }

        // Start the animation
        animator.SetTrigger("PlayAnimation");
    }

    void Update()
    {
        // Check if the animation has finished and the coroutine hasn't started yet
        if (!animationEnded && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0))
        {
            animationEnded = true;
            // Start the coroutine to change the scene after 5 seconds
            StartCoroutine(ChangeSceneAfterDelay(5f));
        }
    }

    private IEnumerator ChangeSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the next scene
        SceneManager.LoadScene("S0");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public void DebugForAnimation()
    {
        Debug.Log("Animation event triggered!");
        // Add your custom logic here
    }


}
