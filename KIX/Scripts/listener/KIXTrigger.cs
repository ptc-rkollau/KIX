/**
 *  KIX Trigger
 *  KIX Event listener which triggers a defined method
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 October 2020  
 * 
 */

using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("[ KIX ] /Listeners/KIX_Trigger")]
public class KIXTrigger : MonoBehaviour
{
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string TriggerEvent = "";

    public UnityEvent TriggerMethod;


    // Start is called before the first frame update
    void Start()
    {
        KIX.Instance.AddEventListener(TriggerEvent, (evt)=> { TriggerMethod.Invoke(); });
    }
}
