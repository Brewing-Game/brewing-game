using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;

// This class represents the user settings profile.
[Serializable]
public class SettingsProfile
{
    public int masterVolume = 0;
    public int resolution = 0;     // Stores the index of _resolutionOptions
    public int autoSaveDelay = 0;  // Stores the index of _autosaveOptions
}


// This class provides settings menu functionalities.
public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    public SettingsProfile defaultProfile;
    public Slider masterVolumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown autosaveDropdown;

    private string _settingsFilePath;
    private SettingsProfile _userProfile;
    private bool _isEnabled;

    private List<int> _autosaveOptions = new List<int>() {1, 5, 10, 15, 30, 60};
    private List<Vector2> _resolutionOptions = new List<Vector2>() {
        new Vector2(640f, 480f),
        new Vector2(800f, 600f),
        new Vector2(1024f,768f),
        new Vector2(1280f,800f),
        new Vector2(1440f,900f),
        new Vector2(1680f,1050f),
        new Vector2(1920f,1080f),
        new Vector2(1920f,1200f),
        new Vector2(2560f,1440f)};

    void Awake()
    {
        _settingsFilePath = Application.persistentDataPath + "Settings.json";
        if (File.Exists(_settingsFilePath)) 
        {
            Load();
        }
        else 
        {
            _userProfile = defaultProfile;
            Save();
        }
        
        PopulateResolutionOptions();
        PopulateAutosaveOptions();
        ApplyUserOptions();
    }

    // Update _userProfile with values from menu.
    void Update()
    {
        _isEnabled = gameObject.activeSelf;
        if (_isEnabled)
        {
            _userProfile.masterVolume = (int)masterVolumeSlider.value;
            _userProfile.resolution = resolutionDropdown.value;
            _userProfile.autoSaveDelay = autosaveDropdown.value;
        }
    }

    // Loads _userProfile from file
    public void Load()
    {
        _userProfile = JsonUtility.FromJson<SettingsProfile>(File.ReadAllText(_settingsFilePath));
    }

    // Saves _userProfile to file
    public void Save()
    {
        File.WriteAllText(_settingsFilePath, JsonUtility.ToJson(_userProfile));
        Debug.Log("Saved successfully!");
    }

    // Populates the resolutions dropdown box
    public void PopulateResolutionOptions()
    {
        foreach(Vector2 resolution in _resolutionOptions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData() {text=$"{resolution}"});
        }
    }

    // Populates the autosave dropdown box
    public void PopulateAutosaveOptions()
    {
        foreach (int delay in _autosaveOptions)
        {
            autosaveDropdown.options.Add(new TMP_Dropdown.OptionData() {text=$"{delay}"});
        }
    }

    public void ApplyUserOptions()
    {
        resolutionDropdown.value = _userProfile.resolution;
        autosaveDropdown.value = _userProfile.autoSaveDelay;
        masterVolumeSlider.value = _userProfile.masterVolume;
    }
}