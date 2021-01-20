#if KIX_VUFORIA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[AddComponentMenu("[ KIX ] /AR/Vuforia/KIX_Planefinder_mode_switcher")]
public class KIXPlaneFinderModeSwitcher : MonoBehaviour
{

    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string SwitchToInteractiveEvent = "";
    [KIXDefinedType] public string SwitchToAutomaticEvent = "";

    public PlaneFinderBehaviour _pfb;

    void Start()
    {
        if(SwitchToInteractiveEvent != "") KIX.Instance.AddEventListener(SwitchToInteractiveEvent, OnSwitchToInteractive);
        if(SwitchToAutomaticEvent!="")KIX.Instance.AddEventListener(SwitchToAutomaticEvent, OnSwitchToAutomatic);
    }

    private void OnSwitchToInteractive( KIXEvent evt )
    {
        SwitchToInteractive();
    }

    private void OnSwitchToAutomatic(KIXEvent evt)
    {
        SwitchToAutomatic();
    }

    public void SwitchToInteractive()
    {
        
       // _pfb.HitTestMode = HitTestMode.AUTOMATIC;
    }

    public void SwitchToAutomatic()
    {
       // _pfb.HitTestMode = HitTestMode.AUTOMATIC;
    }


    private void Update()
    {
        var a = _pfb.PlaneIndicator.GetComponent<AnchorBehaviour>().CurrentStatusInfo;
        Debug.Log( a.ToString());
    }
}
#endif