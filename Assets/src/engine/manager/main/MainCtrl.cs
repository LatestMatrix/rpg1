using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.manager
{
    public class MainCtrl
    {
        private List<LEvent> _eList;
        private List<LEvent> _eListClone;
        public MainCtrl()
        {
            _eList = new List<LEvent>();
            _eListClone = new List<LEvent>();

            InitEvent();
        }

        private void InitEvent()
        {
            LEngine.em.AddEvent(LEventType.MainEvent, OnReceive);
        }

        private void OnReceive(LEvent e)
        {
            _eList.Add(e);
        }

        public void OnUpdate()
        {
            _eListClone.Clear();
            _eListClone.AddRange(_eList);
            _eList.Clear();
        }

    }
}
