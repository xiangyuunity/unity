// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "AE_Model/HalfLambert" {
	Properties {
		_MainTex("Base (RGB) ", 2D) = "white" {}
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,1)
	}

	SubShader {
		Pass {
			Tags{ "Queue" = "Geometry" "LightMode" = "ForwardBase" "RenderType" = "Geometry" "IgnoreProjector" = "true" }
			LOD 200

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _TintColor;

			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
			};

			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				fixed4 color = tex2D(_MainTex, i.texcoord) * _TintColor;

				fixed3 ambient = color.rgb * UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

				fixed halfLambert = dot(worldNormal, worldLightDir) * 0.5 + 0.5;
				fixed3 diffuse = _LightColor0.rgb * color * halfLambert;
				fixed3 o = ambient + diffuse;
				return fixed4(o, color.a);
			}
			ENDCG
		}
		
	}
	FallBack "Diffuse"
}
