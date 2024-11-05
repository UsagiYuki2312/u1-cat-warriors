Shader "Custom/SpriteHighlight" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Highlight ("Highlight", Float) = 0
		_HighlightColor ("Highlight Color", Vector) = (1,1,1,1)
		_Morale ("Morale", Float) = 0
		_MoraleColor ("Morale Color", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}