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
        private bool _isDragMove = false;
        private int _joyFigerID = -1;

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

        private Vector3 GetJoyAreaPos(float range)
        {
            if (Input.touchCount > 0)
            {
                int count = Input.touchCount;
                Vector3 pos;
                if (_joyFigerID >= 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (Input.touches[i].fingerId == _joyFigerID)
                        {
                            return Input.touches[i].position;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        pos = Input.touches[i].position;
                        if (range == 0f || pos.magnitude < Mathf.Min(Screen.width, Screen.height) * range)
                        {
                            _joyFigerID = Input.touches[i].fingerId;
                            return pos;
                        }
                    }
                }
                _joyFigerID = -1;
            }
            else
            {
                return Input.mousePosition;
            }
            _joyFigerID = -1;
            return Vector3.zero;
        }

        private bool IsInJoyArea(float range)
        {
            Vector3 pos = GetJoyAreaPos(range);
            if ((_joyFigerID < 0 && Input.touchCount > 0) || 
                pos.magnitude > Mathf.Min(Screen.width, Screen.height) * range)
            {
                return false;
            }
            return true;
        }

        private void OnRootPress(GameObject btn, bool state)
        {
            if (IsInJoyArea(0.7f) == false)
            {
                return;
            }
            if (state && _isDragMove == false)
            {
                Vector3 pos = GetJoyAreaPos(0.8f);
                pos.x -= Screen.width / 2;
                pos.y -= Screen.height / 2;
                _joyStickTrans.localPosition = pos;
                _joyTarTrans.localPosition = Vector3.zero;
                _joyStick.SetActive(true);
                _isDragMove = true;
                UIEventListener.Get(_uiRoot).onDrag += OnRootDrag;
            }
            else if (_joyFigerID < 0 || TouchFigerLeave())
            {
                _joyStick.SetActive(false);
                _isDragMove = false;
                UIEventListener.Get(_uiRoot).onDrag -= OnRootDrag;
                DragMove(Vector2.zero);
            }
        }

        private bool TouchFigerLeave()
        {
            if (_joyFigerID >= 0)
            {
                int count = Input.touchCount;
                for (int i = 0; i < count; i++)
                {
                    if (Input.touches[i].fingerId == _joyFigerID && 
                        (Input.touches[i].phase == TouchPhase.Ended ||
                        Input.touches[i].phase == TouchPhase.Canceled))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnRootDrag(GameObject go, Vector2 delta)
        {
            if (IsInJoyArea(0.8f) == false || _isDragMove == false)
            {
                return;
            }
            Vector3 pos = GetJoyAreaPos(0f);
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
                _isKeyboardMove = false;
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
