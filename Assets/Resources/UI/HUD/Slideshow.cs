using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class traverses through slides' children one by one,
// only keeping a single child active at any time.
// After reaching the last child, calling NextSlide() will cause
// this.gameObject to become disabled.
public class Slideshow : MonoBehaviour
{
    public bool enableSlideshow;
    public GameObject slides;
    private int _activeSlideIndex = 0;

    public void NextSlide()
    {
        if (slides.transform.childCount > _activeSlideIndex + 1)
        {
            _activeSlideIndex ++;
            return;
        }
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        if (!enableSlideshow) Hide();
    }

    void Update()
    {
        for (int i = 0; i < slides.transform.childCount; i ++)
        {
            if (i == _activeSlideIndex)
            {
                slides.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                slides.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}