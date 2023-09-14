using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;

    public AudioSource audioSource;
    public AudioClip[] soundtracks;
    private int currentTrack = 0;
    private int previousTrack = 0;
 
    private void Awake()
    {
        //Check if there is already a MusicPlayer object in the scene
        MusicPlayer[] musicPlayers = FindObjectsOfType<MusicPlayer>();
        if (musicPlayers.Length > 1)
        {
            //If there is already a MusicPlayer, destroy this object
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //If there isn't a MusicPlayer, set this object to not be destroyed when loading a new scene
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundtracks[currentTrack];
            audioSource.playOnAwake = false;
            audioSource.Play();


            
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            //Randomly select the next soundtrack from the array
            int nextSoundtrack = UnityEngine.Random.Range(0, soundtracks.Length);

            while (nextSoundtrack == previousTrack)
            {
                nextSoundtrack = UnityEngine.Random.Range(0, soundtracks.Length);
            }
            
            previousTrack = nextSoundtrack;
            
            //Set the next soundtrack as the background music
            audioSource.clip = soundtracks[nextSoundtrack];
            //Start playing the new background music
            audioSource.Play();
        }
    }
}
