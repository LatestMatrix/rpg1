using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using engine.core;

namespace engine.manager
{
    public class EventManager
    {
        public delegate void EventDelegate(LEvent e);
        private Dictionary<LEventType, List<EventDelegate>> _map;

        public EventManager()
        {
            _map = new Dictionary<LEventType, List<EventDelegate>>();
        }

        public void AddEvent(LEventType t, EventDelegate d)
        {
            if (_map.ContainsKey(t) == false)
            {
                _map.Add(t, new List<EventDelegate>());
            }
            if (HasSameRegister(t, d))
            {
                return;
            }
            _map[t].Add(d);
        }

        public void RemoveEvent(LEventType t, EventDelegate d)
        {
            if (_map.ContainsKey(t) == false)
            {
                return;
            }
            _map[t].Remove(d);
        }

        public void DispatchEvent(LEvent e)
        {
            if (_map.ContainsKey(e.type) == false)
            {
                return;
            }
            List<EventDelegate> list = _map[e.type];
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                list[i](e);
            }
        }

        private bool HasSameRegister(LEventType t, EventDelegate d)
        {
            List<EventDelegate> list = _map[t];
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                if (list[i] == d)
                {
                    return true;
                }
            }
            return false;
        }

    }
}