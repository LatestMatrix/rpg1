using engine.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{
    public class MoveCtrl
    {
        private List<IMove> _moveList = null;

        public MoveCtrl()
        {
            _moveList = new List<IMove>();
            InitEvent();
        }

        private void InitEvent()
        {
            LEngine.em.AddEvent(LEventType.AddMoveItem, OnAddItem);
            LEngine.em.AddEvent(LEventType.RemoveMoveItem, OnRemoveItem);
        }

        private void OnAddItem(LEvent e)
        {
            _moveList.Add((IMove)e.data);
        }

        private void OnRemoveItem(LEvent e)
        {
            _moveList.Remove((IMove)e.data);
        }

        public void OnUpdate()
        {
            float dt = Time.deltaTime;
            foreach(IMove m in _moveList)
            {
                m.MoveStep(dt);
            }
        }

    }
}
