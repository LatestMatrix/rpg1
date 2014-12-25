using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.core.anim
{
    public class Skeleton
    {
        private Animator _anim;
        private List<int>[] _triChanges;
        private bool _tcp = false;
        private Dictionary<int, string> _nameMap;

        public Skeleton(Animator anim, AnimConfig config)
        {
            _anim = anim;
            _triChanges = new List<int>[2];
            _triChanges[0] = new List<int>();
            _triChanges[1] = new List<int>();
            _nameMap = new Dictionary<int, string>();

            InitHashName(config.animList);
            InitBoolTrigger(config.boolList);
            InitBoolTrigger(config.floatList);
        }

        private void InitHashName(string[] nameList)
        {
            foreach (string an in nameList)
            {
                _nameMap.Add(Animator.StringToHash("Base Layer." + an), an);
            }
        }

        private void InitBoolTrigger(string[] nameList)
        {
            foreach (string an in nameList)
            {
                _nameMap.Add(Animator.StringToHash(an), an);
            }
        }

        private void InitFloatTrigger(string[] nameList)
        {
            foreach (string an in nameList)
            {
                _nameMap.Add(Animator.StringToHash(an), an);
            }
        }

        public string CurAnim()
        {
            AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
            if (_nameMap.ContainsKey(info.nameHash))
            {
                return _nameMap[info.nameHash];
            }
            return null;
        }

        public string NextAnim()
        {
            AnimatorStateInfo info = _anim.GetNextAnimatorStateInfo(0);
            if (_nameMap.ContainsKey(info.nameHash))
            {
                return _nameMap[info.nameHash];
            }
            return null;
        }

        public bool IsInTrans()
        {
            return _anim.IsInTransition(0);
        }

        public bool BoolActionTo(string name)
        {
            if (_nameMap.ContainsValue(name))
            {
                int tri = Animator.StringToHash(name);
                if (!_triChanges[_tcp ? 1 : 0].Contains(tri))
                {
                    _anim.SetBool(tri, true);
                    _triChanges[_tcp ? 1 : 0].Add(tri);
                    return true;
                }
            }
            return false;
        }

        public bool FloatActionTo(string name, float add)
        {
            if (_nameMap.ContainsValue(name))
            {
                int tri = Animator.StringToHash(name);
                _anim.SetFloat(tri, _anim.GetFloat(tri) + add);
                return true;
            }
            return false;
        }

        public bool FloatActionReset(string name, float value = 0)
        {
            if (_nameMap.ContainsValue(name))
            {
                _anim.SetFloat(name, value);
                return true;
            }
            return false;
        }

        public void OnUpdate()
        {
            List<int> l = _triChanges[_tcp ? 1 : 0];
            _tcp = !_tcp;
            foreach(int t in l)
            {
                _anim.SetBool(t, false);
            }
            l.Clear();
        }

    }
}
