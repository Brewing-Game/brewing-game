using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashTunSelector : MonoBehaviour
{
    public MashTunWindow mashTunWindow;
    public MashTun selectedMashTun;

    public void SelectMashTun(MashTun tun)
    {
        selectedMashTun = tun;
        mashTunWindow.tun = tun;
        mashTunWindow.UpdateUI();

        Debug.Log($"Selected mashtun: {tun.gameObject.name}");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                MashTun clickedTun = hit.collider.GetComponent<MashTun>();
                if(clickedTun != null)
                {
                    SelectMashTun(clickedTun);
                }
            }
        }
    }
}
