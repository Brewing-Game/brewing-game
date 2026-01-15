using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingsMenu;

    // Start Button
    public void StartGame() {SceneManager.LoadScene(1);}
    

    // Settings Button



    // Quit Button
    public void QuitGame() 
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    public void ToggleMenus()
    {
        mainMenu.SetActive(settingsMenu.activeSelf);
        settingsMenu.SetActive(!mainMenu.activeSelf);
        //mainMenu.GetComponent<Canvas>().enabled = !settingsMenu.GetComponent<Canvas>().enabled;
        //settingsMenu.GetComponent<Canvas>().enabled = !mainMenu.GetComponent<Canvas>().enabled;
    }
}
