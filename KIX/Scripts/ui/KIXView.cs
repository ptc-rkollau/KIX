using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[AddComponentMenu("[ KIX] /UI/KIXView")]
public class KIXView : KIXListener
{
    public string EVENT_TYPE;
    public string Data_MATCH = "";
    public bool IsHiddenAtStart = false;

    private CanvasGroup _cnvsGroup;

    // Start is called before the first frame update
    void Awake()
    {
        _cnvsGroup = gameObject.AddComponent<CanvasGroup>();
        ToggleViewVisibility(!IsHiddenAtStart);
        AddEventListener(EVENT_TYPE, OnKIXEvent);
    }

    private void OnKIXEvent(KIXEvent evt)
    {
        if( evt.Type == EVENT_TYPE )
        {
            ToggleViewVisibility(((string)evt.Data.Value == Data_MATCH));
        }
    }

    private void ToggleViewVisibility( bool visible )
    {
        _cnvsGroup.alpha = visible ? 1.0f: 0.0f;
        _cnvsGroup.blocksRaycasts = visible;
    }
}
