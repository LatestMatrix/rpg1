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
        public float range = 0.8f;

        private Transform _joyTarTrans = null;
        private GameObject _uiRoot = null;
        private int _touchFiger = -1;

        private bool _btn1State = false;
        private float _repetTime = 0.3f;
        private float _passTime = 0f;
        private GameObject _joyStick = null;
        private Transform _joyStickTrans = null;

        private bool _isKeyboardMove = false;
        private Vector3 _pos;
        private bool _state = false;
        private bool _lastState = false;

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

        private void OnRootDrag(GameObject go, Vector2 delta)
        {
            _pos = Vector3.zero;
            GetInfo();
            if (_lastState != _state)
            {
                return;
            }

            _pos.x -= Screen.width / 2;
            _pos.y -= Screen.height / 2;
            _pos = _pos - _joyStickTrans.localPosition;
            if (_pos.magnitude > maxTarDis)
            {
                _pos.Normalize();
                _pos.x *= maxTarDis;
                _pos.y *= maxTarDis;
            }
            _joyTarTrans.localPosition = _pos;
            _pos.Normalize();
            DragMove(_pos);
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
                _isKeyboardMove = false;
                DragMove(dir);
            }
        }

        private void OnRootPress(GameObject btn, bool state)
        {
            _state = state;
            _pos = Vector3.zero;
            GetInfo();
            //Log.UILog("dg " + _lastState + "," + _info.state + "," + _info.isIn + "," + Input.touchCount);
            if (_lastState == _state)
            {
                return;
            }
            if (_state)
            {
                //Log.UILog("show");
                _pos.x -= Screen.width / 2;
                _pos.y -= Screen.height / 2;
                _joyStickTrans.localPosition = _pos;
                _joyTarTrans.localPosition = Vector3.zero;
                _joyStick.SetActive(true);
                UIEventListener.Get(_uiRoot).onDrag += OnRootDrag;
            }
            else
            {
                //Log.UILog("hide");
                _joyStick.SetActive(false);
                UIEventListener.Get(_uiRoot).onDrag -= OnRootDrag;
                DragMove(Vector2.zero);
            }
            _lastState = _state;
        }

        private void GetInfo()
        {
            if (Input.touchCount == 0)
            {
                GetMouseInfo();
            }
            else
            {
                GetTouchInfo();
            }
        }

        private void GetMouseInfo()
        {
            _pos = Input.mousePosition;
            bool isIn = IsInJoyArea(_pos);
            if(_state == true && _lastState == false && isIn == false)
            {
                _state = false;
            }
        }

        private void GetTouchInfo()
        {
            _state = false;
            int count = Input.touchCount;
            //Log.UILog(count + "==============" + _touchFiger);
            if (_touchFiger == -1)
            {
                for (int i = 0; i < count; i++)
                {
                    Touch t = Input.GetTouch(i);
                    if (t.phase == TouchPhase.Began && IsInJoyArea(t.position))
                    {
                        _pos = t.position;
                        _touchFiger = t.fingerId;
                        _state = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    Touch t = Input.GetTouch(i);
                    if (t.fingerId == _touchFiger)
                    {
                        _pos = t.position;
                        _state = GetTouchState(t);
                        if (_state == false)
                        {
                            _touchFiger = -1;
                        }
                        //Log.UILog(
                        //    "fig = " + t.fingerId +
                        //    "pos = " + _info.pos.ToString() +
                        //    "tp = " + t.phase.ToString() + "," + _info.state.ToString() + "," + _info.isIn.ToString());
                        break;
                    }
                }
            }
        }

        private bool GetTouchState(Touch t)
        {
            if (t.phase == TouchPhase.Canceled ||
                t.phase == TouchPhase.Ended)
            {
                return false;
            }
            return true;
        }

        private bool IsInJoyArea(Vector3 pos)
        {
            return pos.magnitude < Mathf.Min(Screen.width, Screen.height) * range;
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
