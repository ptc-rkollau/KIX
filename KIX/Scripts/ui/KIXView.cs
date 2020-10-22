/**
 *  KIX View
 *  KIX enabled UI view module.
 * 
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020  
 * 
 */
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("[ KIX ] /UI/KIX_View")]
public class KIXView : KIXListener
{
    //publics.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string SHOW_EventType = "";
    [KIXDefinedType] public string HIDE_EventType = "";

    public bool IsHiddenAtStart = false;
    [Space(10)]
    public GameObject[] attached_objects;



    //privates.
    private CanvasGroup _cnvsGroup;

    /// <summary>
    /// Awake
    /// Called By Unity
    /// Get variables.
    /// Sets visibility
    /// add listeners to KIX.
    /// </summary>
    void Awake()
    {
        _cnvsGroup = gameObject.AddComponent<CanvasGroup>();
        ToggleViewVisibility(!IsHiddenAtStart);

        AttachEvents(SHOW_EventType, OnKIXShowEvent);
        AttachEvents(HIDE_EventType, OnKIXHideEvent);
    }

    private void AttachEvents(string eventtype, Action<KIXEvent> eventHandler )
    {
        if (eventtype.StartsWith("!"))
        {
            string actualEvent = eventtype.Substring(1);
            foreach (TypeEntry entry in event_obj.Types)
            {
                if (entry.ID != actualEvent && entry.ID != "")
                {
                    AddEventListener(entry.ID, eventHandler);
                }
            }
        }
        else
        {
            AddEventListener(eventtype, eventHandler);
        }
    }

    private void OnKIXShowEvent(KIXEvent evt)
    {
       
        ToggleViewVisibility(true);
    }
    private void OnKIXHideEvent(KIXEvent evt)
    {
        ToggleViewVisibility(false);
    }

    /// <summary>
    /// Toggle View Visibility
    /// Shows or hides the visibility of this gameobject
    /// by setting the canvasgroup alpha 0 or 1, based on used input true / false
    /// </summary>
    /// <param name="visible">bool</param>
    private void ToggleViewVisibility( bool visible )
    {
        _cnvsGroup.alpha = visible ? 1.0f: 0.0f;
        _cnvsGroup.blocksRaycasts = visible;

        for (int i = 0; i < attached_objects.Length; ++i)
            attached_objects[i].SetActive(visible);
    }
}
