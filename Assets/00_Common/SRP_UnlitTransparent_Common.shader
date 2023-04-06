Shader "UnitySRPFromScratch/SRP_00_Common/UnlitTransparent"
{
	Properties
	{
		_MainTex ("_MainTex (RGBA)", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Cull Off Lighting Off ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Tags { "LightMode" = "SRP_Pass_Main" }

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_VP, mul(UNITY_MATRIX_M, float4(v.vertex.xyz, 1.0)));
				o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				o.color = v.color;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv) * _Color;
				return col;
			}
			ENDHLSL
		}
	}
}
