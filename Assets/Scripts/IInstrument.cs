using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This abstract class declares installable mash tun instruments.
// Instruments who inherit from this class must define the abstract functions
// with the 'override' keyword.
public abstract class Instrument : MonoBehaviour
{
    public abstract void OnMashTunFill();
    public abstract void OnBrewing();
    public abstract void OnCollectBeer();
    public abstract GameObject GetUIElement();
    public abstract void Install(MashTun tun);
    public abstract void Uninstall();
    public abstract void UpdateViewModel(MashTunViewModel viewModel, MashTun tun);
}