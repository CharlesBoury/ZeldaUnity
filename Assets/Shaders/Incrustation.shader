Shader "Charles/Incrustation"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		_Alpha ("Alpha", Range (0,1)) = 1
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend DstColor SrcColor

		Pass
		{
		CGPROGRAM
			#pragma vertex SpriteVert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"

			// User-specified properties
			float _Alpha;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord);
				if (_Alpha < 0) {_Alpha = 0;};
				c.a *= _Alpha;
				fixed4 c2 = lerp(half4(0.5,0.5,0.5,0.5), c, c.a);
				return c2;
			}
		ENDCG
		}
	}
}
