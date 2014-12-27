using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace engine.core.anim
{
    public class LStateTrigger
    {
        public LState from;
        public LState to;
        public float fadeTime = 0;
        public bool triggerOnEnd = true;
        public List<LSwitchCondition> conditions = null;

        public LStateTrigger(LState f, LState t, float ft, bool te = true, LSwitchCondition[] scs = null)
        {
            conditions = new List<LSwitchCondition>();
            from = f;
            to = t;
            fadeTime = ft;
            triggerOnEnd = te;
            if (scs != null)
            {
                foreach (LSwitchCondition sc in scs)
                {
                    conditions.Add(sc);
                }
            }
        }

        public LStateTrigger(LState f, LState t, float ft, bool te, LSwitchCondition sc)
        {
            conditions = new List<LSwitchCondition>();
            from = f;
            to = t;
            fadeTime = ft;
            triggerOnEnd = te;
            conditions.Add(sc);
        }

        public bool Match()
        {
            foreach (LSwitchCondition sc in conditions)
            {
                if (sc.Match() == false)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
