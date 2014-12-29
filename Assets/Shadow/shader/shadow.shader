Shader "Projector/single" {
   Properties {
      _ShadowTex ("Cookie", 2D) = "gray" { TexGen ObjectLinear }
   }

   Subshader {
      Tags { "RenderType"="Transparent-1" }
      Pass {
         ZWrite Off
         Fog { Color (1, 1, 1) }
         AlphaTest Greater 0
         ColorMask RGB
         Blend DstColor Zero
		 Offset -1, -1
         SetTexture [_ShadowTex] {
            combine texture, ONE - texture
            Matrix [_Projector]
         }
      }
   }
}