using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public GameObject menuGO;
    public LevelLoader leveLoader;
    public GameObject optionsPanel;
    private GameObject[] otherUIElements;
    public AudioClip bttnClickSound;
    public Animator escFade;

    bool isEscMenuActive;

    private void Awake()
    {
        escFade.enabled = false;
    }

    void Start()
    {
        isEscMenuActive = false;

        menuGO.SetActive(true);
        // Get references to other UI elements in the scene
        otherUIElements = GameObject.FindGameObjectsWithTag("EscUI");       
        Debug.Log(otherUIElements.Length);
    }

    private void Update()
    {    
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            Sfx.Instance.PlaySound(bttnClickSound);
            if (optionsPanel.activeSelf == false)
                {
                if(isEscMenuActive == false)
                {
                    escFade.enabled = true;
                    Debug.Log("hiii");
                    escFade.Play("escMenuFade");
                    isEscMenuActive = true;
                    
                }
                else if(isEscMenuActive == true)
                {
                    Debug.Log("hiii2");
                    escFade.SetTrigger("End");
                    isEscMenuActive = false;
                }  
                
                }
                if (optionsPanel.activeSelf == true)
                {
                OnBackButtonClick();
                }
            }    
    }

    public void OnEscButtonClick()
    {
        /* if (isEscMenuActive == false)
         {
             Sfx.Instance.PlaySound(bttnClickSound);
             escFade.enabled = true;
             escFade.Play("escMenuFade");
             isEscMenuActive = true;
         }

         else
         {
             Sfx.Instance.PlaySound(bttnClickSound);
             escFade.SetTrigger("End");
             isEscMenuActive = false;
         }
        */
        Sfx.Instance.PlaySound(bttnClickSound);

        if (optionsPanel.activeSelf == false)
        {
            if (isEscMenuActive == false)
            {
                escFade.enabled = true;
                Debug.Log("hiii");
                escFade.Play("escMenuFade");
                isEscMenuActive = true;

            }
            else if (isEscMenuActive == true)
            {
                Debug.Log("hiii2");
                escFade.SetTrigger("End");
                isEscMenuActive = false;
            }

        }
        if (optionsPanel.activeSelf == true)
        {
            OnBackButtonClick();
        }

    }

    public void ReturnToMainMenu()
    {
        Sfx.Instance.PlaySound(bttnClickSound);
        leveLoader.LoadMainMenu();
    }

    public void OnSettingsButtonClick()
    {
        Sfx.Instance.PlaySound(bttnClickSound);
        // Enable the Panel
        optionsPanel.gameObject.SetActive(true);

        // Disable other UI elements
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(false);
        }
    }

    public void OnBackButtonClick()
    {
        //disable the Panel
        optionsPanel.gameObject.SetActive(false);
        Sfx.Instance.PlaySound(bttnClickSound);

        // Enable other UI elements
        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(true);
        }
    }

}

