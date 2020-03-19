using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public struct DataItem
{
    public string ID;
    public string Data;
}

public class KIXButton : KIXDispatcher, IPointerDownHandler, IPointerUpHandler
{
    public string EVENT_TYPE = "";
    public List<DataItem> data;

    private Vector2 oldPos;
    private float _dragThreshold = 10.0f;


    public void OnPointerDown(PointerEventData eventData)
    {
        oldPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float l = Vector2.SqrMagnitude( oldPos - eventData.position );
        if( l < _dragThreshold )
            FireKIXEvent( new KIXEvent( EVENT_TYPE, ConvertDataToKIXData() ));
    }



    private KIXData ConvertDataToKIXData()
    {
        KIXData result = new KIXData();
        if (data.Count == 1)
        {
            result.Value = data[0].Data;
        }
        else
        {
            result.Value = convertDataToDictionary();
        }
        return result;
    }
    private Dictionary<string, object> convertDataToDictionary()
    {
        var result = new Dictionary<string, object>();
        for(int i = 0; i < data.Count; ++i)
        {
            result[data[i].ID] = data[i].Data;
        }
        return result;
    }
}
