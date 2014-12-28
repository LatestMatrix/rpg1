using engine.manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace engine.core
{
    public class SCamera : Dummy, IMove
    {
        private static float MAX_LEN = 0.4f;

        private Transform _cam = null;
        private Transform _tar = null;

        private float _scale = 1;
        private Vector3 _vec = Vector3.zero;
        

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            _cam = gameObject.GetComponent<Transform>();
            rotation = Quaternion.Euler(30, 0, 0);
            scale = 4;
            LEngine.em.DispatchEvent(new LEvent(LEventType.AddMoveItem, this));
        }

        public void SetTarget(Transform tar)
        {
            _tar = tar;
        }

        public Quaternion rotation
        {
            get
            {
                return _cam.localRotation;
            }
            set
            {
                _cam.localRotation = value;
                _vec = Vector3.zero;
            }
        }

        public float scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                _vec = Vector3.zero;
            }
        }

        public void MoveStep(float dTime)
        {
            if(_vec == Vector3.zero)
            {
                _vec = _cam.forward * (-_scale);
            }
            if(_tar != null)
            {
                Vector3 t = _tar.position + _vec;
                float l = Vector3.Distance(t, _cam.localPosition);
                if(l > MAX_LEN)
                {
                    _cam.localPosition = Vector3.Lerp(t, _cam.localPosition, MAX_LEN/l);
                }
            }
        }
    }
}
