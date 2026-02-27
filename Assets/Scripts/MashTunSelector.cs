using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// This class is used to navigate the in-game world and its entities
// without the use of mouse.
// For now it only navigates through MashTuns, but in the future
// it'll encompass more entities.

public class TabNavigator : MonoBehaviour
{
    private GameObject[] _mashTuns;
    private int? _selectedIndex;

    void Start()
    {
        _mashTuns = GameObject.FindGameObjectsWithTag("MashTun");
    }

    private void GetSelectedTun()
    {
        for (int i = 0; i < _mashTuns.Length; i ++)
        {
            _selectedIndex = i;
            if (_mashTuns[i].GetComponent<SelectionHighlight>().IsSelected) return;
        }
        _selectedIndex = null;
    }

    public void SelectNextTun()
    {
        if (!_selectedIndex.HasValue) _selectedIndex = 0;
        else _selectedIndex ++;
        if (_selectedIndex.Value == _mashTuns.Length) _selectedIndex = 0;


        for (int i = 0; i < _mashTuns.Length; i ++)
        {
            if (i == _selectedIndex.Value)
            {
                _mashTuns[i].GetComponent<SelectionHighlight>().Select();
                continue;
            }
            _mashTuns[i].GetComponent<SelectionHighlight>().Deselect();
        }
        
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Tab)) SelectNextTun();
    } 
}