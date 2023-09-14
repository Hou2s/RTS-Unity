using UnityEngine;
using UnityEngine.SceneManagement;

public class Sfx : MonoBehaviour
{
    private static Sfx instance;
    public static Sfx Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Sfx>();
                if (instance == null)
                {
                    GameObject go = new GameObject("Sfx");
                    instance = go.AddComponent<Sfx>();
                }
            }
            return instance;
        }
    }

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }

        else
        {
            Destroy(gameObject);
        }       
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}


