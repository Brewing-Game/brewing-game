using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenericButton : MonoBehaviour
{
    public Button buttonComponent;
    public TextMeshProUGUI textComponent;

    void Start()
    {
        if (!buttonComponent) Debug.LogWarning("Warning! GenericButton does not have buttonComponent. Assign it in the editor.");
        if (!textComponent) Debug.LogWarning("Warning! GenericButton does not have textComponent. Assign it in the editor.");
        
    }

    void Update()
    {
        
    }
}
