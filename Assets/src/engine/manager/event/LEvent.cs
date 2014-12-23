using UnityEngine;
using System.Collections;

namespace engine.manager
{
    public enum LEventType
    {
        MainEvent,//主角事件
    }

    public class LEvent
    {
        public LEventType type;
        public object data;

        public LEvent(LEventType t, object d = null)
        {
            type = t;
            data = d;
        }

    }
}