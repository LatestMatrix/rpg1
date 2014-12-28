using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.core.anim
{
    public class AnimCtrl : IAnim
    {
        protected Animator _anim = null;
        protected Dictionary<string, LState> _states = null;
        protected Dictionary<string, ISwitch> _switch = null;
        protected LState _cur = null;

        public AnimCtrl() { }

        public AnimCtrl(Animator anim)
        {
            _anim = anim;
            _states = new Dictionary<string, LState>();
            _switch = new Dictionary<string, ISwitch>();

            InitSwitch();
            InitState();
        }

        public bool CanMove()
        {
            if (_cur != null)
            {
                return _cur.canMove;
            }
            return false;
        }

        public string GetAnimName()
        {
            if (_cur != null)
            {
                return _cur.animName;
            }
            return null;
        }

        public void Run(LState s, float fadeTime)
        {
            if (_cur != null)
            {
                _cur.Stop();
            }
            if (_cur == s)
            {
                //Log.Trace("1" + s.animName);
                _anim.Play(s.hashName, 0, 0);
            }
            else if (fadeTime > 0)
            {
                //Log.Trace("2" + s.animName);
                _anim.CrossFade(s.hashName, fadeTime, 0, 0);
            }
            else
            {
                //Log.Trace("3" + s.animName);
                _anim.Play(s.hashName);
            }
            ResetAllSwitch();
            _cur = s;
        }

        public void SetSwitch(string name, object value)
        {
            if (_switch.ContainsKey(name))
            {
                _switch[name].Set(value);
                if(_cur != null)
                {
                    _cur.OnImmediately();
                }
            }
        }

        private void ResetAllSwitch()
        {
            foreach (KeyValuePair<string, ISwitch> kv in _switch)
            {
                kv.Value.Reset();
            }
        }

        virtual protected void InitState()
        {

        }

        virtual protected void InitSwitch()
        {

        }

    }
}
