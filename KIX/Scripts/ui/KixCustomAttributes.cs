
using UnityEngine;

public class OnChooseCall : PropertyAttribute
{
    public string methodName;
    public OnChooseCall(string methodNameNoArguments)
    {
        methodName = methodNameNoArguments;
    }
}


public class KIXDefinedType : PropertyAttribute
{
   // public string Type;
   
}

public class KIXOptionalDataFlag : PropertyAttribute
{

}