using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class KIXVuforiaPlaneFinderRecreator : MonoBehaviour
{
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string RecreateEvent = "";
    public HitTestEvent eventCallee;
    public GameObject PlaneIndicatorPrefab;
    public GameObject PlaneFinderOBJ;


    void Start()
    {
        KIX.Instance.AddEventListener(RecreateEvent, OnRecreate);
    }

    void OnRecreate( KIXEvent evt ) 
    {
        Destroy(PlaneFinderOBJ.transform.GetComponent<PlaneFinderBehaviour>());

        var pfb = PlaneFinderOBJ.AddComponent<PlaneFinderBehaviour>();
        pfb.OnAutomaticHitTest = eventCallee;
        pfb.Height = 1.4f;
        pfb.PlaneIndicator = PlaneIndicatorPrefab;
        pfb.HitTestMode = HitTestMode.AUTOMATIC;
    }

}
