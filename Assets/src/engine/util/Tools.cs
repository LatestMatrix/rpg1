using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Tools
{
    static public GameObject Instance(GameObject prefab, GameObject parent = null)
    {
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (go != null && parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            go.layer = parent.layer;
        }
        return go;
    }

    static public void AddChild(GameObject parent, GameObject child)
    {
        if (parent != null && child != null)
        {
            Transform t = child.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            child.layer = parent.layer;
        }
    }

    static public GameObject GetParent(GameObject go)
    {
        Transform t = go.transform;
        Transform parent = t.parent;
        if (parent != null)
        {
            t = parent;
        }
        return t.gameObject;
    }

    static public void Destroy(UnityEngine.Object obj)
    {
        if (obj != null)
        {
            if (Application.isPlaying)
            {
                if (obj is GameObject)
                {
                    GameObject go = obj as GameObject;
                    go.transform.parent = null;
                }

                UnityEngine.Object.Destroy(obj);
            }
            else UnityEngine.Object.DestroyImmediate(obj);
        }
    }

    static public bool fileAccess
    {
        get
        {
            return Application.platform != RuntimePlatform.WindowsWebPlayer &&
                Application.platform != RuntimePlatform.OSXWebPlayer;
        }
    }

    static public bool Save(string fileName, byte[] bytes)
    {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO || UNITY_WP8
		return false;
#else
        if (!fileAccess) return false;
        string path = Application.persistentDataPath + "/" + fileName;

        if (bytes == null)
        {
            if (File.Exists(path)) File.Delete(path);
            return true;
        }

        FileStream file = null;

        try
        {
            file = File.Create(path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }

        file.Write(bytes, 0, bytes.Length);
        file.Close();
        return true;
#endif
    }

    static public byte[] Load(string fileName)
    {
#if UNITY_WEBPLAYER || UNITY_FLASH || UNITY_METRO || UNITY_WP8
		return null;
#else
        if (!fileAccess) return null;

        string path = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        return null;
#endif
    }

    static public string GetFuncName(object obj, string method)
    {
        if (obj == null) return "<null>";
        string type = obj.GetType().ToString();
        int period = type.LastIndexOf('/');
        if (period > 0) type = type.Substring(period + 1);
        return string.IsNullOrEmpty(method) ? type : type + "/" + method;
    }

}

