using engine.core.role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{
    public class MainCtrl
    {
        private List<LEvent> _eList;
        private List<LEvent> _eListClone;
        private Player _main;
        private Transform _mainCamTran;

        public MainCtrl()
        {
            _eList = new List<LEvent>();
            _eListClone = new List<LEvent>();
            _mainCamTran = GameObject.Find("Main Camera").transform;

            InitEvent();
        }

        private void InitEvent()
        {
            LEngine.em.AddEvent(LEventType.KeyEvent, OnReceive);
            LEngine.em.AddEvent(LEventType.SetMainPlayer, OnSettingMainPlayer);
        }

        private void OnReceive(LEvent e)
        {
            _eList.Add(e);
        }

        private void OnSettingMainPlayer(LEvent e)
        {
            _main = e.data as Player;
        }

        public void OnUpdate()
        {
            _eListClone.Clear();
            _eListClone.AddRange(_eList);
            if (_main != null)
            {
                foreach (LEvent e in _eList)
                {
                    if (e.data is string)
                    {
                        _main.ia.SetSwitch((string)e.data, true);
                    }
                    else if(e.data is Vector2)
                    {
                        _main.ia.SetSwitch("2run", true);
                        if (_main.ia.CanMove())
                        {
                            Vector3 tar = _mainCamTran.forward;
                            tar.y = 0;
                            Vector2 t = (Vector2)e.data;
                            tar = Quaternion.Euler(0, Mathf.Atan2(t.x, t.y) * 180 / Mathf.PI, 0) * tar;
                            tar.Normalize();
                            _main.direction = tar;
                        }
                        else
                        {
                            _main.direction = Vector3.zero;
                        }
                    }
                    else//stop
                    {
                        _main.ia.SetSwitch("stop", true);
                        _main.direction = Vector3.zero;
                    }
                }
            }
            _eList.Clear();
        }

    }
}
