using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Panels for our menu
    public GameObject htpPanel;
    public GameObject optionsPanel;
    public GameObject gameModesScreen;
    
    //UI Elements that we will make appear and dissapear 
    public GameObject[] otherUIElements;

    //Audio Clips for our menu
    public AudioClip bttnHoverSound;
    public AudioClip SPBttnSound;
    public AudioClip bttnClickSound;

    //We use this to change scenes with animations and stuff
    public LevelLoader levelLoader;




    void Start()
    {
        // Get references to other UI elements in the scene
        otherUIElements = GameObject.FindGameObjectsWithTag("UI");
    }

    public void OnHowToPlayButtonClick()
    {
        // Enable or disable the Panel
        htpPanel.gameObject.SetActive(true);

        // Disable other UI elements while the Panel is active
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(!htpPanel.gameObject.activeSelf);
        }
    }

    public void OnBackButtonClick()
    {
        // Enable or disable the Panel
        htpPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(false);
        gameModesScreen.SetActive(false);
        

        // Enable other UI elements
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(!htpPanel.gameObject.activeSelf);
        }
    }

    public void OnOptionsButtonClick()  
    {
        // Enable or disable the Panel
        optionsPanel.gameObject.SetActive(true);

        // Disable other UI elements while the Panel is active
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(!optionsPanel.gameObject.activeSelf);
        }
    }

    public void OnSPBttnClick()
    {
        //Play the SFX
        Sfx.Instance.PlaySound(bttnClickSound);

        //Set the game modes screen active
        gameModesScreen.SetActive(true); 

        //Disable other UI elements
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(false);
        }
        PlayerPrefs.SetInt("ConnectionType", 0);
    }

    public void OnMPBttnClick()
    {
        PlayerPrefs.SetInt("ConnectionType", 1);
        levelLoader.LoadMatchMaking();
    }

    public void OnClassicModeClick()
    {
        Sfx.Instance.PlaySound(SPBttnSound);      
        PlayerPrefs.SetInt("GameMode", 0);
        levelLoader.LoadSinglePlayer();             
    }

    public void OnRapidModeClick()
    {
        Sfx.Instance.PlaySound(SPBttnSound);
        PlayerPrefs.SetInt("GameMode", 1);
        levelLoader.LoadSinglePlayer();                 
    }

    public void PlayBttnClickSound()
    {
        Sfx.Instance.PlaySound(bttnClickSound);
    }

    public void PlayOnBttnHoverSound()
    {
        Sfx.Instance.PlaySound(bttnHoverSound);
    }


    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("exit");
    }

}
