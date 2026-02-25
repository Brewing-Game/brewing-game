using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This component swaps the material of highlightedObject with highlightMaterial
// when mouse enters collision with this.gameObject. 

[RequireComponent(typeof(Collider))]
public class HoverHighlight : MonoBehaviour
{

    public Material highlightMaterial;      // Material applied when highlighting.
    public GameObject highlightedObject;    // The object with a Renderer component.

    private Material _fallbackMaterial;     // The original Material.
    private Renderer _renderer;             // The component that stores the Material.

    public void OnPointerEnter(PointerEventData eventData)
    {
        Highlight();
    }

    public void Highlight()
    {
        if (_renderer == null || highlightMaterial == null) return;
        var selectHighlight = GetComponent<SelectHighlight>();
        if (selectHighlight != null && selectHighlight.IsSelected) return;
        
        _renderer.material = highlightMaterial;        
    }

    public void RemoveHighlight()
    {
        if (_renderer == null || highlightMaterial == null) return;
        var selectHighlight = GetComponent<SelectHighlight>();
        if (selectHighlight != null && selectHighlight.IsSelected) return;
        _renderer.material = _fallbackMaterial;
    }
    void Awake()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        if(!_renderer)
        {
            Debug.LogWarning("HoverHighlight: Renderer not found in highlightedObject.");
        }      
        _fallbackMaterial = _renderer != null ? _renderer.material : null;         
    }

    void Start()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        if (!_renderer) Debug.LogWarning("HoverHighlight.cs: Renderer not found in highlightedObject.");

        _fallbackMaterial = _renderer.material;
        if (!_fallbackMaterial) Debug.LogWarning("HoverHighlight.cs: highlightedObject.Renderer has no material.");
    }
}