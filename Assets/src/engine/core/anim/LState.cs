using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.core.anim
{
    public class LState
    {
        public IAnim ia;
        public string animName = "";
        public int hashName = 0;
        public float time = 0;
        public bool isTrans = false;
        public bool isLoop = false;
        public bool canMove = false;
        public List<LStateTrigger> tris = null;
        private int _timerID = -1;
        private bool _isRuning = false;

        public LState(IAnim i, string name, float frame, bool l = false, bool m = false)
        {
            ia = i;
            animName = name;
            hashName = Animator.StringToHash("Base Layer." + animName);
            time = frame/30;
            isLoop = l;
            canMove = m;
            tris = new List<LStateTrigger>();
        }

        public void Start(float fadeTime = 0)
        {
            ia.Run(this, fadeTime);
            _isRuning = true;
            //Log.Trace("start anim " + ia.GetAnimName() + " time = " + Time.time);
            InitTimer();
        }

        public void Stop()
        {
            _isRuning = false;
            ClearTimer();
        }

        private void InitTimer()
        {
            ClearTimer();
            int lv = 1;
            if (isLoop)
            {
                lv = int.MaxValue;
            }
            _timerID = Timer.AddTimer(OnTimer, time, lv);
        }

        private void ClearTimer()
        {
            if (_timerID > 0)
            {
                Timer.RemoveTimer(_timerID);
                _timerID = -1;
            }
        }

        private void OnTimer()
        {
            if (!_isRuning)
            {
                return;
            }
            foreach(LStateTrigger t in tris)
            {
                if (t.triggerOnEnd && t.Match())
                {
                    t.to.Start(t.fadeTime);
                    break;
                }
            }
        }

        public void OnImmediately()
        {
            if (!_isRuning)
            {
                return;
            }
            foreach (LStateTrigger t in tris)
            {
                if (t.triggerOnEnd == false && t.Match())
                {
                    //Log.Trace(t.to.animName);
                    t.to.Start(t.fadeTime);
                    break;
                }
            }
        }

    }
}
