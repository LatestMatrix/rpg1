using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeItem : MonoBehaviour
{
    public CallBackFun fun = null;
    public CallBackRemoveFun removeFun = null;

    private static int UID = 0;

    public int id;
    public int total;
    public int cur;


    public static TimeItem Init(CallBackFun fun, CallBackRemoveFun rf, float intervalTime, int time = int.MaxValue)
    {
        TimeItem ins = new TimeItem();
        ins.id = UID++;
        ins.fun = fun;
        ins.removeFun = rf;
        ins.total = time;
        ins.cur = 0;

        ins.InvokeRepeating("back", intervalTime, intervalTime);
        return ins;
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
    }

    public void Cancel()
    {
        CancelInvoke("back");
    }
}

