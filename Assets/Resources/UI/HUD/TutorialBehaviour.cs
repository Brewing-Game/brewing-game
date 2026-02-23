using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    public GameObject slides;
    public GameObject holes;

    private GameObject _activeSlide;
    private GameObject _activeHole;
    //private GameObject slide;
    //private GameObject[] holes;
    //private GameObject activeHole;
//

    void NextSlide()
    {

        foreach(Transform child in holes.transform)
        {
            if (child.name == _activeSlide.name)
            {
                if (_activeHole) _activeHole.SetActive(false);
                
                _activeHole = child.gameObject;
                child.SetActive(true);
            }
        }



        //foreach(Transform child in slides.transform)
        //{
        //    if (child.name == )
        //}
        //foreach(GameObject holeSlide in holes)
        //{
        //    if (holeSlide.name == slide.name)
        //    {
        //        activeHole.SetActive(false);
        //        activeHole = holeSlide;
        //        activeHole.SetActive(true);
        //    }
        //}
    }

    void Start()
    {

        
    }

    void Update()
    {
        
    }

    void OnMouseButtonDown()
    {
        
    }
}
