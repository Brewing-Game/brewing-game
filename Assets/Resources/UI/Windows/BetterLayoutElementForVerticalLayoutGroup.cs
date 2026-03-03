using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// WAY better LayoutElement for VerticalLayoutGroup specifically.
// It keeps the element's scale constant, and size variable,
// regardless of the resolution... Unlike LayoutElement which uses
// one constant size expressed in pixels.
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Canvas))]
public class BetterLayoutElementForVerticalLayoutGroup : MonoBehaviour
{
    public float heightToWidthRation; // height is predetermined
    private RectTransform _rt;

    void Start()
    {
        _rt = GetComponent<RectTransform>();
        _rt.localScale = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {
        Vector2 availableSize = transform.parent.GetComponent<RectTransform>().sizeDelta; 
        float width = transform.parent.GetComponent<RectTransform>().rect.width;
        _rt.sizeDelta = new Vector2(width, width / heightToWidthRation);
    }
}
