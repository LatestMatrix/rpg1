using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{

    public class KeyManager
    {
        public int maxTarDis = 50;

        private bool _btn1State = false;
        private float _repetTime = 0.3f;
        private float _passTime = 0f;
        private GameObject _joyStick = null;
        private Transform _joyStickTrans = null;
        private Transform _joyTarTrans = null;
        private GameObject _uiRoot = null;
        private bool _isKeyboardMove = false;

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
            _joyStick = GameObject.Find("joystick");
            _joyStickTrans = _joyStick.transform;
            _joyTarTrans = GameObject.Find("joyTarget").transform;
            _uiRoot = GameObject.Find("UI Root");
            UIEventListener.Get(_uiRoot).onPress = OnRootPress;
            _joyStick.SetActive(false);
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
                _passTime = 0;
                LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent, "2att"));
                //LEngine.sm.mainPlayerScript.ia.SetSwitch("2att", true);
            }
        }

        private void OnSkill2Press(GameObject btn, bool state)
        {
            if(state)
            {
                //LEngine.sm.mainPlayerScript.ia.SetSwitch("2skill1", true);
                LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent, "2skill1"));
            }
        }

        private void OnSkill3Press(GameObject btn, bool state)
        {
            if (state)
            {
                //LEngine.sm.mainPlayerScript.ia.SetSwitch("2skill2", true);
                LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent, "2skill2"));
            }
        }

        private void OnSkill4Press(GameObject btn, bool state)
        {
            if (state)
            {
                //LEngine.sm.mainPlayerScript.ia.SetSwitch("2skill3", true);
                LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent, "2skill3"));
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
                    //LEngine.sm.mainPlayerScript.ia.SetSwitch("2att", true);
                    LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent, "2att"));
                }
            }
            KeyBoardMove();
        }

        private void OnRootPress(GameObject btn, bool state)
        {
            if (state)
            {
                Vector3 pos = Input.mousePosition;
                if (pos.magnitude < Mathf.Min(Screen.width, Screen.height) * 0.9)
                {
                    pos.x -= Screen.width / 2;
                    pos.y -= Screen.height / 2;
                    _joyStickTrans.localPosition = pos;
                    _joyTarTrans.localPosition = Vector3.zero;
                    _joyStick.SetActive(true);
                    UIEventListener.Get(_uiRoot).onDrag += OnRootDrag;
                    return;
                }
            }
            _joyStick.SetActive(false);
            UIEventListener.Get(_uiRoot).onDrag -= OnRootDrag;
            DragMove(Vector2.zero);
        }

        private void OnRootDrag(GameObject go, Vector2 delta)
        {
            Vector3 pos = Input.mousePosition;
            pos.x -= Screen.width / 2;
            pos.y -= Screen.height / 2;
            pos = pos - _joyStickTrans.localPosition;
            if (pos.magnitude > maxTarDis)
            {
                pos.Normalize();
                pos.x *= maxTarDis;
                pos.y *= maxTarDis;
            }
            _joyTarTrans.localPosition = pos;
            pos.Normalize();
            DragMove(pos);
        }

        private void KeyBoardMove()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
            {
                dir.y += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir.x -= 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir.y -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir.x += 1;
            }
            if(dir != Vector2.zero)
            {
                _isKeyboardMove = true;
                dir.Normalize();
                DragMove(dir);
            }
            else if(_isKeyboardMove)
            {
                DragMove(dir);
            }
        }

        private void DragMove(Vector2 dir)
        {
            //Log.Trace(Time.time);
            if (dir == Vector2.zero)
            {
                LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent));
            }
            else
            {
                LEngine.em.DispatchEvent(new LEvent(LEventType.KeyEvent, dir));
            }
        }

    }
}
