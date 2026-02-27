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
        if (_renderer != null && selectedMaterial != null)
            _renderer.material = selectedMaterial;
    }

    public void Deselect()
    {
        IsSelected = false;
        if (_renderer != null && _fallbackMaterial != null)
            _renderer.material = _fallbackMaterial;
    }

    void Awake()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        if(_renderer == null) Debug.LogWarning("SelectHighlight: Renderer missing");
        _fallbackMaterial = _renderer ? _renderer.material : null;
    }

    void Start()
    {
        _renderer = highlightedObject.GetComponent<Renderer>();
        _fallbackMaterial = _renderer.material;
    }
}