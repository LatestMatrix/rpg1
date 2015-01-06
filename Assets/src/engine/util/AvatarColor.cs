using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace tools.util
{
    class AvatarColor : MonoBehaviour
    {
        private SkinnedMeshRenderer _render;
        private Shader _shader;

        void Start()
        {
            _render = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            _shader = _render.material.shader;
            Color c = _render.material.GetColor("_MainColor");
            _render.material.SetColor("_MainColor", Color.red);
        }

        public void SetColor(Color c)
        {
            _render.material.color = Color.red;
        }
        
    }
}
