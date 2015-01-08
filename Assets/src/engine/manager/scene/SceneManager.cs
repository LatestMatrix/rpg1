using engine.core;
using engine.core.anim;
using engine.core.role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{
    public class SceneManager
    {
        private GameObject _player = null;
        private GameObject _monster = null;
        private GameObject _npc = null;
        private GameObject _scene = null;

        public GameObject mainPlayer = null;
        public Player mainPlayerScript = null;

        public GameObject mainCamera = null;
        public SCamera mainCameraScript = null;

        public SceneManager()
        {
            InitEvent();
        }

        private void InitEvent()
        {
            
        }

        public void InitScene()
        {
            _player = GameObject.Find("Player");
            _monster = GameObject.Find("Monster");
            _npc = GameObject.Find("Npc");
            _scene = GameObject.Find("Scene");
            //初始化主角
            mainPlayer = Tools.Instance(LEngine.rm.Load("role/1/role1"), _player);
            mainPlayer.name = "MainRole";
            mainPlayerScript = mainPlayer.AddComponent<Player>();
            Animator anim = mainPlayer.GetComponent<Animator>();
            mainPlayerScript.ia = new MainAnimCtrl(anim);
            LEngine.em.DispatchEvent(new LEvent(LEventType.SetMainPlayer, mainPlayerScript));
            LEngine.em.DispatchEvent(new LEvent(LEventType.AddMoveItem, mainPlayerScript));
            //绑武器
            Tools.AddBinding(LEngine.rm.Load("role/1/weapon1"), mainPlayer, "Bip001 Weapons");
            //SkinnedMeshRenderer[] renders = mainPlayer.GetComponentsInChildren<SkinnedMeshRenderer>();
            //foreach (SkinnedMeshRenderer r in renders)
            //{
            //    r.useLightProbes = true;
            //}
            //初始化摄像机
            mainCamera = GameObject.Find("Main Camera");
            mainCameraScript = mainCamera.AddComponent<SCamera>();
            mainCameraScript.SetTarget(mainPlayer.transform);
            //初始化场景
            Application.LoadLevel("Demo6");
            //Application.LoadLevel("scene2");
            Timer.AddTimer(InitNavAgent, 1);
            
        }

        private void InitNavAgent()
        {
            NavMeshAgent na = mainPlayer.AddComponent<NavMeshAgent>();
        }

    }
}
