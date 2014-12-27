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

        public void Run(LState s, float fadeTime)
        {
            if (_cur != null)
            {
                _cur.Stop();
            }
            if (_cur == s)
            {
                _anim.Play(s.hashName, 0, 0);
            }
            else if (fadeTime > 0)
            {
                _anim.CrossFade(s.hashName, fadeTime);
            }
            else
            {
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
