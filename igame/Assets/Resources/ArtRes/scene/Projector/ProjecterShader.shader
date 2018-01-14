﻿// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "AE_Shader/ProjecterShader" {
	Properties {
		_ShadowTex ("Shadow", 2D) = "gray" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
			Pass {
				ZWrite Off
			
				ColorMask RGB
				Blend DstColor OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct v2f {
					float4 pos:POSITION;
					half4 sproj:TEXCOORD0;
				};
					
				float4x4 unity_Projector;
					
				sampler2D _ShadowTex;

				v2f vert(float4 vertex:POSITION){
					v2f o;
					o.pos = UnityObjectToClipPos(vertex);
					o.sproj = mul(unity_Projector, vertex);
					return o;
				}

				float4 frag(v2f i):COLOR{
					half4 coord = UNITY_PROJ_COORD(i.sproj);

					float4 c = tex2Dproj(_ShadowTex, coord);

					//if(coord.x < 0 || coord.x > 1 || coord.y < 0 || coord.y > 1)//uv只取0~1,避免投影拉伸
					//{
					//	c.r = c.g = c.b = 1;
					//}
					//else
					//{
                        c.a = 0.8 - (c.r + c.g + c.b) / 3.75;
                    //    if(c.a > 0)
                    //    {
                            c.g = 0.38 * c.a;
                            c.r = 0.0 * c.a;
                            c.b = 0.8 * c.a;
                    //    }
                    //    else
                    //    {
					//		c.r = c.g = c.b = 0;
                    //    }
					//}
					return c;
				}
				ENDCG
		}
	}
}
