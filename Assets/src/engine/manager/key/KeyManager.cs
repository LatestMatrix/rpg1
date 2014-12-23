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
            initEvent();
            initJoy();
            initSkill();
        }

        private void initEvent()
        {
            
        }

        private void initJoy()
        {
            GameObject tar = GameObject.Find("joyTarget");
            UIJoystick joy = tar.GetComponent<UIJoystick>();
            joy.dragMove += dragMove;
        }

        private void dragMove(Vector2 dir)
        {
            LEngine.em.sendEvent(new LEvent(LEventType.MainEvent, dir));
        }

        private void initSkill()
        {
            GameObject btn;
            btn = GameObject.Find("skill1Btn");
            UIEventListener.Get(btn).onPress = onSkill1Press;

            btn = GameObject.Find("skill2Btn");
            UIEventListener.Get(btn).onPress = onSkill2Press;

            btn = GameObject.Find("skill3Btn");
            UIEventListener.Get(btn).onPress = onSkill3Press;

            btn = GameObject.Find("skill4Btn");
            UIEventListener.Get(btn).onPress = onSkill4Press;
        }

        private void onSkill1Press(GameObject btn, bool state)
        {
            _btn1State = state;
            if (state)
            {
                Log.log("!!!");
            }
        }

        private void onSkill2Press(GameObject btn, bool state)
        {
            if(state)
            {
                Log.log("222");
            }
        }

        private void onSkill3Press(GameObject btn, bool state)
        {
            if (state)
            {
                Log.log("333");
            }
        }

        private void onSkill4Press(GameObject btn, bool state)
        {
            if (state)
            {
                Log.log("444");
            }
        }

        public void onUpdate()
        {
            if (_btn1State == true)
            {
                _passTime += Time.deltaTime;
                if (_passTime > _repetTime)
                {
                    _passTime -= _repetTime;
                    Log.log("!!!");
                }
            }
        }

    }
}
