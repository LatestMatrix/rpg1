using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.manager
{
    public class BaseMain
    {
        protected LMainEventType type;

        public BaseMain(LMainEventType t)
        {
            type = t;
        }

        virtual public void add(ref List<LMainEvent> list, LMainEvent e)
        {
            
        }

        virtual public void work(LMainEvent e)
        {

        }

    }
}
