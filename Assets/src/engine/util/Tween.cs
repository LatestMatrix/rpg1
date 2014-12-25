using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace tools.util
{
    public class Tween
    {
        private static Tween _ins = null;

        public delegate void CallBack();

        private List<TweenItem> _list= null;

        public static Tween instance
        {
            get
            {
                if(_ins == null)
                {
                    _ins = new Tween();
                }
                return _ins;
            }
        }

        public Tween()
        {
            _list = new List<TweenItem>();
        }

        public int Add(Component cp, string name, float from, float to, float time, CallBack cb = null)
        {
            if(time <= 0)
            {
                throw(new System.Exception("时间不能小于等于0"));
            }
            TweenItem t;
            t = has(cp, name);
            if(t == null)
            {
                PropertyReference pr = new PropertyReference();
                pr.Set(cp, name);
                t = new TweenItem(pr, from, to, time, cb);
                _list.Add(t);
            }
            else
            {
                t.from = from;
                t.to = to;
                t.totalTime = time;
                t.passTime = 0;
                t.cbFun = cb;
            }
            return t.id;
        }

        private TweenItem has(Component cp, string name)
        {
            foreach(TweenItem item in _list)
            {
                if (item.pr.target == cp && item.pr.name == name)
                {
                    return item;
                }
            }
            return null;
        }

        public void OnFixedUpdate()
        {
            float dt = Time.fixedDeltaTime;
            for (int i = 0; i < _list.Count; )
            {
                TweenItem item = _list[i];
                if(item.update(dt))
                {
                    _list.Remove(item);
                }
                else
                {
                    i++;
                }
            }
        }

        class TweenItem
        {
            private static int UID = 0;

            public int id;
            public PropertyReference pr;
            public float from;
            public float to;
            public float totalTime;
            public float passTime;
            public CallBack cbFun;

            public TweenItem(PropertyReference p, float f, float t, float time, CallBack cb)
            {
                id = UID++;
                pr = p;
                from = f;
                to = t;
                totalTime = time;
                passTime = 0;
                cbFun = cb;
            }

            public bool update(float dt)
            {
                passTime += dt;
                if (passTime >= totalTime)
                {
                    passTime = totalTime;
                    pr.Set(to);
                    if(cbFun != null)
                    {
                        cbFun();
                    }
                    return true;
                }
                else
                {
                    pr.Set(from + (to - from) * (passTime / totalTime));
                }
                return false;
            }

        }
    }
}
