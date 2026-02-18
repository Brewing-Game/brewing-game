using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MashTunSelector : MonoBehaviour
{
    public MashTunWindow mashTunWindow;
    public MashTun selectedMashTun;

    [Header("Keyboard Navigation")]
    public List<MashTun> allTuns = new List<MashTun>();

    public void CycleThroughTuns()
    {
        if(allTuns.Count == 0) return;

        if(selectedMashTun == null)
        {
            SelectMashTun(allTuns[0]);
        }
        else
        {
            int currentIndex = allTuns.IndexOf(selectedMashTun);
            int nextIndex = (currentIndex + 1) % allTuns.Count;
            SelectMashTun(allTuns[nextIndex]);
        }
    }

    public void SelectMashTun(MashTun tun)
    {
        if (selectedMashTun != null)
            selectedMashTun.GetComponent<SelectHighlight>()?.Deselect();

        selectedMashTun = tun;
        tun.GetComponent<SelectHighlight>()?.Select();

        mashTunWindow.tun = tun;
        mashTunWindow.gameObject.SetActive(true);
        mashTunWindow.UpdateUI();

        Debug.Log($"Selected mashtun: {tun.gameObject.name}");
    }

    public void DeselectMashTun()
    {
        if (selectedMashTun != null)
            selectedMashTun.GetComponent<SelectHighlight>()?.Deselect();

        selectedMashTun = null;
        mashTunWindow.gameObject.SetActive(false);
        Debug.Log($"Clicked away from tuns");
    }
    // Start is called before the first frame update
    void Start()
    {
        selectedMashTun = null;
        mashTunWindow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CycleThroughTuns();
        }

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
                else
                {
                    DeselectMashTun();
                }
            }
            else
            {
                DeselectMashTun();
            }
        }
    }
}
