/**
 *  KIX Vuforia Disabler
 *  Disables Vuforia after event has been received.
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020
 * 
 */
using UnityEngine;
using Vuforia;
[AddComponentMenu("[ KIX ] /AR/Vuforia/Vuforia_Disabler")]
public class KIXVuforiaDisabler : KIXListener
{
    //user defined event type to dispatch at OnEnable.
    public string EventType = "";

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
        VuforiaBehaviour.Instance.enabled = false;
    }
}
