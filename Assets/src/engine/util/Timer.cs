using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public delegate void CallBackFun();
public delegate void CallBackRemoveFun(TimeItem item);
public class Timer
{
    private static List<TimeItem> _items = null;
    public static int AddTimer(CallBackFun fun, float intervalTime, int time)
    {
        TimeItem item = GetTimer(fun);
        if (item != null)
        {
            RemoveTimer(item);
        }
        item = TimeItem.Init(fun, RemoveTimer, intervalTime, time);
        _items.Add(item);
        return item.id;
    }

    public static bool RemoveTimer(int id)
    {
        int count = _items.Count;
        for (int i = 0; i < count; i++ )
        {
            TimeItem item = _items[i];
            if(item.id == id)
            {
                _items.RemoveAt(i);
                item.Cancel();
                return true;
            }
        }
        return false;
    }

    public static void RemoveTimer(TimeItem item)
    {
        if(_items.Remove(item))
        {
            item.Cancel();
        }
    }

    public static TimeItem GetTimer(CallBackFun fun)
    {
        foreach(TimeItem i in _items)
        {
            if(i.fun == fun)
            {
                return i;
            }
        }
        return null;
    }

    public static TimeItem GetTimer(int id)
    {
        foreach (TimeItem i in _items)
        {
            if (i.id == id)
            {
                return i;
            }
        }
        return null;
    }

}



