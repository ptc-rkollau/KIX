/**
 *  KIX Timed Dispatcher
 *  KIX Event dispatcher which fires after a user defined delay.
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020  
 * 
 */
using UnityEngine;
[AddComponentMenu("[ KIX] /Dispatchers/KIX_Timed_Dispatcher")]
public class KIXTimedDispatcher : KIXDataDispatcher
{
    //user defined event type to dispatch at Start.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string EventType = "";

    //delay in ms after which user defined event type will fire.
    public int delayInMS     = 3000;

    /// <summary>
    /// Start
    /// Fired by Unity
    /// Delayed Fires a KIXEvent with type of EventType
    /// </summary>
    void Start()
    {
        FireDelayedKIXEvent( new KIXEvent(EventType, GetData()), delayInMS );
    }
}
