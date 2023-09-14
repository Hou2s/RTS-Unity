using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    public LevelLoader levelLoader = new LevelLoader(); //Levelloader object for scene transitionning. 

    public GameObject loadingScreen;    //Screen that's active while player is connecting to server
    public TextMeshProUGUI statusText;  //Text that says stuff like "Connecting to servers" or whatever

    // Reference to an item that only appears when play gets disconnected (not manually disconnected)
    public GameObject disconnectedItem;
    public TextMeshProUGUI disconnectedText;

    private bool disconnected = false;   //Becomes true when player clicks on return to main menu button. To check if player manually disconnected or no.

    // Reference to the text fields in the scene
    public TMP_InputField createTextField;
    public TMP_InputField joinTextField;

    // Start is called before the first frame update
    void Start()
    {
        // Connect to the Photon cloud servers using the settings from the PhotonServerSettings asset
        PhotonNetwork.ConnectUsingSettings();

        // Show loading screen and status text
        loadingScreen.SetActive(true);
        statusText.text = "Connecting to server...";
    }

    public override void OnConnectedToMaster()
    {
        // Hide loading screen and status text
        loadingScreen.SetActive(false);
        statusText.text = "";
        Debug.Log("Connection successful!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (disconnected == false)
        {
            // Hide loading screen and show error message
            loadingScreen.SetActive(false);
            Debug.LogWarning("Disconnected from Photon servers: " + cause.ToString());
            disconnectedItem.SetActive(true);
            disconnectedText.text = "Disconnected from server: " + cause.ToString();

        }
        else
        {
            levelLoader.LoadMainMenu();
            statusText.text = "Disconnecting from server...";
            Debug.LogWarning("Disconnected from Photon servers: " + cause.ToString());
        }
    }

    // Handler for the Create Room button click
    public void OnCreateButtonClick()
    {
        
        string roomName = createTextField.text; // Get the room name from the create text field

        if (roomName.Length >= 3)
        {
        
        PhotonNetwork.CreateRoom(roomName); // Create a new room with the specified name
        }
        else
        {
            statusText.text = "Room name must be at least 3 characters";
        }
    }

    // Handler for the Join Room button click
    public void OnJoinButtonClick()
    {
        
        string roomName = joinTextField.text; // Get the room name from the join text field

        if (roomName.Length >= 3)
        {

            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            statusText.text = "Room name must be at least 3 characters";
        }
    }

    // Callback function for when a room is successfully created
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
    }

    // Callback function for when a room join attempt succeeds
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
    }

    // Callback function for when a room create attempt fails
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create room: " + message);
    }

    // Callback function for when a room join attempt fails
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to join room: " + message);
    }


    //Return to main menu button click
    public void returnToMainMenu()
    {
        PhotonNetwork.Disconnect(); // Disconnect from the Photon server      
        disconnected = true;
    }
   
}
