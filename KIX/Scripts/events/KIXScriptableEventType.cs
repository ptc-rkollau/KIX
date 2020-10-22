using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TypeEntry
{
    public string ID;
    public int UID { get { return KIXScriptableEventType.TypeCounter; } set { } }
    public TypeEntry()
    {
        UID = KIXScriptableEventType.TypeCounter;
        KIXScriptableEventType.TypeCounter++;
    }
}


[CreateAssetMenu(fileName = "Data", menuName = "[ KIX ]/KIXScriptableEventType", order = 1)]
public class KIXScriptableEventType : ScriptableObject
{
    public static int TypeCounter;
    public TypeEntry[] Types;
}

