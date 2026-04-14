using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class essentially "connects" each mash tun to the
// MashTunWindow UI GameObject in accordance to the selection state.
public class MashTunUIConnector : MonoBehaviour
{
    private GameObject[] _mashTuns;
    public MashTunWindow mashTunWindow;
    private int? _selectedIndex = null;

    public void FetchAllTuns()
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

    private void DisplayWindowForSelectedTun()
    {
        if(_mashTuns[_selectedIndex.Value].GetComponent<MashTun>() != null && mashTunWindow != null)
        {
            mashTunWindow.tun = _mashTuns[_selectedIndex.Value].GetComponent<MashTun>();
            mashTunWindow.gameObject.SetActive(true);
            mashTunWindow.UpdateUI();
        }
    }

    void Start()
    {
        FetchAllTuns();
    }

    void Update()
    {
        GetSelectedTun();
        if (!_selectedIndex.HasValue) 
        {
            mashTunWindow.gameObject.SetActive(false);
            return;
        }
        DisplayWindowForSelectedTun();
    }
}