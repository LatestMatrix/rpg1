// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:1,uamb:True,mssp:False,lmpd:False,lprd:False,enco:False,frtr:False,vitr:False,dbil:True,rmgx:True,rpth:0,hqsc:False,hqlp:False,tesm:0,blpr:1,bsrc:3,bdst:7,culm:2,dpts:2,wrdp:True,ufog:False,aust:False,igpj:True,qofs:1000,qpre:0,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32488,y:32411|diff-1757-OUT,diffpow-1757-OUT,spec-427-RGB,gloss-435-OUT,normal-18-RGB,emission-543-OUT,lwrap-1996-OUT,amdfl-1996-OUT,alpha-2050-OUT,clip-636-OUT;n:type:ShaderForge.SFN_Tex2d,id:18,x:32912,y:32664,ptlb:Mormal Map,ptin:_MormalMap,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:344,x:33159,y:32256,ptlb:Color Map,ptin:_ColorMap,tex:2e86b2d52eb86f84b8560ad1e8378e4c,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:403,x:33282,y:33050,ptlb:Fresnel Power,ptin:_FresnelPower,min:0,cur:0.4,max:1;n:type:ShaderForge.SFN_Color,id:427,x:33291,y:32427,ptlb:Specular Color,ptin:_SpecularColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:435,x:33193,y:32652,ptlb:Specular Range,ptin:_SpecularRange,min:0,cur:0.2,max:1;n:type:ShaderForge.SFN_Fresnel,id:443,x:33609,y:33039|EXP-468-OUT;n:type:ShaderForge.SFN_Blend,id:451,x:33121,y:33036,blmd:10,clmp:True|SRC-403-OUT,DST-591-OUT;n:type:ShaderForge.SFN_Slider,id:468,x:33658,y:33314,ptlb:Fresnel Range,ptin:_FresnelRange,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:482,x:33079,y:33217,ptlb:Fresnel Color,ptin:_FresnelColor,glob:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Blend,id:543,x:32884,y:33007,blmd:14,clmp:True|SRC-451-OUT,DST-482-RGB;n:type:ShaderForge.SFN_Subtract,id:591,x:33361,y:33215|A-443-OUT,B-468-OUT;n:type:ShaderForge.SFN_Slider,id:608,x:33134,y:32919,ptlb:Alpha Cutoff,ptin:_AlphaCutoff,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Power,id:636,x:32990,y:32868|VAL-344-A,EXP-608-OUT;n:type:ShaderForge.SFN_Blend,id:1757,x:32835,y:32262,blmd:10,clmp:True|SRC-1758-RGB,DST-344-RGB;n:type:ShaderForge.SFN_Color,id:1758,x:33123,y:32037,ptlb:Main Color,ptin:_MainColor,glob:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Vector3,id:1996,x:32975,y:32497,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Slider,id:2050,x:33053,y:32775,ptlb:Alpha,ptin:_Alpha,min:0,cur:1,max:1;proporder:2050-1758-344-18-427-435-482-403-468-608;pass:END;sub:END;*/

Shader "Starland/Character Shader Beta1" {
    Properties {
        _Alpha ("Alpha", Range(0, 1)) = 1
        _MainColor ("Main Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _ColorMap ("Color Map", 2D) = "white" {}
        _MormalMap ("Mormal Map", 2D) = "bump" {}
        _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
        _SpecularRange ("Specular Range", Range(0, 1)) = 0.2
        _FresnelColor ("Fresnel Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _FresnelPower ("Fresnel Power", Range(0, 1)) = 0.4
        _FresnelRange ("Fresnel Range", Range(0, 1)) = 1
        _AlphaCutoff ("Alpha Cutoff", Range(0, 10)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Background+1000"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MormalMap; uniform float4 _MormalMap_ST;
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            uniform float _FresnelPower;
            uniform float4 _SpecularColor;
            uniform float _SpecularRange;
            uniform float _FresnelRange;
            uniform float4 _FresnelColor;
            uniform float _AlphaCutoff;
            uniform float4 _MainColor;
            uniform float _Alpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_2234 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_MormalMap,TRANSFORM_TEX(node_2234.rg, _MormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
                float4 node_344 = tex2D(_ColorMap,TRANSFORM_TEX(node_2234.rg, _ColorMap));
                clip(pow(node_344.a,_AlphaCutoff) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 node_1996 = float3(1,1,1);
                float3 w = node_1996*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 node_1757 = saturate(( node_344.rgb > 0.5 ? (1.0-(1.0-2.0*(node_344.rgb-0.5))*(1.0-_MainColor.rgb)) : (2.0*node_344.rgb*_MainColor.rgb) ));
                float3 forwardLight = pow(max(float3(0.0,0.0,0.0), NdotLWrap + w ), node_1757);
                float3 diffuse = forwardLight * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb*2;
////// Emissive:
                float3 emissive = saturate(( saturate(( (pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange) > 0.5 ? (1.0-(1.0-2.0*((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange)-0.5))*(1.0-_FresnelPower)) : (2.0*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange)*_FresnelPower) )) > 0.5 ? (_FresnelColor.rgb + 2.0*saturate(( (pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange) > 0.5 ? (1.0-(1.0-2.0*((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange)-0.5))*(1.0-_FresnelPower)) : (2.0*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange)*_FresnelPower) )) -1.0) : (_FresnelColor.rgb + 2.0*(saturate(( (pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange) > 0.5 ? (1.0-(1.0-2.0*((pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange)-0.5))*(1.0-_FresnelPower)) : (2.0*(pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRange)-_FresnelRange)*_FresnelPower) ))-0.5))));
///////// Gloss:
                float gloss = _SpecularRange;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float3 specularColor = _SpecularColor.rgb;
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                diffuseLight += node_1996; // Diffuse Ambient Light
                finalColor += diffuseLight * node_1757;
                finalColor += specular;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,_Alpha);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            uniform float _AlphaCutoff;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_2235 = i.uv0;
                float4 node_344 = tex2D(_ColorMap,TRANSFORM_TEX(node_2235.rg, _ColorMap));
                clip(pow(node_344.a,_AlphaCutoff) - 0.5);
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            uniform float _AlphaCutoff;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float2 node_2236 = i.uv0;
                float4 node_344 = tex2D(_ColorMap,TRANSFORM_TEX(node_2236.rg, _ColorMap));
                clip(pow(node_344.a,_AlphaCutoff) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
