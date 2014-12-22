using UnityEngine;
using System.Collections;

namespace engine.manager
{
    public enum LEventType
    {
        AIEvent,//AI事件
    }

    public class LEvent
    {
        public LEventType type;

        public LEvent()
        {

        }

    }
}