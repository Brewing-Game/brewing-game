using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This scripts provides behaviours for all buttons in Main Menu.
public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    // Start Button
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    // Settings Button
    public void ToggleMenus()
    {
        mainMenu.SetActive(settingsMenu.activeSelf);
        settingsMenu.SetActive(!mainMenu.activeSelf);
    }

    // Quit Button
    public void QuitGame() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }    
}