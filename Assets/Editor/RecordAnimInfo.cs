using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class RecordAnimInfo : EditorWindow
{
    private static string RESOURCE_PATH = null;

    private bool isReadAnim = false;

    [MenuItem("Export/Anim")]
    static void Initialize()
    {
        RESOURCE_PATH = Application.dataPath + "/Resources/";

        RecordAnimInfo window = (RecordAnimInfo)EditorWindow.GetWindowWithRect(typeof(RecordAnimInfo), new Rect(0, 0, 180, 110), false, "动作数据导出");
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 160, 100));
        {
            if (GUILayout.Button("导出动作数据"))
            {
                isReadAnim = true;
                ReadDirectorie(RESOURCE_PATH);
            }
            if (GUILayout.Button("导出触发器数据"))
            {
                isReadAnim = false;
                ReadDirectorie(RESOURCE_PATH);
            }
        }
        GUILayout.EndArea();
    }

    private void ReadDirectorie(string path)
    {
        if (Directory.Exists(path))
        {
            string[] filePaths = Directory.GetFiles(path);
            foreach (string filePath in filePaths)
            {
                ReadFile(filePath);
            }
        }
        string[] dirParhs = Directory.GetDirectories(path);
        foreach (string dirPath in dirParhs)
        {
            ReadDirectorie(dirPath);
        }
    }

    private void ReadFile(string path)
    {
        if (path.Contains(".prefab") && !path.Contains(".meta"))
        {
            int start = RESOURCE_PATH.Length;
            int end = path.LastIndexOf(".");
            string name = path.Substring(start, end - start);
            GameObject prefab = Resources.Load<GameObject>(name);
            if (prefab)
            {
                ReadPrefab(prefab, path.Substring(0, end));
            }
        }
    }

    private void ReadPrefab(GameObject prefab, string path)
    {
        GameObject obj = GameObject.Instantiate(prefab) as GameObject;
        Animator anim = obj.GetComponent<Animator>();
        if (anim)
        {
            AnimatorController ac = anim.runtimeAnimatorController as AnimatorController;
            if(ac != null)
            {
                if(isReadAnim)
                {
                    AnimatorControllerLayer l = ac.GetLayer(0);
                    StateMachine state = l.stateMachine;
                    int count = state.stateCount;
                    State s;

                    StreamWriter sw = File.CreateText(path + ".anim");

                    for (int i = 0; i < count; i++)
                    {
                        s = state.GetState(i);
                        sw.WriteLine(s.name);
                    }
                    sw.Close();
                    sw.Dispose();
                }
                else
                {
                    int count = ac.parameterCount;
                    for(int i = 0; i < count; i++)
                    {
                        //AnimatorControllerParameter acp = ac.GetParameter(i);
                        //Debug.Log(acp.name + "," + acp.type);
                    }
                }
            }
        }
        GameObject.DestroyImmediate(obj);
    }

}

