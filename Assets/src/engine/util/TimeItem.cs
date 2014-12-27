using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeItem : MonoBehaviour
{
    public CallBackFun fun = null;
    public CallBackRemoveFun removeFun = null;

    private static int UID = 1;
    private static GameObject _obj = null;

    public int id;
    public int total;
    public int cur;


    public static TimeItem Init(CallBackFun fun, CallBackRemoveFun rf, float intervalTime, int time)
    {
        if(_obj == null)
        {
            _obj = GameObject.Find("Timer");
        }
        TimeItem ins = _obj.AddComponent<TimeItem>();
        ins.id = UID++;
        ins.fun = fun;
        ins.removeFun = rf;
        ins.total = time;
        ins.cur = 0;

        ins.InvokeRepeating("back", intervalTime, intervalTime);
        return ins;
    }

    public void Reset(CallBackFun f, CallBackRemoveFun rf, float intervalTime, int time)
    {
        id = UID++;
        fun = f;
        removeFun = rf;
        total = time;
        cur = 0;

        InvokeRepeating("back", intervalTime, intervalTime);
    }

    public void back()
    {
        cur++;
        if (cur >= total)
        {
            Cancel();
            fun();
            removeFun(this);
        }
        else
        {
            fun(); 
        }
    }

    public void Cancel()
    {
        CancelInvoke("back");
    }
}

