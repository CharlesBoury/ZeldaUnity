Shader "Sprites/Teinte"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0

		// speciale
		_Teinte("Teinte", Color) = (1,1,1,0)
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
		Blend One OneMinusSrcAlpha

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

			fixed4 _Teinte;

			fixed4 frag(v2f IN) : SV_Target
			{
				 // recuperation de la texture (avec la couleur du spriteRenderer)
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				// recuperation de la couleur de teinte
				fixed4 add = _Teinte;

				// marche comme une incrustation en mode normal dans photoshop
				// cad:
				//     teinte rouge à opacité 10%  = couleur 10% plus rouge
				//     teinte rouge à opacité 100% = couleur rouge
				//     teinte rouge à opacité 0%   = couleur non altérée
				c.rgb = c.rgb*(1-add.a) + add.rgb*add.a;

				c.rgb *= c.a; // application de l'alpha de la texture
				return c;
			}
		ENDCG
		}
	}
}
