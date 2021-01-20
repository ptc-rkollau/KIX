/**
 *  KIX Relay
 *  KIX Event listener which triggers a defined event dispatch
 *  
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 October 2020  
 * 
 */

using UnityEngine;

[AddComponentMenu("[ KIX ] /Listeners/KIX_Relay")]
public class KIXRelay : MonoBehaviour
{
    public KIXScriptableEventType[] event_obj;
    [KIXDefinedType] public string TriggerEvent = "";
    [KIXDefinedType] public string DispatchEvent = "";


    // Start is called before the first frame update
    void Awake()
    {
        KIX.Instance.AddEventListener(TriggerEvent, (evt) => { KIX.Instance.FireEvent(new KIXEvent(DispatchEvent)); });
    }
}
