/**
 *  KIX Vuforia Disabler
 *  Disables Vuforia after event has been received.
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020
 * 
 */
#if KIX_VUFORIA
using UnityEngine;
using Vuforia;
[AddComponentMenu("[ KIX ] /AR/Vuforia/KIX_Vuforia_Disabler")]
public class KIXVuforiaDisabler : KIXListener
{
    //user defined event type to dispatch at OnEnable.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string EventType = "";

    /// <summary>
    /// Awake
    /// Called by Unity
    /// Adds a Listener for the start event.
    /// </summary>
    void Awake()
    {
        AddEventListener( EventType, DisableVuforia );
    }

    /// <summary>
    /// Disable Vuforia
    /// Disables Vuforia behaviour.
    /// </summary>
    /// <param name="evt"></param>
    private void DisableVuforia( KIXEvent evt )
    {
        PositionalDeviceTracker _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
        _deviceTracker?.Reset();
        VuforiaBehaviour.Instance.enabled = false;
    }
}
#endif