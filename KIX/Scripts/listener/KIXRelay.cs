using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
