﻿using UnityEngine;
using System.Collections;
using engine.core;
using engine.manager;

public class Main : MonoBehaviour
{
    private MoveCtrl _mc;
    private MainCtrl _mac;

    private KeyManager _km;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LEngine.ma = this;
        LEngine.em = new EventManager();
        LEngine.sm = new SceneManager();
        LEngine.rm = new ResManager();

        _mc = new MoveCtrl();
        _mac = new MainCtrl();

        _km = new KeyManager();

        LEngine.sm.InitScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        _mac.OnUpdate();
        _km.OnUpdate();
        _mc.OnUpdate();
    }

    private void OnGUI()
    {
        GUILayout.Label("FPS:" + (1 / Time.deltaTime).ToString("f0"));
    }

}
