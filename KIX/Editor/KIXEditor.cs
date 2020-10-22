using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KIXEditor : MonoBehaviour
{
    [MenuItem("[ KIX ]/Create/UI/View  &v")]
    static void CreateKIXView()
    {
        //create.
        var go  = new GameObject();
        var rt  = go.AddComponent<RectTransform>();
        go.AddComponent<CanvasRenderer>();
        go.AddComponent<Image>().color = Color.white;
        go.AddComponent<KIXView>();

        //size.
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        Vector2 Res = (Vector2)GetSizeOfMainGameView.Invoke(null, null);

        rt.sizeDelta      = new Vector2(Res.x, Res.y);
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;

        //canvas
        Canvas canvas = null;
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] list = scene.GetRootGameObjects();
        for( int i = 0; i < list.Length; ++i)
        if (list[i].GetComponent<Canvas>() != null)  canvas = list[i].GetComponent<Canvas>();
        
        GameObject target = Selection.activeGameObject ? Selection.activeGameObject : canvas ? canvas.gameObject : null;

        //naming.
        int count = 0;
        if(target)
        {
            for (int i = 0; i < target.transform.childCount; ++i)
            if (target.transform.GetChild(i).name.Contains("kx_view")) ++count;
            ++count;
        }
        go.name = "kx_view_" + count.ToString("D2");

        //position.
        if( target != null)
        {
            go.transform.SetParent(target.transform);
            rt.localPosition = Vector3.zero;
        }
        
    }

    [MenuItem("[ KIX ]/Create/UI/Button &b")]
    static void CreateKIXButton()
    {
        var go = new GameObject();
        var rt = go.AddComponent<RectTransform>();
        go.AddComponent<CanvasRenderer>();
        go.AddComponent<Image>().color = Color.grey;
        go.AddComponent<KIXButton>();

        rt.sizeDelta = new Vector2(200, 100);

        int count = 0;
        if (Selection.activeGameObject)
        {
            for (int i = 0; i < Selection.activeGameObject.transform.childCount; ++i)
            if (Selection.activeGameObject.transform.GetChild(i).name.Contains("kx_btn")) ++count;
            ++count;
        }
        go.name = "kx_btn_" + count.ToString("D2");
        if (Selection.activeGameObject != null)
        {
            go.transform.SetParent(Selection.activeGameObject.transform);
            rt.localPosition = Vector3.zero;
        }
    }
    [MenuItem("[ KIX ]/Create/UI/Data Button &#B")]
    static void CreateKIXDataButton()
    {
        var go = new GameObject();
        var rt = go.AddComponent<RectTransform>();
        go.AddComponent<CanvasRenderer>();
        go.AddComponent<Image>().color = Color.grey;
        go.AddComponent<KIXDataButton>();

        rt.sizeDelta = new Vector2(200, 100);

        int count = 0;
        if (Selection.activeGameObject)
        {
            for (int i = 0; i < Selection.activeGameObject.transform.childCount; ++i)
                if (Selection.activeGameObject.transform.GetChild(i).name.Contains("kx_dbtn")) ++count;
            ++count;
        }
        go.name = "kx_dbtn_" + count.ToString("D2");
        if (Selection.activeGameObject != null)
        {
            go.transform.SetParent(Selection.activeGameObject.transform);
            rt.localPosition = Vector3.zero;
        }
    }
}
