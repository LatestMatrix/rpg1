using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Log
{
    public static int LC = 0xFF;
    public static int WC = 0xFF;
    public static int EC = 0xFF;

    public static List<string> UIMessageList = new List<string>();

    public static void Trace(object message, int channel = 1)
    {
        if ((channel & LC) != 0)
        {
            Debug.Log(message);
        }
    }

    public static void Warn(object message, int channel = 1)
    {
        if ((channel & WC) != 0)
        {
            Debug.LogWarning(message);
        }
    }

    public static void Error(object message, int channel = 1)
    {
        if ((channel & EC) != 0)
        {
            Debug.LogError(message);
        }
    }

    public static void UILog(string message)
    {
        UIMessageList.Add(message);
        if(UIMessageList.Count > 20)
        {
            UIMessageList.RemoveAt(0);
        }
    }

}

