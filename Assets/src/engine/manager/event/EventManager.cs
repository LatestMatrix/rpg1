using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using engine.core;

namespace engine.manager
{
    public class EventManager
    {
        public delegate void EventDelegate(LEvent e);
        private Dictionary<string, List<EventDelegate>> _map;

        public EventManager()
        {
            _map = new Dictionary<string, List<EventDelegate>>();
        }

        public void addEvent(string e, EventDelegate d)
        {
            if (_map.ContainsKey(e) == false)
            {
                _map.Add(e, new List<EventDelegate>());
            }
            if (hasSameRegister(e, d))
            {
                return;
            }
            _map[e].Add(d);
        }

        public void removeEvent(string e, EventDelegate d)
        {
            if (_map.ContainsKey(e) == false)
            {
                return;
            }
            _map[e].Remove(d);
        }

        public void sendEvent(LEvent e)
        {
            if (_map.ContainsKey(e.name) == false)
            {
                return;
            }
            List<EventDelegate> list = _map[e.name];
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                list[i](e);
            }
        }

        private bool hasSameRegister(string e, EventDelegate d)
        {
            List<EventDelegate> list = _map[e];
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