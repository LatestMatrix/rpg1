using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{
    public class KeyManager
    {
        private bool _btn1State = false;
        private float _repetTime = 0.5f;
        private float _passTime = 0f;
        public KeyManager()
        {
            InitEvent();
            InitJoy();
            InitSkill();
        }

        private void InitEvent()
        {
            
        }

        private void InitJoy()
        {
            GameObject tar = GameObject.Find("joyTarget");
            UIJoystick joy = tar.GetComponent<UIJoystick>();
            joy.dragMove += DragMove;
        }

        private void DragMove(Vector2 dir)
        {
            LEngine.em.DispatchEvent(new LEvent(LEventType.MainEvent, dir));
        }

        private void InitSkill()
        {
            GameObject btn;
            btn = GameObject.Find("skill1Btn");
            UIEventListener.Get(btn).onPress = OnSkill1Press;

            btn = GameObject.Find("skill2Btn");
            UIEventListener.Get(btn).onPress = OnSkill2Press;

            btn = GameObject.Find("skill3Btn");
            UIEventListener.Get(btn).onPress = OnSkill3Press;

            btn = GameObject.Find("skill4Btn");
            UIEventListener.Get(btn).onPress = OnSkill4Press;
        }

        private void OnSkill1Press(GameObject btn, bool state)
        {
            _btn1State = state;
            if (state)
            {
                Log.Trace("!!!");
            }
        }

        private void OnSkill2Press(GameObject btn, bool state)
        {
            if(state)
            {
                Log.Trace("222");
            }
        }

        private void OnSkill3Press(GameObject btn, bool state)
        {
            if (state)
            {
                Log.Trace("333");
            }
        }

        private void OnSkill4Press(GameObject btn, bool state)
        {
            if (state)
            {
                Log.Trace("444");
            }
        }

        public void OnUpdate()
        {
            if (_btn1State == true)
            {
                _passTime += Time.deltaTime;
                if (_passTime > _repetTime)
                {
                    _passTime -= _repetTime;
                    Log.Trace("!!!");
                }
            }
        }

    }
}
