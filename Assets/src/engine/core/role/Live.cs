using engine.core.anim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.core.role
{
    public class Live : Dummy, IMove, IAI
    {
        public IAnim ia = null;
        public Vector3 direction;

        public float moveSpeed = 2f;//移动速度 单位/s
        public int angelSpeed = 540;//转身速度 单位/s

        private Transform _trans = null;

        public void MoveStep(float dTime)
        {
            if(_trans == null)
            {
                _trans = GetComponent<Transform>();
            }
            if(direction != Vector3.zero)
            {
                float angle = Vector3.Angle(_trans.forward, direction);
                float at = angle / angelSpeed;
                if(at > dTime)
                {
                    _trans.forward = Vector3.Slerp(_trans.forward, direction, dTime/at);
                }
                else
                {
                    _trans.forward = direction;
                }
                Vector3 step = direction;
                step *= (moveSpeed * dTime);
                _trans.localPosition += step;
            }
        }

    }
}
