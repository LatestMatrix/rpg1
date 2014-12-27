using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.anim
{
    public class FloatSwitch : ISwitch
    {
        public float data = 0;

        public bool Match(LSwitchCondition c)
        {
            switch (c.c)
            {
                case Condition.EQUAL:
                    if (data == (float)c.target)
                    {
                        return true;
                    }
                    break;
                case Condition.NOT_EQUAL:
                    if (data != (float)c.target)
                    {
                        return true;
                    }
                    break;
                case Condition.LESS:
                    if (data < (float)c.target)
                    {
                        return true;
                    }
                    break;
                case Condition.LESS_EQUAL:
                    if (data <= (float)c.target)
                    {
                        return true;
                    }
                    break;
                case Condition.GREATER:
                    if (data > (float)c.target)
                    {
                        return true;
                    }
                    break;
                case Condition.GREATER_EQUAL:
                    if (data >= (float)c.target)
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
            data = 0;
        }

        public void Set(object value)
        {
            data = (float)value;
        }

    }
}
