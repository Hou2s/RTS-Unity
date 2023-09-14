using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public AnimationClip fadeOutAnimation;
    public AnimationClip fadeInAnimation;
    public Image fadeImage;

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(LoadSceneAfterFadeOut(sceneName));
        Debug.Log("Hello");
    }

    private IEnumerator LoadSceneAfterFadeOut(string sceneName)
    {
        fadeImage.enabled = true;
        fadeImage.CrossFadeAlpha(1f, fadeOutAnimation.length, false);
        yield return new WaitForSeconds(fadeOutAnimation.length);
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeImage.CrossFadeAlpha(0f, fadeInAnimation.length, false);
    }
}
