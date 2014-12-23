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

            initEvent();
        }

        private void initEvent()
        {
            LEngine.em.addEvent(LEventType.MainEvent, onReceive);
        }

        private void onReceive(LEvent e)
        {
            _eList.Add(e);
        }

        public void onUpdate()
        {
            _eListClone.Clear();
            _eListClone.AddRange(_eList);
            _eList.Clear();

        }

    }
}
