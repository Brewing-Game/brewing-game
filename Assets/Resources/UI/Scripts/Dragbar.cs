using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This class defines the functionality of a Dragbar GameObject.
// While the object is being clicked (LMB is held), draggedObject
// follows Input.mousePosition at an offset from its origin point.
public class Dragbar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject draggedObject;
    private bool _isHolding;
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _isHolding = true;
            offset =  Input.mousePosition - draggedObject.transform.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) _isHolding = false;
    }

    void Update()
    {
        if (_isHolding)
        {
            draggedObject.transform.position = Input.mousePosition - offset;
        }
    }
}