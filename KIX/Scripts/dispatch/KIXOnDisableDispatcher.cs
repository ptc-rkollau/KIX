/**
 *  KIX On Disable Dispatcher
 *  KIX Event dispatcher which fires when Unity
 *  calls 'OnDisable' on this MonoBehaviour.
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020  
 * 
 */
public class KIXOnDisableDispatcher : KIXDispatcher
{
    //user defined event type to dispatch at OnEnable.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string EventType = "";

    /// <summary>
    /// On Disable
    /// Fired by Unity
    /// Fires a KIXEvent with type of EventType
    /// </summary>
    void OnDisable()
    {
        FireKIXEvent(new KIXEvent(EventType));
    }
}
