using System;
using UnityEditor;
using UnityEngine;

public class TW_WindowEditor : EditorWindow
{
    public GameObject prefab;
    [MenuItem("TWUnity/CreateAuthDataObject")]
    public static void CreateDataObject()
    {

        if (FindFirstObjectByType<TW_AuthDataHandler>()) return;

        GameObject prefab = Resources.Load<GameObject>("TW_AuthData");
        GameObject newobject = Instantiate(prefab);
        if (newobject != null)
        {
            Undo.RegisterCreatedObjectUndo(newobject, "CreateAuthObject");
            newobject.transform.position = Vector3.zero;
            Selection.activeObject = newobject;
        }

        Debug.Log("hola");
    }


}
