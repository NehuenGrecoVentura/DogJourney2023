// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Waterfall"
{
	Properties
	{
		_SpeedWeave1("Speed Weave", Float) = 0
		_Weaves1("Weaves", Float) = 0
		_DensityAmount1("Density Amount", Float) = 0
		[HDR]_FoamColor1("Foam Color", Color) = (0.2728729,0.7873107,0.7924528,0)
		_Foam1("Foam", Float) = 0
		_WaterColor1("Water Color", Color) = (0.366901,0.6339706,0.8018868,0)
		_SpeedWater1("Speed Water", Float) = 0
		_Depth1("Depth", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float _SpeedWeave1;
		uniform float _Weaves1;
		uniform float4 _WaterColor1;
		uniform float _DensityAmount1;
		uniform float _SpeedWater1;
		uniform float _Foam1;
		uniform float4 _FoamColor1;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depth1;


		float2 voronoihash12( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi12( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
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
			 		float2 o = voronoihash12( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 break3_g1 = ase_worldPos;
			float mulTime6_g1 = _Time.y * _SpeedWeave1;
			float3 temp_cast_0 = (( sin( ( ( break3_g1.x + break3_g1.z ) + mulTime6_g1 ) ) * _Weaves1 )).xxx;
			v.vertex.xyz += temp_cast_0;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime14 = _Time.y * _SpeedWater1;
			float time12 = mulTime14;
			float2 coords12 = i.uv_texcoord * _DensityAmount1;
			float2 id12 = 0;
			float2 uv12 = 0;
			float voroi12 = voronoi12( coords12, time12, id12, uv12, 0 );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth17 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float smoothstepResult6 = smoothstep( 0.5 , ( 1.0 - ( eyeDepth17 - ( 0.0 - _Depth1 ) ) ) , 0.0);
			o.Albedo = ( ( _WaterColor1 + ( pow( voroi12 , _Foam1 ) * _FoamColor1 ) ) + smoothstepResult6 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
616;78;1906;978;1169.508;323.2422;1.342063;True;False
Node;AmplifyShaderEditor.CommentaryNode;3;-2001.795,-480.2827;Inherit;False;1277.891;665.9703;Water Texture;10;22;21;14;13;12;9;8;7;5;4;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;2;-2226.217,-1131.141;Inherit;False;1665.236;576.6536;Depth;8;20;19;18;17;16;15;10;6;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1969.885,-214.8438;Inherit;False;Property;_SpeedWater1;Speed Water;7;0;Create;True;0;0;0;False;0;False;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;14;-1767.059,-208.6549;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1762.539,-109.7842;Inherit;False;Property;_DensityAmount1;Density Amount;3;0;Create;True;0;0;0;False;0;False;0;34.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2126.004,-707.2804;Inherit;False;Property;_Depth1;Depth;8;0;Create;True;0;0;0;False;0;False;0;3.94;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;17;-2176.217,-1060.142;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-1788.644,-808.4877;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;12;-1508.954,-328.9198;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;2;False;2;FLOAT;9;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;7;-1476.259,-49.35082;Inherit;False;Property;_Foam1;Foam;5;0;Create;True;0;0;0;False;0;False;0;4.11;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-1300.572,-28.74225;Inherit;False;Property;_FoamColor1;Foam Color;4;1;[HDR];Create;True;0;0;0;False;0;False;0.2728729,0.7873107,0.7924528,0;0.0523763,0.07807495,0.1037736,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;16;-1502.051,-905.4713;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;5;-1319.578,-305.2492;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-1071.197,-64.58353;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;20;-1157.948,-770.1978;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;9;-1951.795,-430.2827;Inherit;False;Property;_WaterColor1;Water Color;6;0;Create;True;0;0;0;False;0;False;0.366901,0.6339706,0.8018868,0;0.2663314,0.4689247,0.5943396,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;6;-820.331,-780.2054;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-962.163,-424.6998;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-422.6518,-24.62354;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;15;-2166.811,-954.3175;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;18;-1971.594,-927.8619;Inherit;False;FLOAT;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;1;-421,363;Inherit;False;WaterFunction;0;;1;e66c94c7cc31bb74999100cfe6082898;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Waterfall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;13;0
WireConnection;19;1;10;0
WireConnection;12;1;14;0
WireConnection;12;2;21;0
WireConnection;16;0;17;0
WireConnection;16;1;19;0
WireConnection;5;0;12;0
WireConnection;5;1;7;0
WireConnection;4;0;5;0
WireConnection;4;1;8;0
WireConnection;20;0;16;0
WireConnection;6;2;20;0
WireConnection;22;0;9;0
WireConnection;22;1;4;0
WireConnection;11;0;22;0
WireConnection;11;1;6;0
WireConnection;18;0;15;0
WireConnection;0;0;11;0
WireConnection;0;11;1;0
ASEEND*/
//CHKSM=61F30E0CFC7BEDCC684335856B1C210EE09A60C5