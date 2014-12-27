using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.anim
{
    public class BoolSwitch : ISwitch
    {
        public bool data = false;

        public bool Match(LSwitchCondition c)
        {
            switch(c.c)
            {
                case Condition.EQUAL:
                    if(data == (bool)c.target)
                    {
                        return true;
                    }
                    break;
                case Condition.NOT_EQUAL:
                    if (data != (bool)c.target)
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        public void Reset()
        {
            data = false;
        }

        public void Set(object value)
        {
            data = (bool)value;
        }

    }
}
