using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.manager
{
    public class KeyManager
    {
        public KeyManager()
        {
            initEvent();
            initJoy();
        }

        private void initEvent()
        {
            
        }

        private void initJoy()
        {
            GameObject tar = GameObject.Find("joyTarget");
            UIJoystick joy = tar.GetComponent<UIJoystick>();
            joy.dragMove += dragMove;
        }

        private void dragMove(Vector2 dir)
        {
            LEngine.em.sendEvent(LMainEvent.MoveEvent(dir));
        }

    }
}
