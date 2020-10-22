/**
 *  KIX Button
 *  KIX enabled button module.
 * 
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020 
 * 
 * 
 */
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("[ KIX ] /UI/KIX_Button")]
public class KIXButton : KIXDispatcher, IPointerDownHandler, IPointerUpHandler
{
    //publics.
    public KIXScriptableEventType[] event_obj;
    [KIXDefinedType] public string EventType;



    //privates.
    private Vector2 oldPos;
    private float _dragThreshold = 50.0f;


    /// <summary>
    /// On Pointer Down
    /// Called by Unity when the user pressed this button.
    /// </summary>
    /// <param name="eventData">PointerEventData</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        oldPos = eventData.position;
    }

    /// <summary>
    /// On Pointer Up
    /// Called by Unity when the user released this button.
    /// </summary>
    /// <param name="eventData">PointerEventData</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        float l = Vector2.SqrMagnitude(oldPos - eventData.position);
        if (l < _dragThreshold)
            FireKIXEvent(new KIXEvent(EventType));
    }
}
