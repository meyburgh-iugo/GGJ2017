﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "IUGOGGJ17/WaterDistortion" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader{
		Pass{
		ZTest Always Cull Off ZWrite Off

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	uniform sampler2D _MainTex;
	uniform float4 _MainTex_TexelSize;
	half4 _MainTex_ST;
	uniform float _Speed;
	uniform float _Distortion;
	uniform float _Waves;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	v2f vert(appdata_img v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}

	float4 frag(v2f i) : SV_Target
	{
		float2 offset = i.uv;

		float norm_sintime = sin(_Time.w * _Speed + offset.y * _Distortion) / 2 + 0.5f;
		offset.x += norm_sintime * (1.0f/_Waves);

		return tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(offset, _MainTex_ST));
	}
		ENDCG

	}
	}

		Fallback off

}
