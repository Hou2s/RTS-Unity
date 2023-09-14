using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    //public static SceneManage instance;

    /*private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    public void LoadSinglePlayer()
    {
        PlayerPrefs.SetInt("GameMode", 0);
        SceneManager.LoadScene(1);
    }

    public void LoadMultiPlayer()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
        
    }

    public void LoadGameOver()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(2);
    }
}
