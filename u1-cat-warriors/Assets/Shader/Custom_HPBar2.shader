Shader "Custom/HPBar2" {
	Properties {
		_Fill ("Fill", Float) = 0
		_Prefill ("PreFill", Float) = 0
		_OutlineColor ("OutlineColor", Vector) = (1,1,1,0.5)
		_FillColor ("FillColor", Vector) = (0,1,0,1)
		_PrefillColor ("PrefillColor", Vector) = (1,1,1,1)
		_Scale ("Scale", Float) = 1
		_CornerRadius ("CornerRadus", Float) = 0.15
		_LineThickness ("LineThickness", Float) = 0.065
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}