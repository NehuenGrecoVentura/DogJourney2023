// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewFire"
{
	Properties
	{
		_FireTexture("Fire Texture", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (0,0,0,0)
		_NoiseScale("Noise Scale", Float) = 6
		_Speed("Speed", Float) = 0
		_DissolveSpeed("Dissolve Speed", Vector) = (-0.1,-0.5,0,0)
		_DistortionSpeed("Distortion Speed", Vector) = (0,-0.3,0,0)
		_Distortion("Distortion", Float) = 0
		_DissolveScale("Dissolve Scale", Float) = 0
		_DissolveAmount("Dissolve Amount", Float) = 0
		_NormalScale("NormalScale", Float) = 1
		_FireNormal("Fire Normal", 2D) = "white" {}
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _FireNormal;
		uniform float4 _FireNormal_ST;
		uniform float _NormalScale;
		uniform float4 _Color;
		uniform sampler2D _FireTexture;
		uniform float2 _DistortionSpeed;
		uniform float _Speed;
		uniform float _NoiseScale;
		uniform float _Distortion;
		uniform float _DissolveScale;
		uniform float2 _DissolveSpeed;
		uniform float _DissolveAmount;
		uniform float _Metallic;
		uniform float _Smoothness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float2 voronoihash17( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi17( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash17( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 //		if( d<F1 ) {
			 //			F2 = F1;
			 			float h = smoothstep(0.0, 1.0, 0.5 + 0.5 * (F1 - d) / smoothness); F1 = lerp(F1, d, h) - smoothness * h * (1.0 - h);mg = g; mr = r; id = o;
			 //		} else if( d<F2 ) {
			 //			F2 = d;
			 //		}
			 	}
			}
			return F1;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_FireNormal = i.uv_texcoord * _FireNormal_ST.xy + _FireNormal_ST.zw;
			float2 temp_output_2_0_g2 = uv_FireNormal;
			float2 break6_g2 = temp_output_2_0_g2;
			float temp_output_25_0_g2 = ( pow( 0.5 , 3.0 ) * 0.1 );
			float2 appendResult8_g2 = (float2(( break6_g2.x + temp_output_25_0_g2 ) , break6_g2.y));
			float4 tex2DNode14_g2 = tex2D( _FireNormal, temp_output_2_0_g2 );
			float temp_output_4_0_g2 = _NormalScale;
			float3 appendResult13_g2 = (float3(1.0 , 0.0 , ( ( tex2D( _FireNormal, appendResult8_g2 ).g - tex2DNode14_g2.g ) * temp_output_4_0_g2 )));
			float2 appendResult9_g2 = (float2(break6_g2.x , ( break6_g2.y + temp_output_25_0_g2 )));
			float3 appendResult16_g2 = (float3(0.0 , 1.0 , ( ( tex2D( _FireNormal, appendResult9_g2 ).g - tex2DNode14_g2.g ) * temp_output_4_0_g2 )));
			float3 normalizeResult22_g2 = normalize( cross( appendResult13_g2 , appendResult16_g2 ) );
			float3 Normal44 = normalizeResult22_g2;
			o.Normal = Normal44;
			float mulTime8 = _Time.y * _Speed;
			float2 uv_TexCoord5 = i.uv_texcoord + ( _DistortionSpeed * mulTime8 );
			float simplePerlin2D4 = snoise( uv_TexCoord5*_NoiseScale );
			simplePerlin2D4 = simplePerlin2D4*0.5 + 0.5;
			float2 temp_cast_0 = (simplePerlin2D4).xx;
			float2 lerpResult11 = lerp( i.uv_texcoord , temp_cast_0 , _Distortion);
			float4 tex2DNode1 = tex2D( _FireTexture, lerpResult11 );
			float time17 = 2.0;
			float voronoiSmooth0 = 0.0;
			float2 uv_TexCoord16 = i.uv_texcoord + ( _DissolveSpeed * mulTime8 );
			float2 coords17 = uv_TexCoord16 * _DissolveScale;
			float2 id17 = 0;
			float2 uv17 = 0;
			float voroi17 = voronoi17( coords17, time17, id17, uv17, voronoiSmooth0 );
			float4 FireBase47 = ( tex2DNode1 * ( simplePerlin2D4 * pow( voroi17 , _DissolveAmount ) ) );
			float4 Albedo49 = ( _Color * FireBase47 );
			o.Albedo = Albedo49.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Alpha = tex2DNode1.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
794;73;602;528;2354.023;2192.955;8.033517;False;False
Node;AmplifyShaderEditor.CommentaryNode;52;-3122.21,-294.1825;Inherit;False;3103.455;989.6194;FireBase;21;22;47;1;21;11;20;12;13;4;17;16;5;6;9;15;8;7;14;10;28;27;FireBase;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-3064.011,27.94107;Inherit;False;Property;_Speed;Speed;5;0;Create;True;0;0;0;False;0;False;0;2.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;14;-3061.901,124.3532;Inherit;False;Property;_DissolveSpeed;Dissolve Speed;6;0;Create;True;0;0;0;False;0;False;-0.1,-0.5;-0.1,-0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;7;-3072.21,-138.7897;Inherit;False;Property;_DistortionSpeed;Distortion Speed;7;0;Create;True;0;0;0;False;0;False;0,-0.3;0,-0.3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;8;-2846.559,32.24704;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2626.613,127.911;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-2630.993,-129.2054;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-2353.875,313.2982;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;27;-2313.754,599.6844;Inherit;False;Property;_DissolveScale;Dissolve Scale;9;0;Create;True;0;0;0;False;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2314.352,169.9382;Inherit;False;Property;_NoiseScale;Noise Scale;4;0;Create;True;0;0;0;False;0;False;6;2.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-2355.709,-119.5928;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;17;-1965.076,314.4291;Inherit;True;0;0;1;0;1;False;1;False;True;4;0;FLOAT2;0,0;False;1;FLOAT;2;False;2;FLOAT;2;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.NoiseGeneratorNode;4;-1930.493,13.11931;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1892.065,-93.32233;Inherit;False;Property;_Distortion;Distortion;8;0;Create;True;0;0;0;False;0;False;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-1922.566,-244.1825;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-1869.384,605.5464;Inherit;False;Property;_DissolveAmount;Dissolve Amount;10;0;Create;True;0;0;0;False;0;False;0;0.37;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;20;-1593.867,432.0885;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;0.81;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;11;-1382.986,-146.4144;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1291.066,425.1815;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-910.1071,-169.2666;Inherit;True;Property;_FireTexture;Fire Texture;0;0;Create;True;0;0;0;False;0;False;-1;None;0000000000000000f000000000000000;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-537.3972,403.8061;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;47;-223.1235,-163.2957;Inherit;False;FireBase;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;45;-3100.168,-1058.688;Inherit;False;1008.965;414.4866;Normal;4;43;40;39;44;Normal;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;51;-3093.635,-1761.582;Inherit;False;884.2222;370.7025;Albedo;4;2;48;3;49;Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;43;-3050.168,-1005.253;Inherit;True;Property;_FireNormal;Fire Normal;12;0;Create;True;0;0;0;False;0;False;None;0000000000000000f000000000000000;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.RangedFloatNode;40;-3015.306,-760.2011;Inherit;False;Property;_NormalScale;NormalScale;11;0;Create;True;0;0;0;False;0;False;1;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-3043.635,-1709.978;Inherit;False;Property;_Color;Color;3;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;9.617364,3.679834,0.4990142,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;48;-3016.398,-1506.88;Inherit;False;47;FireBase;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-2720.962,-1706.494;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;39;-2652.557,-1003.256;Inherit;True;NormalCreate;1;;2;e12f7ae19d416b942820e3932b56220f;0;4;1;SAMPLER2D;;False;2;FLOAT2;0,0;False;3;FLOAT;0.5;False;4;FLOAT;2;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;49;-2433.412,-1711.582;Inherit;False;Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;44;-2315.203,-1008.688;Inherit;False;Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;714.5906,-343.8354;Inherit;False;44;Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;54;618.243,-148.78;Inherit;False;Property;_Smoothness;Smoothness;14;0;Create;True;0;0;0;False;0;False;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;623.4832,-244.7178;Inherit;False;Property;_Metallic;Metallic;13;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;50;717.3868,-443.9228;Inherit;False;49;Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;35;1329.793,-250.0923;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;NewFire;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;10;0
WireConnection;15;0;14;0
WireConnection;15;1;8;0
WireConnection;9;0;7;0
WireConnection;9;1;8;0
WireConnection;16;1;15;0
WireConnection;5;1;9;0
WireConnection;17;0;16;0
WireConnection;17;2;27;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;20;0;17;0
WireConnection;20;1;28;0
WireConnection;11;0;12;0
WireConnection;11;1;4;0
WireConnection;11;2;13;0
WireConnection;21;0;4;0
WireConnection;21;1;20;0
WireConnection;1;1;11;0
WireConnection;22;0;1;0
WireConnection;22;1;21;0
WireConnection;47;0;22;0
WireConnection;3;0;2;0
WireConnection;3;1;48;0
WireConnection;39;1;43;0
WireConnection;39;4;40;0
WireConnection;49;0;3;0
WireConnection;44;0;39;0
WireConnection;35;0;50;0
WireConnection;35;1;46;0
WireConnection;35;3;53;0
WireConnection;35;4;54;0
WireConnection;35;9;1;4
ASEEND*/
//CHKSM=64C263650E1F368DC6A9DB53D7235B83CADD0D13