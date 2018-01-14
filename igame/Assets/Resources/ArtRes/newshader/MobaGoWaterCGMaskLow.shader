// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MobaGo/Water CG Mask Low" 
{
	Properties 
	{
		_Color("_Color", Color) = (1,1,1,1)
		_Texture1("_Texture1(RGB)", 2D) = "black" {}
		_Mask("_Mask(R)", 2D) = "black" {}
	}
    SubShader {
		Tags{  "RenderType"="Opaque" }
        Pass 
		{

            CGPROGRAM
			//#pragma target 2.0
			//#pragma only_renderers d3d9 opengl gles gles3 glcore d3d11 d3d11_9x metal

            #pragma vertex vert
            #pragma fragment frag
			//#pragma multi_compile_fog
            #include "UnityCG.cginc"
			#include "AutoLight.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
			};

            struct v2f {
			  float4 pos : SV_POSITION;
			  float4 pack0 : TEXCOORD0; // _Texture1 _DistortionMap1
            };
			
			float4 _Texture1_ST;

            v2f vert (appdata v)
            {
			  v2f o;
			  UNITY_INITIALIZE_OUTPUT(v2f,o);
			  o.pos = UnityObjectToClipPos (v.vertex);
			  o.pack0.xy = TRANSFORM_TEX(v.texcoord, _Texture1);
			  return o;
            }
			
			fixed4 _Color;
			sampler2D _Texture1;
			sampler2D _Mask;

            fixed4 frag (v2f i) : SV_Target
            {
			  float2 uv_Texture1 = i.pack0.xy;

			  float MaskTex = tex2D(_Mask, uv_Texture1).r;	
			  float4 DistortedMainTex=tex2D(_Texture1, uv_Texture1);	
			  float4 col  = lerp(_Color, DistortedMainTex, MaskTex); 			
			  return col;
            }
            ENDCG

        }
    }
}