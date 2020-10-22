/**
 *  KIX Data Button
 *  KIX enabled button module that has the ability to send data.
 * 
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 20 March 2020 
 * 
 * 
 */
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("[ KIX ] /UI/KIX_DATA_Button")]
public class KIXDataButton : KIXDataDispatcher, IPointerDownHandler, IPointerUpHandler
{
    //publics.
    public KIXScriptableEventType event_obj;
    [KIXDefinedType] public string EventType;



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
            FireKIXEvent(new KIXEvent(EventType, GetData()));
    }



    //privates.
    private Vector2 oldPos;
    private float _dragThreshold = 10.0f;
}
