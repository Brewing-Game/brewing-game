using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This class allows us to group together multiple auto-sized TextMeshProUGUI objects.
// by removing differences 
public class TMP_GroupSizeController : MonoBehaviour
{
    public List<GameObject> textMeshes;
    private float _lowestFontSize = float.MaxValue;
    private float _lastHeight; // Used to detect changes in resolution
    private float _lastWidth;  // Increases efficiency

    private void FindLowestFontSize()
    {
        foreach (GameObject mesh in textMeshes)
        {
            if (mesh.GetComponent<TextMeshProUGUI>().fontSize < _lowestFontSize)
            {
                _lowestFontSize = mesh.GetComponent<TextMeshProUGUI>().fontSize;
            }
        }
    }

    private void ApplyLowestFontSize()
    {
        foreach(GameObject mesh in textMeshes)
        {
            mesh.GetComponent<TextMeshProUGUI>().fontSize = _lowestFontSize;
            mesh.GetComponent<TextMeshProUGUI>().enableAutoSizing = false;
        }
    }

    void Start()
    {
        _lastWidth = Screen.width;
        _lastHeight = Screen.height;
        StartCoroutine(SyncTextSize());
    }

    private IEnumerator SyncTextSize()
    {
        foreach(GameObject mesh in textMeshes)
        {
            mesh.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
        }

        yield return null;

        FindLowestFontSize();
        ApplyLowestFontSize();
    }

    void Update()
    {
        if (Screen.height != _lastHeight || Screen.width != _lastWidth)
        {
            StartCoroutine(SyncTextSize());
        }
    }
}