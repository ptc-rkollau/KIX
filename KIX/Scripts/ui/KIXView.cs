/**
 *  KIX View
 *  KIX enabled UI view module.
 * 
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020  
 * 
 */
using UnityEngine;

[AddComponentMenu("[ KIX ] /UI/KIX_View")]
public class KIXView : KIXListener
{
    //publics.
    public string EventType = "";
    public string Data_MATCH = "";
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
        AddEventListener(EventType, OnKIXEvent);
    }

    /// <summary>
    /// On KIX Event
    /// Listener for KIX user set event type.
    /// </summary>
    /// <param name="evt">KIXEvent</param>
    private void OnKIXEvent(KIXEvent evt)
    {
        if( evt.Type == EventType)
        {
            ToggleViewVisibility(((string)evt.Data.Value == Data_MATCH));
        }
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
