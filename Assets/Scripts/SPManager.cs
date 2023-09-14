using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SPManager : MonoBehaviour
{
    //victory/defeat canvas
    public GameObject defeatCanvas;
    public GameObject victoryCanvas;
   

    //Audio Clips
    public AudioClip victorySound;
    public AudioClip roundWinSound;
    public AudioClip roundLostSound;
    public AudioClip roundDrawSound;
    public AudioClip defeatSound;
    public AudioClip bttnHoverSound;
    public AudioClip rockSfx;
    public AudioClip paperSfx;
    public AudioClip scissorsSfx;

    //I think this is for the 3 buttons (RPS)
    public CanvasGroup bttnGroup;
    

    //ANIMATIONS
    public Animator tmpFade;
    public Animator choiceImageFade;
    public Animator oppchoiceImageFade;
    public Animator[] scoreAnimators;
    public Animator[] oppscoreAnimators;
    
    //I needed that to change scenes
    public LevelLoader levelLoader;

    //Variable to know if someone won
    private bool gameOver = false;

    //Those are images that will appear when you choose between RPS
    public GameObject choiceImage;
    public GameObject oppChoiceImage;

    //Those are UI objects to see whats your score
    public Image rockScore1;
    public Image rockScore2;
    public Image rockScore3;
    public Image paperScore1;
    public Image paperScore2;
    public Image paperScore3;
    public Image scissorsScore1;
    public Image scissorsScore2;
    public Image scissorsScore3;

    //Those are UI objects to see opponent score
    public Image opprockScore1;
    public Image opprockScore2;
    public Image opprockScore3;
    public Image opppaperScore1;
    public Image opppaperScore2;
    public Image opppaperScore3;
    public Image oppscissorsScore1;
    public Image oppscissorsScore2;
    public Image oppscissorsScore3;

    //This is a counter to count the amount of rounds played
    int roundCounter = 1;

    //counters to count your amount of wins with each object
    int rockWins;
    int paperWins;
    int scissorsWins;

    //counters to count opponent's amount of wins with each object
    int oppRockWins;
    int oppPaperWins;
    int oppScissorsWins;

    //This is the text that will appear between each round saying stuff like "You win this round" or "It's a draw!"
    public TMP_Text betweenRoundsText;
    //I needed a gameobject for some reason
    public GameObject betweenRoundsTextGO;

    //I'm not too sure what's that for
    public TMP_Text resultText;
   
    //Three interactable buttons
    public GameObject rockButton;
    public GameObject paperButton;
    public GameObject scissorsButton;

    //This text will display the timer, pretty self explanatory eh?
    public Text timerText;

    //I don't remember why I needed those exactly, anyway they're private so who cares
    private string playerChoice;
    private string opponentChoice;

    //This is the timer
    private float timer;
    
    //Variable to know if the timer reached zero
    private bool timerReachedZero = false;

    //I don't remember why I needed that... Oh yeah it's some weird mechanic
    private bool timerPause = false;

    //I don't remember why I needed that either
    private string playerWon = null;




    private void Awake()
    {
        //disable all animators so that they dont play on start
        for(int i = 0; i < scoreAnimators.Length; i++)
        {
            scoreAnimators[i].enabled = false;
            oppscoreAnimators[i].enabled = false;
        }
    }

    private void Start()
    {       
        betweenRoundsTextGO.SetActive(false);
        choiceImage.SetActive(false);
        oppChoiceImage.SetActive(false);
  
        //Set the timer depending on the game mode chosen
        if(PlayerPrefs.GetInt("GameMode") == 0)
        {
            timer = 30f;
        }
        if (PlayerPrefs.GetInt("GameMode") == 1)
        {
            timer = 15f;
        }
       
        rockButton.GetComponent<Button>().onClick.AddListener(RockButtonClicked);
        paperButton.GetComponent<Button>().onClick.AddListener(PaperButtonClicked);
        scissorsButton.GetComponent<Button>().onClick.AddListener(ScissorsButtonClicked);
        resultText.text = "Round " + roundCounter;              
    }

    void Update()
    {
        //Timer behavior
        if (timerPause == false)
        {
            timer -= Time.deltaTime;
        }

        if (timer >= 5)
        {
            timerText.color = Color.white;
            timerText.text = timer.ToString("f0");            
        }
        if(timer < 5 && timer > 0) 
        {
            timerText.color = Color.red;
            timerText.text = timer.ToString("f1");            
        }

        if (timer <= 0 && !timerReachedZero)
        {
            timerReachedZero = true;
            timerText.color = Color.red;
            timerText.text = timer.ToString("f1");
            ChooseOpponentChoice();
            CheckWinner();
        }

     
    }

    public void RockButtonClicked()
    {
        playerChoice = "rock"; 
        timerPause = true;
        ChooseOpponentChoice();
        CheckWinner();       
    }

    public void PaperButtonClicked()
    {
        playerChoice = "paper";       
        timerPause = true;
        ChooseOpponentChoice();
        CheckWinner();     
    }

    public void ScissorsButtonClicked()
    {
        playerChoice = "scissors";        
        timerPause = true;
        ChooseOpponentChoice();
        CheckWinner();       
    }

    //Randomly chooses an item for the AI
    private void ChooseOpponentChoice()
    {
        int randomNumber = Random.Range(0, 3);

        if (randomNumber == 0)
        {
            opponentChoice = "rock";
        }
        else if (randomNumber == 1)
        {
            opponentChoice = "paper";
        }
        else
        {
            opponentChoice = "scissors";
        }      
    }
    //Method that checks who won and adds up the score
    private void CheckWinner()
    {
        if (playerChoice == "rock")
        {
            if (opponentChoice == "rock")
            {
                playerWon = "Draw";
            }
            else if (opponentChoice == "paper")
            {
                playerWon = "Lost";
                oppPaperWins++;
                if (PlayerPrefs.GetInt("GameMode") == 0)
                {
                    if (rockWins > 0)
                    {
                        rockWins--;
                    }
                }
            }
            else if (opponentChoice == "scissors")
            {
                playerWon = "Win";
                rockWins++;
                if (PlayerPrefs.GetInt("GameMode") == 0)
                {
                    if (oppScissorsWins > 0)
                    {
                        oppScissorsWins--;
                    }
                }
            }
        }

        else if (playerChoice == "paper")
        {
            if (opponentChoice == "rock")
            {
                playerWon = "Win";
                paperWins++;
                if (PlayerPrefs.GetInt("GameMode") == 0)
                {
                    if (oppRockWins > 0)
                    {
                        oppRockWins--;
                    }
                }
            }
            else if (opponentChoice == "paper")
            {
                playerWon = "Draw";
            }
            else if (opponentChoice == "scissors")
            {
                playerWon = "Lost";
                oppScissorsWins++;
                if (PlayerPrefs.GetInt("GameMode") == 0)
                {
                    if (paperWins > 0)
                    {
                        paperWins--;
                    }
                }
            }
        }
        else if (playerChoice == "scissors")
        {
            if (opponentChoice == "rock")
            {
                playerWon = "Lost";
                oppRockWins++;
                if (PlayerPrefs.GetInt("GameMode") == 0)
                {
                    if (scissorsWins > 0)
                    {
                        scissorsWins--;
                    }
                }
            }
            else if (opponentChoice == "paper")
            {
                playerWon = "Win";
                scissorsWins++;
                if (PlayerPrefs.GetInt("GameMode") == 0)
                {
                    if (oppPaperWins > 0)
                    {
                        oppPaperWins--;
                    }
                }
            }
            else if (opponentChoice == "scissors")
            {
                playerWon = "Draw";
            }
        }
        else if (playerChoice == null)
            {
            if (opponentChoice == "rock")
            {
                playerWon = "Lost";
                oppRockWins++;
            }
            else if (opponentChoice == "paper")
            {
                playerWon = "Lost";
                oppPaperWins++;
            }
            else if (opponentChoice == "scissors")
            {
                playerWon = "Lost";
                oppScissorsWins++;
            }
        }

        //Check if the win conditions are met
        if (rockWins == 3 || paperWins == 3 || scissorsWins == 3 || rockWins >= 1 && paperWins >=1 && scissorsWins >= 1)
        {
            gameOver = true;
            StartCoroutine(ActionBetweenRounds());
        }

        else if (oppRockWins == 3 || oppPaperWins == 3 || oppScissorsWins == 3 || oppRockWins >= 1 && oppPaperWins >= 1 && oppScissorsWins >= 1)
        {         
            gameOver = true;
            StartCoroutine(ActionBetweenRounds());
        }
        else
        {          
            StartCoroutine(ActionBetweenRounds());           
        }
    }

    //Method that loads the gameOver scene
    IEnumerator LoadSceneAfterSeconds()
    {
        
        Invoke("updateGameEndText", 4.5f);
        
                
        yield return new WaitForSeconds(9f);

        levelLoader.LoadAftermatch();
    }

    //Method that takes care of all the behavior between each round.
    IEnumerator ActionBetweenRounds()
    {
        bttnGroup.interactable = false;
        updateImageChoice();

        if (gameOver == false)
        {
            yield return new WaitForSeconds(4.5f);

            roundCounter++;
            resultText.text = "Round " + roundCounter;
            tmpFade.SetTrigger("Start");
            choiceImageFade.SetTrigger("Start");
            oppchoiceImageFade.SetTrigger("Start");
            playerChoice = null;
            opponentChoice = null;
            Invoke("timerReset", 1.5f);           
        }
        else
        {
            StartCoroutine(LoadSceneAfterSeconds());
        }          
    }

    //method that i created just so that i can invoke it
    private void timerReset()
    {
        if (PlayerPrefs.GetInt("GameMode") == 0)
        {
            timer = 30f;
        }
        if (PlayerPrefs.GetInt("GameMode") == 1)
        {
            timer = 15f;
        }
        timerReachedZero = false;
        timerPause = false;
        bttnGroup.interactable = true;        
    }
   
    //method that checks how many rock , paper and scissors wins and enables the images in the score
    private void updateScore()
    {
        //player score
        if(rockWins == 0)
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (rockScore1.enabled == true)
                {
                    scoreAnimators[0].SetTrigger("Start");                  
                }
            }
        }
        if (rockWins == 1)
        {
            if (rockScore1.enabled == false)
            {
                if (scoreAnimators[0].enabled == false)
                {
                    scoreAnimators[0].enabled = true;
                }
                else
                {
                    scoreAnimators[0].Play("Fade1");
                }
            }
            if(PlayerPrefs.GetInt("GameMode") == 0)
            {
                if(rockScore2.enabled == true)
                {
                    scoreAnimators[1].SetTrigger("Start");
                }
            }
        }
        if (rockWins == 2)
        {
            if (rockScore2.enabled == false)
            {
                if (scoreAnimators[1].enabled == false)
                {
                    scoreAnimators[1].enabled = true;
                }
                else
                {
                    scoreAnimators[1].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (rockScore3.enabled == true)
                {
                    scoreAnimators[2].SetTrigger("Start");
                }
            }
        }

        if (rockWins == 3)
        {
            if (rockScore3.enabled == false)
            {
                if (scoreAnimators[2].enabled == false)
                {
                    scoreAnimators[2].enabled = true;
                }
               
            }
            
        }
        if (paperWins == 0)
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (paperScore1.enabled == true)
                {
                    scoreAnimators[3].SetTrigger("Start");
                }
            }
        }
        if (paperWins == 1)
        {
            if (paperScore1.enabled == false)
            {
                if (scoreAnimators[3].enabled == false)
                {
                    scoreAnimators[3].enabled = true;
                }
                else
                {
                    scoreAnimators[3].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (paperScore2.enabled == true)
                {
                    scoreAnimators[4].SetTrigger("Start");
                }
            }
        }
        if (paperWins == 2)
        {
            if (paperScore2.enabled == false)
            {
                if (scoreAnimators[4].enabled == false)
                {
                    scoreAnimators[4].enabled = true;
                }
                else
                {
                    scoreAnimators[4].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (paperScore3.enabled == true)
                {
                    scoreAnimators[5].SetTrigger("Start");
                }
            }
        }
        if (paperWins == 3)
        {
            if (paperScore3.enabled == false)
            {
                if (scoreAnimators[5].enabled == false)
                {
                    scoreAnimators[5].enabled = true;
                }             
            }           
        }
        if (scissorsWins == 0)
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (scissorsScore1.enabled == true)
                {
                    scoreAnimators[6].SetTrigger("Start");
                }
            }
        }
        if (scissorsWins == 1)
        {
            if (scissorsScore1.enabled == false)
            {
                if (scoreAnimators[6].enabled == false)
                {
                    scoreAnimators[6].enabled = true;
                }
                else
                {
                    scoreAnimators[6].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (scissorsScore2.enabled == true)
                {
                    scoreAnimators[7].SetTrigger("Start");
                }
            }
        }
        if (scissorsWins == 2)
        {
            if (scissorsScore2.enabled == false)
            {
                if (scoreAnimators[7].enabled == false)
                {
                    scoreAnimators[7].enabled = true;
                }
                else
                {
                    scoreAnimators[7].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (scissorsScore3.enabled == true)
                {
                    scoreAnimators[8].SetTrigger("Start");
                }
            }
        }
        if (scissorsWins == 3)
        {
            if (scissorsScore3.enabled == false)
            {
                if (scoreAnimators[8].enabled == false)
                {
                    scoreAnimators[8].enabled = true;
                }
            }
        }

        //opponent score
        if (oppRockWins == 0)
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (opprockScore1.enabled == true)
                {
                    oppscoreAnimators[0].SetTrigger("Start");
                }
            }
        }
        if (oppRockWins == 1)
        {
            if (opprockScore1.enabled == false)
            {
                if (oppscoreAnimators[0].enabled == false)
                {
                    oppscoreAnimators[0].enabled = true;
                }
                else
                {
                    oppscoreAnimators[0].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (opprockScore2.enabled == true)
                {
                    oppscoreAnimators[1].SetTrigger("Start");
                }
            }
        }
        if (oppRockWins == 2)
        {
            if (opprockScore2.enabled == false)
            {
                if (oppscoreAnimators[1].enabled == false)
                {
                    oppscoreAnimators[1].enabled = true;
                }
                else
                {
                    oppscoreAnimators[1].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (opprockScore3.enabled == true)
                {
                    oppscoreAnimators[2].SetTrigger("Start");
                }
            }
        }

        if (oppRockWins == 3)
        {
            if (opprockScore3.enabled == false)
            {
                if (oppscoreAnimators[2].enabled == false)
                {
                    oppscoreAnimators[2].enabled = true;
                }

            }

        }
        if (oppPaperWins == 0)
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (opppaperScore1.enabled == true)
                {
                    oppscoreAnimators[3].SetTrigger("Start");
                }
            }
        }
        if (oppPaperWins == 1)
        {
            if (opppaperScore1.enabled == false)
            {
                if (oppscoreAnimators[3].enabled == false)
                {
                    oppscoreAnimators[3].enabled = true;
                }
                else
                {
                    oppscoreAnimators[3].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (opppaperScore2.enabled == true)
                {
                    oppscoreAnimators[4].SetTrigger("Start");
                }
            }
        }
        if (oppPaperWins == 2)
        {
            if (opppaperScore2.enabled == false)
            {
                if (oppscoreAnimators[4].enabled == false)
                {
                    oppscoreAnimators[4].enabled = true;
                }
                else
                {
                    oppscoreAnimators[4].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (opppaperScore3.enabled == true)
                {
                    oppscoreAnimators[5].SetTrigger("Start");
                }
            }
        }
        if (oppPaperWins == 3)
        {
            if (opppaperScore3.enabled == false)
            {
                if (oppscoreAnimators[5].enabled == false)
                {
                    oppscoreAnimators[5].enabled = true;
                }
            }
        }
        if (oppScissorsWins == 0)
        {
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (oppscissorsScore1.enabled == true)
                {
                    oppscoreAnimators[6].SetTrigger("Start");
                }
            }
        }
        if (oppScissorsWins == 1)
        {
            if (oppscissorsScore1.enabled == false)
            {
                if (oppscoreAnimators[6].enabled == false)
                {
                    oppscoreAnimators[6].enabled = true;
                }
                else
                {
                    oppscoreAnimators[6].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (oppscissorsScore2.enabled == true)
                {
                    oppscoreAnimators[7].SetTrigger("Start");
                }
            }
        }
        if (oppScissorsWins == 2)
        {
            if (oppscissorsScore2.enabled == false)
            {
                if (oppscoreAnimators[7].enabled == false)
                {
                    oppscoreAnimators[7].enabled = true;
                }
                else
                {
                    oppscoreAnimators[7].Play("Fade1");
                }
            }
            if (PlayerPrefs.GetInt("GameMode") == 0)
            {
                if (oppscissorsScore3.enabled == true)
                {
                    oppscoreAnimators[8].SetTrigger("Start");
                }
            }
        }
        if (oppScissorsWins == 3)
        {
            if (oppscissorsScore3.enabled == false)
            {
                if (oppscoreAnimators[8].enabled == false)
                {
                    oppscoreAnimators[8].enabled = true;
                }
            }
        }
    }

    //Method that update the image depending on your choice, and plays the animation and stuff
    private void updateImageChoice()
    {
             
        Sprite rockSprite = Resources.Load<Sprite>("Asset 10");
        Sprite paperSprite = Resources.Load<Sprite>("Asset 1");
        Sprite scissorsSprite = Resources.Load<Sprite>("Saw");

        //player 1 stuff
        if (playerChoice == "rock")
        {
            choiceImage.SetActive(true);
            choiceImageFade.Play("Crossfadechoiceobject");
            choiceImage.GetComponent<Image>().sprite = rockSprite;
            rockSound();
        }
        if (playerChoice == "paper")
        {
            choiceImage.SetActive(true);
            choiceImageFade.Play("Crossfadechoiceobject");
            choiceImage.GetComponent<Image>().sprite = paperSprite;
            paperSound();
            
        }
        if (playerChoice == "scissors")
        {
            choiceImage.SetActive(true);
            choiceImageFade.Play("Crossfadechoiceobject");
            choiceImage.GetComponent<Image>().sprite = scissorsSprite;
            scissorsSound();
        }

        Invoke("updateOppImageChoice", 1.2f);

    }

    //Method that update the image depending on opponent choice, and plays the animation and stuff
    private void updateOppImageChoice()
    {

        Sprite rockSprite = Resources.Load<Sprite>("Asset 10");
        Sprite paperSprite = Resources.Load<Sprite>("Asset 1");
        Sprite scissorsSprite = Resources.Load<Sprite>("Saw");
       
        //AI stuff

        if (opponentChoice == "rock")
        {
            oppChoiceImage.SetActive(true);
            oppchoiceImageFade.Play("Crossfadechoiceobject");
            oppChoiceImage.GetComponent<Image>().sprite = rockSprite;
            rockSound();

        }
        if (opponentChoice == "paper")
        {
            oppChoiceImage.SetActive(true);
            oppchoiceImageFade.Play("Crossfadechoiceobject");
            oppChoiceImage.GetComponent<Image>().sprite = paperSprite;
            paperSound();

        }
        if (opponentChoice == "scissors")
        {
            oppChoiceImage.SetActive(true);
            oppchoiceImageFade.Play("Crossfadechoiceobject");
            oppChoiceImage.GetComponent<Image>().sprite = scissorsSprite;
            scissorsSound();
        }
        Invoke("updateTextBtwnRounds", 1.2f);
        Invoke("updateScore", 1.2f);
    }

    //Method that update the text depeding on circomstances, and play the text animation
    private void updateTextBtwnRounds()
    {    
        if (playerWon == "Draw")
        {
            betweenRoundsText.text = "It's a draw!";
            Sfx.Instance.PlaySound(roundDrawSound);
        }
        if (playerWon == "Lost")
        {        
                betweenRoundsText.text = "You lost this round";
                Sfx.Instance.PlaySound(roundLostSound);      
        }
        if (playerWon == "Win")
        {                      
                betweenRoundsText.text = "You won this round";
                Sfx.Instance.PlaySound(roundWinSound);          
        }

        betweenRoundsTextGO.SetActive(true);
        tmpFade.Play("CrossfadeText");      
    }

    private void updateGameEndText()
    {      
        if (playerWon == "Lost")
        {           
            Sfx.Instance.PlaySound(defeatSound);
            defeatCanvas.SetActive(true);
        }
        if (playerWon == "Win")
        {            
            Sfx.Instance.PlaySound(victorySound);
            victoryCanvas.SetActive(true);
        }
    }

    //Method that play sound effects, I probably made it just so that I can invoke it
    private void sfxGameOver()
    {
        if (playerWon == "Win")
        {
            Sfx.Instance.PlaySound(victorySound);          
        }

        if (playerWon == "Lost")
        {
            Sfx.Instance.PlaySound(defeatSound);           
        }
    }

    public void rockBttnHoverSound()
    {
        if (bttnGroup.interactable == true)
        {
            Sfx.Instance.PlaySound(rockSfx);
        }
    }

    public void paperBttnHoverSound()
    {
        if (bttnGroup.interactable == true)
        {
            Sfx.Instance.PlaySound(paperSfx);
        }
    }

    public void scissorsBttnHoverSound()
    {
        if (bttnGroup.interactable == true)
        {
            Sfx.Instance.PlaySound(scissorsSfx);
        }
    }


    private void rockSound()
    {              
            Sfx.Instance.PlaySound(rockSfx);       
    }
    private void paperSound()
    {        
            Sfx.Instance.PlaySound(paperSfx);       
    }
    private void scissorsSound()
    {            
            Sfx.Instance.PlaySound(scissorsSfx);        
    }
}

