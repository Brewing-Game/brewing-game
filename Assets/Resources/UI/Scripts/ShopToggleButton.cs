using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is attached to the Shop button in the top-right corner.
// It sets the Shop's activeSelf to the reverse of its current state.
[RequireComponent(typeof(Button))]
public class ShopToggleButton : MonoBehaviour
{
    public GameObject shopObject;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleShop);
    }

    public void ToggleShop()
    {
        shopObject.SetActive(!shopObject.activeSelf);
    }
}