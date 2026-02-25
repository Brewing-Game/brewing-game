using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MashTunSelector : MonoBehaviour
{
    public MashTunWindow mashTunWindow;
    public MashTun selectedMashTun;
    private MashTun _hoveredMashTun; //mashtun selector uniquely handles input logic
    [Header("Keyboard Navigation")]
    public List<MashTun> allTuns = new List<MashTun>();
    [Header("Tutorial")]
    public Slideshow tutorialSlideshow; //to track tutorial state
    
    void Start()
    {
        selectedMashTun = null;
        SetHovered(null);
        if(mashTunWindow) mashTunWindow.gameObject.SetActive(false);
    }

    public void CycleThroughTuns()
    {
        if(allTuns.Count == 0) return;

        if(selectedMashTun == null)
        {
            SelectMashTun(allTuns[0]);
        }
        else
        {
            int currentIndex = allTuns.IndexOf(selectedMashTun);
            int nextIndex = (currentIndex + 1) % allTuns.Count;
            SelectMashTun(allTuns[nextIndex]);
        }
    }

    public void SelectMashTun(MashTun tun)
    {
        if (selectedMashTun != null)
        {
            selectedMashTun.GetComponent<SelectHighlight>()?.Deselect();

            //not sure about this
            if (selectedMashTun == _hoveredMashTun)
                selectedMashTun.GetComponent<HoverHighlight>()?.Highlight();
        }       

        tun.GetComponent<SelectHighlight>()?.Select();
        selectedMashTun = tun;

        mashTunWindow.tun = tun;
        mashTunWindow.gameObject.SetActive(true);
        mashTunWindow.UpdateUI();

        Debug.Log($"Selected mashtun: {tun.gameObject.name}");
    }

    public void DeselectMashTun()
    {
        if (selectedMashTun != null)
        {
            selectedMashTun.GetComponent<SelectHighlight>()?.Deselect();

            //not sure either
            if (selectedMashTun == _hoveredMashTun)
                selectedMashTun.GetComponent<HoverHighlight>()?.Highlight();
        }

        selectedMashTun = null;
        mashTunWindow.gameObject.SetActive(false);
        Debug.Log($"Clicked away from tuns");
    }
    public void SetHovered(MashTun currentHover)
    {
        if(_hoveredMashTun == currentHover) return;
        //remove highlight from old hovered tun
        if(_hoveredMashTun != null && _hoveredMashTun != selectedMashTun)
            _hoveredMashTun.GetComponent<HoverHighlight>()?.RemoveHighlight();
        _hoveredMashTun = currentHover;
        //add highlight to current hovered tun
        if (_hoveredMashTun != null && _hoveredMashTun != selectedMashTun)
            _hoveredMashTun.GetComponent<HoverHighlight>()?.Highlight();
    }
    private bool IsTutorialBlocking()
    {
        return tutorialSlideshow != null && tutorialSlideshow.gameObject.activeInHierarchy;
    }

    private MashTun GetTargetTun()
    {
        if (!Camera.main) return null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {            
            return hit.collider.GetComponentInParent<MashTun>();
        }
        return null;
    }

    void Update()
    {
        //if tutorial is active don't detect mashtun inputs
        if (IsTutorialBlocking())
        {
            SetHovered(null);
            return;
        }
        //keyboard input tab cycling
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CycleThroughTuns();
        }


        var targetTun = GetTargetTun();
        SetHovered(targetTun);        

        //click input
        if(Input.GetMouseButtonDown(0))
        {              
            targetTun = GetTargetTun();

            if (targetTun != null)
            {
                SelectMashTun(targetTun);
                return; //ok, it's a mashtun
            }

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return; //ok, it's ui

            
            DeselectMashTun(); //no mashtun nor ui clicked
        }
    }    
}