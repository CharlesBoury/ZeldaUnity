// Derivé de Sprite Default
Shader "Sprites/Rays"
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
		[PerRendererData] _BlendMode ("BlendMode", Float) = 0
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

		GrabPass {"_BackgroundTexture"}

		Pass
		{
		CGPROGRAM
			#pragma vertex myVertex
			#pragma fragment myFrag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"

			sampler2D _BackgroundTexture;
			float _BlendMode;

			struct newV2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float4 screenPos : TEXCOORD1; // ajout par rapport a v2f
				UNITY_VERTEX_OUTPUT_STEREO
			};

			newV2f myVertex (appdata_t IN)
			{
				// exactement comme le vertexShader sauf qu'on retourne le newV2f
				newV2f OUT;

				UNITY_SETUP_INSTANCE_ID (IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

			#ifdef UNITY_INSTANCING_ENABLED
				IN.vertex.xy *= _Flip.xy;
			#endif

				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.screenPos = OUT.vertex; // et on rajoute cette ligne
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color * _RendererColor;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 myFrag(newV2f IN) : SV_Target
			{
				float2 grabTexcoord = IN.screenPos.xy / IN.screenPos.w; 
				grabTexcoord.x = (grabTexcoord.x + 1.0) * .5;
				grabTexcoord.y = (grabTexcoord.y + 1.0) * .5; 
				#if UNITY_UV_STARTS_AT_TOP
				grabTexcoord.y = 1.0 - grabTexcoord.y;
				#endif
				
				fixed4 a = tex2D(_BackgroundTexture, grabTexcoord); 
				fixed4 b = SampleSpriteTexture (IN.texcoord) * IN.color;

				fixed4 result;
				// HardLight (a,b)
				// Multiply OR Screen, depending on the blend color (b)
				result.r = b.r < 0.5 ? 2 * a.r * b.r : 1 - 2 * (1 - a.r) * (1 - b.r);
				result.g = b.g < 0.5 ? 2 * a.g * b.g : 1 - 2 * (1 - a.g) * (1 - b.g);
				result.b = b.b < 0.5 ? 2 * a.b * b.b : 1 - 2 * (1 - a.b) * (1 - b.b);
				result.a = b.a;
				result.rgb *= b.a;
				return result;
			}
		ENDCG
		}
	}
}