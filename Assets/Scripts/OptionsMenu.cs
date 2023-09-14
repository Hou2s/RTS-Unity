using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle;

    public List<ResItem> resolutions = new List<ResItem>();

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private int selectedResolution;

    public TMP_Text resolutionLabel;


    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;


        //musicPlayer = MusicPlayer.instance;
        musicVolumeSlider.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxVolumeSlider.onValueChanged.AddListener(delegate { UpdateSfxVolume(); });
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1.0f);

        saveResolutionLabel();
    }

    void UpdateMusicVolume()
    {
        MusicPlayer.instance.audioSource.volume = musicVolumeSlider.value;       
        SaveMusicVolume();
    }
    void UpdateSfxVolume()
    {
        Sfx.Instance.SetVolume(sfxVolumeSlider.value);
        SaveSfxVolume();
    }

    void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }
    void SaveSfxVolume()
    {
        PlayerPrefs.SetFloat("SfxVolume", sfxVolumeSlider.value);
    }

   
    public void ResLeft()
    {
        selectedResolution--;
        if(selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();

        if(Screen.fullScreen == true)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenToggle.isOn);
    }

    public void saveResolutionLabel()
    {
        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;

            UpdateResLabel();
        }
    }

}
[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
