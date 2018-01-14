Shader "AE_Model/Player_AlbedoSolid" {
Properties {
	//_Color ("Main Color", Color) = (1,1,1,1)
	_Color("Tint (A = Opacity)", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) ", 2D) = "white" {}//Gloss (A = Transparency)
	_Illum ("Illumin (A)", 2D) = "white" {}
	_EmissionLM ("Emission (Lightmapper)", Float) = 0
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
	_RimColor2 ("Rim Color2", Color) = (0.58,0.61176,0.8431,0.6)
    _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    _AttenPower ("Atten Power", Range(0.0,5.0)) = 2.0
    _EnableRim ("Enable Rim",Range(0,1)) = 1
    //_nLayer ("Enable int",float) = 1.0
    //_Floor("Render Floor", int) = 1
}
//Category {
		
		
	SubShader {
	Tags { "Queue" = "AlphaTest"  "IgnoreProjector"="true" "RenderType"="Opaque" }	//Opaque //TransParent //
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		//Pass{
			//Cull Front Geometry+501
		//	ZWrite Off
		//	Tags { "Queue" = "Geometry+502" }
		//	ZTest Greater
		//	Cull back
			
		//	Color[_RimColor2]
			
		//}	
		
	CGPROGRAM
	#pragma surface surf SimpleLambert //alphatest:_Cutoff //投影拾取对象不能够使用alphatest

	//#pragma surface surf Lambert alphatest:_Cutoff
		sampler2D _MainTex;
		sampler2D _Illum;
		fixed4 _Color;
		float4 _RimColor;
		float _RimPower;
		float  _EnableRim;
		float _AttenPower;
		bool	_bZtest;
		fixed _Cutoff;
		struct Input {
			float2 uv_MainTex;
			float2 uv_Illum;
			float3 viewDir;
		};
		
		half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten)// 
    	{
    		//half3 lightDir = float(1,1,1);
    		half NdotL = dot(s.Normal, lightDir);
    		half atten1 = 1.2;
    		half4 c;
    		c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten1 *_AttenPower);
    		c.a = s.Alpha;
    		return c;   		
    	}// if (col.a <= _Cutoff) clip(-1);

    	
		void surf (Input IN, inout SurfaceOutput o) {
			//fixed4 tex = tex2D(_MainTex, IN.uv_MainTex)*_Color;
			//fixed4 c = tex * _Color;

			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			if (c.a <= _Cutoff)
    			clip(-1);

			half rim = 1 - saturate(dot (normalize(IN.viewDir), o.Normal));
			o.Emission = c.rgb;// * normalize(IN.viewDir);// * (1.0 - rim)*tex2D(_Illum, IN.uv_Illum).a;;//* dot (normalize(IN.viewDir), o.Normal);//tex2D(_Illum, IN.uv_Illum).a;
			if(_EnableRim)
			{
				o.Emission = o.Emission + _RimColor.rgb * pow (rim, _RimPower); 
			}
			o.Albedo = (0.5 - rim)*0.5 * c.rgb;//tex2D(_Illum, IN.uv_Illum).a;//c.rgb;// * tex2D(_Illum, IN.uv_Illum).a;
			if(_EnableRim)
			{
				o.Albedo = o.Albedo + _RimColor.rgb * pow (rim, _AttenPower); 
			}

		}
	ENDCG
	}
	FallBack "Self-Illumin/VertexLit"
 }

//}
// Lambert// alphatest:_Cutoff