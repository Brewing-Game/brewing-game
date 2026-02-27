using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOnThisEnable : MonoBehaviour
{
    public GameObject objectToDisplay;
    void OnEnable()
    {
        if (!objectToDisplay) return;
        objectToDisplay = Instantiate(objectToDisplay);
        objectToDisplay.SetActive(true);
    }

    void OnDisable()
    {
        if (!objectToDisplay) return;
        Destroy(objectToDisplay);
    }
}
