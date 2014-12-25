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
            AnimConfig acf = new AnimConfig();
            acf.animList = new string[17]
            {
                "stand",
                "run",
                "attack_first",
                "attack_first_reduction",
                "attack_second",
                "attack_second_reduction",
                "attack_third",
                "attack_third_reduction",
                "attack_end_first half",
                "attack_end_reduction",
                "attack_end_second half",
                "skill_cut",
                "skill_cut_reduction",
                "skill_Rotation",
                "skill_Rotation_reduction",
                "skill_stab",
                "skill_stab_Reduction"
            };
            acf.boolList = new string[6] 
            { 
                "2run",
                "2att",
                "2skill1",
                "2skill2",
                "2skill3",
                "stop"
            };
            acf.floatList = new string[1]
            {
                "skill2Time"
            };
            mainPlayerScript.s = new Skeleton(anim, acf);
        }

    }
}
