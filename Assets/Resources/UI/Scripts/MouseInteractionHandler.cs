using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class handles selection and highliting interactions.
// It is added to every MashTun so they can handle their state internally.

[RequireComponent(typeof(SelectionHighlight))]
[RequireComponent(typeof(HoverHighlight))]
public class MouseInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SelectionHighlight _selectComp;
    private HoverHighlight _hoverComp;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverComp.Highlight();
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        _hoverComp.RemoveHighlight(); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_selectComp.IsSelected) _selectComp.Select();
        else                         _selectComp.Deselect();
    }

    void Start()
    {
        _selectComp = gameObject.GetComponent<SelectionHighlight>();
        _hoverComp = gameObject.GetComponent<HoverHighlight>();
    }

    void Update()
    {
        if (!Camera.main) return;
        if (!Input.GetMouseButtonDown(0)) return;

        if (EventSystem.current.IsPointerOverGameObject()) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = LayerMask.GetMask("Factory");
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject != gameObject) _selectComp.Deselect();
            }   
            return;
        }
        _selectComp.Deselect();
    }   
}