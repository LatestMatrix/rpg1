using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.anim
{
    public enum Condition
    {
        EQUAL,
        NOT_EQUAL,
        GREATER,
        GREATER_EQUAL,
        LESS,
        LESS_EQUAL
    }
    public interface ISwitch
    {
        bool Match(LSwitchCondition c);
        void Reset();

        void Set(object value);
    }
}
