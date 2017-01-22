Shader "Custom/WaterDistortionShader" {

	Properties{
		_MyTexture("My Texture", 2D) = "white" { }
	// other properties like colors or vectors go here as well
	}



		SubShader{

		// Grab the screen behind the object into _BackgroundTexture
		GrabPass
	{
		"_BackgroundTexture"
	}

		Pass{
		// ... the usual pass state setup ...

		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members grabPos)
#pragma exclude_renderers d3d11
		// compilation directives for this snippet, e.g.:
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f
	{
		float4 grabPos ;
		float4 pos : SV_POSITION;
	};

	float2 vert(appdata_base v) : TEXCOORD0 {
		v2f o;

		o.pos = UnityObjectToClipPos(v.vertex);

		o.grabPos = ComputeGrabScreenPos(o.pos);
		return o;
	}

	sampler2D _BackgroundTexture;

	float4 frag(v2f i) : SV_Target
	{
		//just add u offset, should calculate sine
		return tex2D(_BackgroundTexture, i.grabPos + float(i.y, 0));
		//return 1 - bgcolor;
	}
		ENDCG
		// ... the rest of pass setup ...
	}


	}
}
