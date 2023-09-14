using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;


    

    public void LoadSinglePlayer()
    {      
        StartCoroutine(LoadLevel(1));
    }

    public void LoadAftermatch()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadMatchMaking()
    {
        StartCoroutine(LoadLevel(3));
    }

    

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

}
