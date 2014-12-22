using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{
    public enum LMainEventType
    {
        MOVE,   //移动
        SKILL,      //使用技能
        HIT,         //打击
        TRIGGER,    //触发
    }

    public class LMainEvent : LEvent
    {
        public LMainEventType aiType;

        public Vector2 dir;

        public static LMainEvent MoveEvent(Vector2 d)
        {
            LMainEvent e = new LMainEvent();
            e.type = LEventType.AIEvent;
            e.aiType = LMainEventType.MOVE;
            e.dir = d;
            return e;
        }


    }
}
