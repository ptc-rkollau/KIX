#if KIX_VUFORIA
using UnityEngine;
using Vuforia;

public class KIXVuforiaTrackerManager : MonoBehaviour
{
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string TrackerResetEvt = "";


    private PositionalDeviceTracker _positionalDeviceTracker;

    // Start is called before the first frame update
    void Start()
    {
        DeviceTrackerARController.Instance.RegisterTrackerStartedCallback(OnTrackerStarted);
        DeviceTrackerARController.Instance.RegisterDevicePoseStatusChangedCallback(OnDevicePoseStatusChanged);

        //KIX.Instance.AddEventListener(TrackerResetEvt, OnTrackerReset);
    }

    void OnTrackerStarted()
    {
        
    }

    void OnTrackerReset(KIXEvent evt)
    {
        _positionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
        _positionalDeviceTracker?.Reset();
    }



    void OnDevicePoseStatusChanged(TrackableBehaviour.Status status, TrackableBehaviour.StatusInfo statusInfo)
    {
        switch (statusInfo)
        {
            case TrackableBehaviour.StatusInfo.NORMAL:
            case TrackableBehaviour.StatusInfo.UNKNOWN:
            case TrackableBehaviour.StatusInfo.INITIALIZING:
            case TrackableBehaviour.StatusInfo.EXCESSIVE_MOTION:
            case TrackableBehaviour.StatusInfo.INSUFFICIENT_FEATURES:
            case TrackableBehaviour.StatusInfo.INSUFFICIENT_LIGHT:
            case TrackableBehaviour.StatusInfo.RELOCALIZING:
                break;
            default:
                break;
        }
    }

}
#endif