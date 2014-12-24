using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditorInternal;
using UnityEngine;

namespace engine.core
{
    public class Skeleton
    {
        private Animator _anim;
        private Dictionary<STriggerType, int> _triggers;
        private List<int>[] _triChanges;
        private bool _tcp = false;
        private Dictionary<int, string> _nameMap;

        public Skeleton(Animator anim)
        {
            _anim = anim;
            _triggers = new Dictionary<STriggerType, int>();
            _triChanges = new List<int>[2];
            _triChanges[0] = new List<int>();
            _triChanges[1] = new List<int>();
            InitHashName();
        }

        private void InitHashName()
        {
            AnimatorController ctrl = _anim.runtimeAnimatorController as AnimatorController;
            AnimatorControllerLayer layer = ctrl.GetLayer(0);
            StateMachine stateMachine = layer.stateMachine;
            _nameMap = new Dictionary<int, string>();
            
            for (int i = 0; i < stateMachine.stateCount; i++)
            {
                string name = stateMachine.GetState(i).name;
                _nameMap.Add(Animator.StringToHash(layer.name + "." + name), name);
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

        public void AddTrigger(STriggerType t, string trigger)
        {
            if (_triggers.ContainsKey(t))
            {
                _triggers[t] = Animator.StringToHash(trigger);
            }
            else
            {
                _triggers.Add(t, Animator.StringToHash(trigger));
            }
        }

        public void RemoveTrigger(STriggerType t)
        {
            if (_triggers.ContainsKey(t))
            {
                _triggers.Remove(t);
            }
        }

        public bool ActionTo(STriggerType t)
        {
            if (_triggers.ContainsKey(t))
            {
                int tri = _triggers[t];
                if (!_triChanges[_tcp ? 1 : 0].Contains(tri))
                {
                    _anim.SetBool(tri, true);
                    _triChanges[_tcp ? 1 : 0].Add(tri);
                    return true;
                }
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
