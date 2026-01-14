using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This component swaps the material of highlightedObject with highlightMaterial
// when mouse enters collision with this.gameObject. 

[RequireComponent(typeof(Collider))]
public class HoverHighlight : MonoBehaviour
{

    public Material highlightMaterial;      // Material applied when highlighting.
    public GameObject highlightedObject;    // The object with a Renderer component.

    private Material _fallbackMaterial;     // The original Material.
    private Renderer _renderer;             // The component that stores the Material.

    void OnMouseEnter()
    {
        _renderer.material = highlightMaterial;
    }

    void OnMouseExit()
    {
        _renderer.material = _fallbackMaterial;
    }

    void Start()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        if (!_renderer) Debug.LogWarning("HoverHighlight.cs: Renderer not found in highlightedObject.");

        _fallbackMaterial = _renderer.material;
        if (!_fallbackMaterial) Debug.LogWarning("HoverHighlight.cs: highlightedObject.Renderer has no material.");
    }
}