using System;
using UnityEngine;

[System.Serializable]
public class WatchOBJ
{
    public MonoBehaviour target;
    public string property_name;
    public string EventType;

}
public class KIXWatchEventType : KIXEventType
{
    public static readonly KIXEventType VALUE_CHANGED = new KIXEventType(180, "WATCHER_VALUE_CHANGED");
    public KIXWatchEventType(int key, string value) : base(key, value) { }
}

public class KIXWatcher : MonoBehaviour
{
    public bool isWatching = true;
    public bool isVerbose  = false;
    public WatchOBJ[] watchObjects;

    // Start is called before the first frame update
    void Start()
    {
        for( int i = 0; i < watchObjects.Length; ++i)
        {
            Type t = watchObjects[i].target.GetType();
            KIX.Instance.Watch(watchObjects[i].target, watchObjects[i].property_name, new KIXEvent(watchObjects[i].EventType));
            KIX.Instance.AddEventListener(watchObjects[i].EventType, OnValueChanged );
        }
    }

    private void OnValueChanged( KIXEvent evt )
    {
        KIXData data = evt.Data;
        KIX.Instance.FireEvent(new KIXEvent(KIXWatchEventType.VALUE_CHANGED, evt.Data));
        if (isVerbose)
        {
            KIXWatchObject o = (KIXWatchObject)data.Value;
            Debug.Log("OnValueChanged  - Name : " + o.target.GetType().Name + ", Property Name: " + o.property.Name + " , Value: " + o.value);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(isWatching) KIX.Instance.CheckWatch();
    }
}
