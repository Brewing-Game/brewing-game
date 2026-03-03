using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script simply highlights mashTuns during a single slide in the tutorial.
public class TutorialSlideHighlightTuns : MonoBehaviour
{
    public GameObject[] mashTuns;
    void OnEnable()
    {
        foreach(GameObject tun in mashTuns)
        {
            tun.gameObject.GetComponent<HoverHighlight>().Highlight();
        }
    }

    void OnDisable()
    {
        foreach(GameObject tun in mashTuns)
        {
            tun.GetComponent<HoverHighlight>().RemoveHighlight();
        }
    }
}
