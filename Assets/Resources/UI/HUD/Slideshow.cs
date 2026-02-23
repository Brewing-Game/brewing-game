using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slideshow : MonoBehaviour
{
    public bool enableTutorial;
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
        if (!enableTutorial) Hide();
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