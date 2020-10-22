using UnityEngine;
using UnityEngine.Events;

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
