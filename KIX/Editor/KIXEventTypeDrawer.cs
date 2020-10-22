/**
 *  KIX EventType Custom Drawer
 *  Draws a custom eventtype drawer for the KIX eventsystem.
 * 
 *  @author : Robin Kollau
 *  @version: 1.0.0
 *  @date   : 01 March 2020  
 * 
 */
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(KIXDefinedType))]
public class KIXEventTypeDrawer : PropertyDrawer
{
    public int c_e = 0;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var data         = new string[] { };
        bool has_data    = false;
        var targetObject = property.serializedObject.targetObject;
        var targetObjectClassType = targetObject.GetType();
        var fields = targetObjectClassType.GetFields();
        foreach( FieldInfo field in fields){
            if (field.FieldType == typeof(KIXScriptableEventType)){
                KIXScriptableEventType value = (KIXScriptableEventType)field.GetValue(targetObject);
                if (value != null) {
                    int ci = 0;
                    data = new string[value.Types.Length * 2];
                    for (int i = 0; i < value.Types.Length; ++i) {data[ci++] = value.Types[i].ID;}
                    for (int i = 0; i < value.Types.Length; ++i) { data[ci++] = "!"+value.Types[i].ID; }
                }
            }
            else if (field.FieldType == typeof(KIXScriptableEventType[])){
                KIXScriptableEventType[] values = (KIXScriptableEventType[])field.GetValue(targetObject);
                if( values != null){
                    int size = 0;
                    foreach (var typeDef in values){
                        if (typeDef == null) continue;
                        size += typeDef.Types.Length;}
                    data = new string[size*2]; int c = 0;
                    foreach (var typeDef in values){
                        if (typeDef == null) continue;
                        for (int i = 0; i < typeDef.Types.Length; ++i) { data[c++] = typeDef.Types[i].ID; }
                        for (int i = 0; i < typeDef.Types.Length; ++i) { data[c++] = "!"+typeDef.Types[i].ID; }
                    }
                }
            }
        }
        has_data = (data.Length > 0);
        c_e = 0; var fieldType = targetObjectClassType.GetField(property.propertyPath);
        if (fieldType != null){
            string val = (string)fieldType.GetValue(targetObject);
            for (int i = 0; i < data.Length; i++){if (data[i] == val){c_e = i;break;}}
        }
        EditorGUI.BeginProperty(position, label, property);
        if(has_data){
            float half_width = (position.width / 2);
            float extra = half_width * 0.25f;
            Rect label_pos = new Rect(position.x, position.y, half_width - extra, position.height);
            Rect popup_pos = new Rect(position.x + half_width - extra, position.y, half_width + extra, position.height);

            EditorGUI.LabelField(label_pos, label);
            //EditorGUI.BeginChangeCheck();
            c_e = EditorGUI.Popup(popup_pos, c_e, data);
            //if (EditorGUI.EndChangeCheck()) {
            var field = targetObjectClassType.GetField(property.propertyPath);
            if (field != null){ field.SetValue(targetObject, data[c_e]); }//}
         }else{ EditorGUI.PropertyField(position, property); }
        EditorGUI.EndProperty();
        property.serializedObject.ApplyModifiedProperties();
      
    }

    

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float h = base.GetPropertyHeight(property, label);
        return h;

    }
}
