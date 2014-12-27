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
        private GameObject _player;
        private GameObject _monster;
        private GameObject _npc;
        private GameObject _scene;

        public GameObject mainPlayer;
        public Player mainPlayerScript;

        public SceneManager()
        {
            InitEvent();
        }

        private void InitEvent()
        {
            
        }

        public void initScene()
        {
            _player = GameObject.Find("Player");
            _monster = GameObject.Find("Monster");
            _npc = GameObject.Find("Npc");
            _scene = GameObject.Find("Scene");
            //初始化场景
            //初始化主角
            mainPlayer = Tools.Instance(LEngine.rm.Load("role/1/role1"), _player);
            mainPlayer.name = "MainRole";
            mainPlayerScript = mainPlayer.AddComponent<Player>();
            Animator anim = mainPlayer.GetComponent<Animator>();
            mainPlayerScript.ia = new MainAnimCtrl(anim);
        }

    }
}
