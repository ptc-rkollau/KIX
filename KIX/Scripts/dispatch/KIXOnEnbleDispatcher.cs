/**
 *  KIX On Enable Dispatcher
 *  KIX Event dispatcher which fires when Unity
 *  calls 'OnEnable' on this MonoBehaviour.
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020  
 * 
 */
public class KIXOnEnbleDispatcher : KIXDispatcher
{
    //user defined event type to dispatch at OnEnable.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string EventType = "";
    
    /// <summary>
    /// On Enable
    /// Fired by Unity
    /// Fires a KIXEvent with type of EventType
    /// </summary>
    void OnEnable()
    {
        FireKIXEvent(new KIXEvent(EventType));
    }
}
