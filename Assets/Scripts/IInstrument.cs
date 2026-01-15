using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInstrument
{
    public void OnMashTunFill();
    public void OnBrewing();
    public void OnCollectBeer();
    public GameObject GetUIElement();
    public void Install(MashTun tun);
    public void Uninstall();
}
