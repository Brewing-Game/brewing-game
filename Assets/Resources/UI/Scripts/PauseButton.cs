using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class PauseButton : MonoBehaviour
{
    public GameObject PauseMenu;
    public Sprite ResumeIcon;

    private Sprite _fallbackIcon;
    private Image _imageComponent;

    public void ToggleMenu() 
    { 
        PauseMenu.SetActive(!PauseMenu.activeSelf); 
        if (PauseMenu.activeSelf) _imageComponent.sprite = ResumeIcon;
    }
    void Start() 
    {
        if (!PauseMenu) Debug.LogWarning("PauseButton.cs: PauseMenu:GameObject is null."); 
        _imageComponent = gameObject.GetComponent<Image>();
        _fallbackIcon = _imageComponent.sprite;
    }
    
    void Update()
    {
        if (!PauseMenu.activeSelf) _imageComponent.sprite = _fallbackIcon;
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleMenu();
    }
}