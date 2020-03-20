/**
 *  KIX Vuforia Initializer
 *  Initializes Vuforia based on Delayed Initialisation.
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020
 * 
 */
using UnityEngine;
using Vuforia;
[AddComponentMenu("[ KIX ] /AR/Vuforia/Vuforia_Initializer")]
public class KIXVuforiaInitializer : KIXDataDispatcher
{
    //publics
    public string startEventType = "";
    public string initiatedEventType = "";

    /// <summary>
    /// Awake
    /// Called by Unity
    /// Disables Vuforia Behaviour
    /// Enables  Delayed Initialization
    /// Adds a Listener for the start event.
    /// </summary>
    void Awake()
    {
        VuforiaBehaviour.Instance.enabled = false;
        VuforiaConfiguration.Instance.Vuforia.DelayedInitialization = true;
        KIX.Instance.AddEventListener(startEventType, InitializeVuforia);
    }

    /// <summary>
    /// Initialize Vuforia
    /// Resgisters a handler to Vuforia Started event.
    /// Initis Vuforia.
    /// </summary>
    void InitializeVuforia( KIXEvent evt)
    {
        VuforiaBehaviour.Instance.enabled = true;
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaInitialized);
        Vuforia.VuforiaRuntime.Instance.InitVuforia();
    }

    /// <summary>
    /// On Vuforia Initialized
    /// Handler for completion of Vuforia Initialisation.
    /// </summary>
    private void OnVuforiaInitialized()
    {
        KIX.Instance.FireEvent(new KIXEvent(initiatedEventType, GetData()));
    }
}
