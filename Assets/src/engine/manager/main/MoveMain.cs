using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.manager
{
    public class MoveMain : BaseMain
    {
        public MoveMain()
            : base(LMainEventType.MOVE)
        {
            
        }
        override public void add(ref List<LMainEvent> list, LMainEvent e)
        {
            list.Add(e);
            Log.log(list.Count);
        }

        override public void work(LMainEvent e)
        {
            //Log.log(e);
        }
    }
}
