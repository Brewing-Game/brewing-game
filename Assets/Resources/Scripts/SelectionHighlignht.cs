using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SelectHighlight : MonoBehaviour
{
    public bool IsSelected { get; private set; }
    public Material selectedMaterial;
    public GameObject highlightedObject;

    private Material _fallbackMaterial;
    private Renderer _renderer;

    public void Select()
    {
        IsSelected = true;
        _renderer.material = selectedMaterial;
    }

    public void Deselect()
    {
        IsSelected = false;
        _renderer.material = _fallbackMaterial;
    }

    void Start()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        _fallbackMaterial = _renderer.material;
    }
}