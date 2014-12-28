using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.anim
{
    public class LSwitchCondition
    {
        public ISwitch s;
        public Condition c;
        public object target;

        public LSwitchCondition(ISwitch sw, Condition cn, object tar)
        {
            s = sw;
            c = cn;
            target = tar;
        }

        virtual public bool Match()
        {
            return s.Match(this);
        }
    }
}
