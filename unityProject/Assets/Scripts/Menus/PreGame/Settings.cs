using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] TMP_Dropdown textureDropdown;
    [SerializeField] TMP_Dropdown aaDropdown;
    [SerializeField] Slider volumeSlider;
    private float currentVolume;
    private Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "" + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        KeyBinds = new Dictionary<string, KeyCode>();
        CheckKeys();


    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,
                  resolution.height, Screen.fullScreen);
    }

    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        qualityDropdown.value = 6;
    }
    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        qualityDropdown.value = 6;
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6) // if the user is not using any of the preset
            QualitySettings.SetQualityLevel(qualityIndex);

        // Si on decommente ca en dessous le jeu crash
        /*switch (qualityIndex)
        {
            case 0: // quality level - very low
                textureDropdown.value = 3;
                aaDropdown.value = 0;
                break;
            case 1: // quality level - low
                textureDropdown.value = 2;
                aaDropdown.value = 0;
                break;
            case 2: // quality level - medium
                textureDropdown.value = 1;
                aaDropdown.value = 0;
                break;
            case 3: // quality level - high
                textureDropdown.value = 0;
                aaDropdown.value = 0;
                break;
            case 4: // quality level - very high
                textureDropdown.value = 0;
                aaDropdown.value = 1;
                break;
            case 5: // quality level - ultra
                textureDropdown.value = 0;
                aaDropdown.value = 2;
                break;
        }*/

        qualityDropdown.value = qualityIndex;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference",
                   qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference",
                   resolutionDropdown.value);
        PlayerPrefs.SetInt("TextureQualityPreference",
                   textureDropdown.value);
        PlayerPrefs.SetInt("AntiAliasingPreference",
                   aaDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference",
                   Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference",
                   currentVolume);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value =
                         PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value =
                         PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("TextureQualityPreference"))
            textureDropdown.value =
                         PlayerPrefs.GetInt("TextureQualityPreference");
        else
            textureDropdown.value = 0;
        if (PlayerPrefs.HasKey("AntiAliasingPreference"))
            aaDropdown.value =
                         PlayerPrefs.GetInt("AntiAliasingPreference");
        else
            aaDropdown.value = 1;
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen =
            Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("VolumePreference"))
            volumeSlider.value =
                        PlayerPrefs.GetFloat("VolumePreference");
        else
            volumeSlider.value =
                        PlayerPrefs.GetFloat("VolumePreference");
    }

    public void CheckKeys()
    {
        if (!PlayerPrefs.HasKey("NegThrottle"))
        {
            PlayerPrefs.SetInt("NegThrottle", (int)KeyCode.LeftControl);
            KeyBinds.Add("NegThrottle", KeyCode.LeftControl);
        }
        else
        {
            KeyBinds["NegThrottle"] = (KeyCode)PlayerPrefs.GetInt("NegThrottle");
        }

        if (!PlayerPrefs.HasKey("PosThrottle"))
        {
            PlayerPrefs.SetInt("PosThrottle", (int)KeyCode.LeftShift);
            KeyBinds.Add("PosThrottle", KeyCode.LeftShift);
        }
        else
        {
            KeyBinds["PosThrottle"] = (KeyCode)PlayerPrefs.GetInt("PosThrottle");
        }

        if (!PlayerPrefs.HasKey("NegYaw"))
        {
            PlayerPrefs.SetInt("NegYaw", (int)KeyCode.Q);
            KeyBinds.Add("NegYaw", KeyCode.Q);
        }
        else
        {
            KeyBinds["NegYaw"] = (KeyCode)PlayerPrefs.GetInt("NegYaw");
        }
        if (!PlayerPrefs.HasKey("PosYaw"))
        {
            PlayerPrefs.SetInt("PosYaw", (int)KeyCode.D);
            KeyBinds.Add("PosYaw", KeyCode.D);
        }
        else
        {
            KeyBinds["PosYaw"] = (KeyCode)PlayerPrefs.GetInt("PosYaw");
        }

        if (!PlayerPrefs.HasKey("NegPitch"))
        {
            PlayerPrefs.SetInt("NegPitch", (int)KeyCode.S);
            KeyBinds.Add("NegPitch", KeyCode.S);
        }
        else
        {
            KeyBinds["NegPitch"] = (KeyCode)PlayerPrefs.GetInt("NegPitch");
        }
        if (!PlayerPrefs.HasKey("PosPitch"))
        {
            PlayerPrefs.SetInt("PosPitch", (int)KeyCode.Z);
            KeyBinds.Add("PosPitch", KeyCode.Z);
        }
        else
        {
            KeyBinds["PosPitch"] = (KeyCode)PlayerPrefs.GetInt("PosPitch");
        }

        if (!PlayerPrefs.HasKey("NegRoll"))
        {
            PlayerPrefs.SetInt("NegRoll", (int)KeyCode.A);
            KeyBinds.Add("NegRoll", KeyCode.A);
        }
        else
        {
            KeyBinds["NegRoll"] = (KeyCode)PlayerPrefs.GetInt("NegRoll");
        }
        if (!PlayerPrefs.HasKey("PosRoll"))
        {
            PlayerPrefs.SetInt("PosRoll", (int)KeyCode.E);
            KeyBinds.Add("PosRoll", KeyCode.E);
        }
        else
        {
            KeyBinds["PosRoll"] = (KeyCode)PlayerPrefs.GetInt("PosRoll");
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

    public void Reset()
    {

    }

    public void SaveControls()
    {
        foreach (var Key in KeyBinds)
        {
            PlayerPrefs.SetInt(Key.Key, (int)Key.Value);
        }
        CustomInput.RefreshKeys();
    }

    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                KeyBinds[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString();
                EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
                currentKey = null;
            }
        }
    }
}
