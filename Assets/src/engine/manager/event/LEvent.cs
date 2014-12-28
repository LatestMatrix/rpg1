using UnityEngine;
using System.Collections;

namespace engine.manager
{
    public enum LEventType
    {
        KeyEvent,//操作事件
        SetMainPlayer,//设置主角
        AddMoveItem,//添加可移动对象
        RemoveMoveItem,//移除可移动对象
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