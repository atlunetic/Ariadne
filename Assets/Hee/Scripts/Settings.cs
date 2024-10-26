using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPanel;
    public AudioMixer masterMixer;
    public Slider audioSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    int [,] resolutionList = new int[,] { {1280, 720}, {1920, 1080}, {2560, 1440}, {3840, 2160}};
    public static Settings instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }
    void Start()
    { 
        resolutionDropdown.options.Clear();

        Resolution maxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
        for(int i =0; i<resolutionList.GetLength(0); i++)
        {
            if(resolutionList[i,0] > maxResolution.width ||
               resolutionList[i,1] > maxResolution.height) break;

            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = resolutionList[i,0] + " X " + resolutionList[i,1];
            resolutionDropdown.options.Add(option);
        }
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = resolutionDropdown.options.Count-1;
        DropBoxOptionChange(resolutionDropdown.value);

        fullScreenToggle.isOn = Screen.fullScreen;
    }

    public void DropBoxOptionChange(int x){
        Screen.SetResolution(resolutionList[x,0], 
                                resolutionList[x,1], 
                                Screen.fullScreenMode);
    }

    public void ToggleFullScreenMode(bool isOn){
        Screen.fullScreen = isOn;
    }

    public void AudioControl(){
        float sound = audioSlider.value;

        AudioListener.volume = sound;

        // if(sound == -30f) masterMixer.SetFloat("Master", -80);
        // else masterMixer.SetFloat("Master", sound);
    }

    public void ToggleAudioVolume(){
        AudioListener.volume = (AudioListener.volume == 0) ? 1 : 0;
    }

    public void OpenSaveAndLoad(){
        SaveAndLoad.instance.SavePanel.SetActive(true);
        DeactiveSettingsPanel();
    }
    
    public void DeactiveSettingsPanel(){
        SettingsPanel.SetActive(false);
    }
}
