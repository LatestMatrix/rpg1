using engine.core.anim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.role
{
    public class Live : Dummy, IMove, IAI
    {
        public Skeleton s;

        public Live()
        {

        }

        void Update()
        {
            if(s != null)
            {
                s.OnUpdate();
            }
        }
    }
}
