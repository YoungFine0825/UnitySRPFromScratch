Shader "UnitySRPFromScratch/SRP_02_DrawCalls/Unlit"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{
			Tags{"LightMode"="SRP_Pass_Main"}
			HLSLPROGRAM
			#pragma vertex UnlitPassVertex
			#pragma fragment UnlitPassFragment
			#include "Unlit.hlsl"
			ENDHLSL
		}
	}
}
