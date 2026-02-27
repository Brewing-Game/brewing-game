using UnityEngine;

// This class provides GameObjects with the ability to apply and remove highlight.
// This is done by swapping the material on the GameObject's Renderer.
// This is similar to HoverHighlight. The only difference is the application.
// Objects are SELECTED when clicked,
// but HIGHLIGHTED when on contact with cursor (without clicking)

[RequireComponent(typeof(Collider))]
public class SelectionHighlight : MonoBehaviour
{
    public bool IsSelected { get; private set; }
    public Material selectedMaterial;
    public GameObject highlightedObject;
    private Material _fallbackMaterial;
    private Renderer _renderer;

    public void Select()
    {
        IsSelected = true;
        if (_renderer && selectedMaterial) _renderer.material = selectedMaterial;
    }

    public void Deselect()
    {
        IsSelected = false;
        if (_fallbackMaterial) _renderer.material = _fallbackMaterial;
    }

    void Start()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        _fallbackMaterial = _renderer ? _renderer.material : null;
    }
}