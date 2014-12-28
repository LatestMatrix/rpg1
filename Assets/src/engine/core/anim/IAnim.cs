using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.anim
{
    public interface IAnim
    {
        void Run(LState s, float fadeTime = 0);
        void SetSwitch(string name, object value);
        string GetAnimName();
        bool CanMove();
    }
}
