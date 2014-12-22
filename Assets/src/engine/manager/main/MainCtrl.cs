using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.manager
{
    public class MainCtrl
    {
        private List<LMainEvent> _eList;
        private List<LMainEvent> _eListClone;
        private List<BaseMain> _wList;
        public MainCtrl()
        {
            _eList = new List<LMainEvent>();
            _eListClone = new List<LMainEvent>();
            _wList = new List<BaseMain>();
            _wList.Add(new MoveMain());
            initEvent();
        }

        private void initEvent()
        {
            LEngine.em.addEvent(LEventType.AIEvent, onReceive);
        }

        private void onReceive(LEvent e)
        {
            LMainEvent ae = e as LMainEvent;
            _wList[(int)(ae.aiType)].add(ref _eList, ae);
        }

        public void work()
        {
            _eListClone.Clear();
            _eListClone.AddRange(_eList);
            _eList.Clear();
            foreach (LMainEvent e in _eListClone)
            {
                _wList[(int)(e.aiType)].work(e);
            }
            
        }

    }
}
