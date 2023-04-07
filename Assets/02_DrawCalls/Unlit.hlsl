#ifndef CUSTOM_UNLIT_PASS_INCLUDE
#define CUSTOM_UNLIT_PASS_INCLUDE

#include "../00_Common/Common.hlsl"

CBUFFER_START(UnityPerMaterial)
	float4 _Color;
CBUFFER_END

float4 UnlitPassVertex(float3 positionL:POSITION) :SV_POSITION
{
	return TransformObjectToHClip(positionL);
}

float4 UnlitPassFragment() : SV_Target
{
	return _Color;
}

#endif