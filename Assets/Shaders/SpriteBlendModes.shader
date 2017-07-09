// Derivé de Sprite Default
Shader "Sprites/BlendModes"
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

		GrabPass { }

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

			sampler2D _GrabTexture;
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


			// Code des différents modes
			// sources :
			// https://helpx.adobe.com/photoshop/using/blending-modes.html
			// https://mouaif.wordpress.com/2009/01/05/photoshop-math-with-glsl-shaders/

			fixed4 Darken (fixed4 a, fixed4 b)
			{
				fixed4 result;
				result.r = min(a.r, b.r);
				result.g = min(a.g, b.g);
				result.b = min(a.b, b.b);
				return result;

			}
			fixed4 Lighten (fixed4 a, fixed4 b)
			{
				fixed4 result;
				result.r = max(a.r, b.r);
				result.g = max(a.g, b.g);
				result.b = max(a.b, b.b);
				return result;

			}

			fixed4 Multiply (fixed4 a, fixed4 b)
			{
				fixed4 result;
				result.r = a.r * b.r;
				result.g = a.g * b.g;
				result.b = a.b * b.b;
				return result;
			}

			fixed4 Screen (fixed4 a, fixed4 b)
			{
				fixed4 result;
				result.r = 1 - (1-a.r)*(1-b.r);
				result.g = 1 - (1-a.g)*(1-b.g);
				result.b = 1 - (1-a.b)*(1-b.b);
				return result;
			}

			fixed4 Overlay (fixed4 a, fixed4 b)
			{
				// Multiply OR Screen, depending on the base color (a)
				fixed4 result;
				result.r = a.r < 0.5 ? 2 * a.r * b.r : 1 - 2 * (1 - a.r) * (1 - b.r);
				result.g = a.g < 0.5 ? 2 * a.g * b.g : 1 - 2 * (1 - a.g) * (1 - b.g);
				result.b = a.b < 0.5 ? 2 * a.b * b.b : 1 - 2 * (1 - a.b) * (1 - b.b);
				return result;
			}

			fixed4 HardLight (fixed4 a, fixed4 b)
			{
				// Multiply OR Screen, depending on the blend color (b)
				fixed4 result;
				result.r = b.r < 0.5 ? 2 * a.r * b.r : 1 - 2 * (1 - a.r) * (1 - b.r);
				result.g = b.g < 0.5 ? 2 * a.g * b.g : 1 - 2 * (1 - a.g) * (1 - b.g);
				result.b = b.b < 0.5 ? 2 * a.b * b.b : 1 - 2 * (1 - a.b) * (1 - b.b);
				return result;
			}

			fixed4 SoftLight (fixed4 a, fixed4 b)
			{
				fixed4 result;
				result.r = b.r < 0.5 ? 2*a.r*b.r + a.r*a.r*(1-2*b.r):2*a.r*(1-b.r)+sqrt(a.r)*(2*b.r-1);
				result.g = b.g < 0.5 ? 2*a.g*b.g + a.g*a.g*(1-2*b.g):2*a.g*(1-b.g)+sqrt(a.g)*(2*b.g-1);
				result.b = b.b < 0.5 ? 2*a.b*b.b + a.b*a.b*(1-2*b.b):2*a.b*(1-b.b)+sqrt(a.b)*(2*b.b-1);
				return result;
			}

			fixed4 myFrag(newV2f IN) : SV_Target
			{
				float2 grabTexcoord = IN.screenPos.xy / IN.screenPos.w; 
				grabTexcoord.x = (grabTexcoord.x + 1.0) * .5;
				grabTexcoord.y = (grabTexcoord.y + 1.0) * .5; 
				#if UNITY_UV_STARTS_AT_TOP
				grabTexcoord.y = 1.0 - grabTexcoord.y;
				#endif
				
				fixed4 a = tex2D(_GrabTexture, grabTexcoord); 
				fixed4 b = SampleSpriteTexture (IN.texcoord) * IN.color;

				fixed4 result;
 				switch (_BlendMode)
 				{
 					case 0:
 						result = b; break; // Normal
 					case 1:
 						result = Darken(a,b); break;
 					case 2:
 						result = Lighten(a,b); break;
 					case 3:
 						result = Multiply(a,b); break;
 					case 4:
 						result = Screen(a,b); break;
 					case 5:
 						result = Overlay(a,b); break;
 					case 6:
 						result = HardLight(a,b); break;
 					case 7:
 						result = SoftLight(a,b); break;
 					default:
 						result = b; break;
 				}
				result.a = b.a;
				result.rgb *= b.a;
				return result;
			}
		ENDCG
		}
	}
}