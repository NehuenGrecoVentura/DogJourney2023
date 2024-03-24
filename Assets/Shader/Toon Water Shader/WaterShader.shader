// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "WaterShader"
{
	Properties
	{
		_SpeedWeave("Speed Weave", Float) = 0
		_DensityAmount("Density Amount", Float) = 0
		_Weaves("Weaves", Float) = 0
		[HDR]_FoamColor("Foam Color", Color) = (0.2728729,0.7873107,0.7924528,0)
		_Foam("Foam", Float) = 0
		_WaterColor("Water Color", Color) = (0.366901,0.6339706,0.8018868,0)
		_SpeedWater("Speed Water", Float) = 0
		_Depth("Depth", Float) = 0
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
			float4 screenPos;
			float2 uv_texcoord;
		};

		uniform float _SpeedWeave;
		uniform float _Weaves;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depth;
		uniform float4 _WaterColor;
		uniform float _DensityAmount;
		uniform float _SpeedWater;
		uniform float _Foam;
		uniform float4 _FoamColor;


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
			float3 break4 = ase_worldPos;
			float mulTime6 = _Time.y * _SpeedWeave;
			float temp_output_10_0 = ( sin( ( ( break4.x + break4.z ) + mulTime6 ) ) * _Weaves );
			float3 temp_cast_0 = (temp_output_10_0).xxx;
			v.vertex.xyz += temp_cast_0;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float eyeDepth29 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float smoothstepResult32 = smoothstep( 0.5 , ( 1.0 - ( eyeDepth29 - ( 0.0 - _Depth ) ) ) , 0.0);
			float mulTime15 = _Time.y * _SpeedWater;
			float time17 = mulTime15;
			float2 coords17 = i.uv_texcoord * _DensityAmount;
			float2 id17 = 0;
			float2 uv17 = 0;
			float voroi17 = voronoi17( coords17, time17, id17, uv17, 0 );
			float3 ase_worldPos = i.worldPos;
			float3 break4 = ase_worldPos;
			float mulTime6 = _Time.y * _SpeedWeave;
			float temp_output_10_0 = ( sin( ( ( break4.x + break4.z ) + mulTime6 ) ) * _Weaves );
			o.Albedo = ( ( smoothstepResult32 + ( _WaterColor + ( pow( voroi17 , _Foam ) * _FoamColor ) ) ) + temp_output_10_0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
163;302;1906;2131;3286.029;927.621;2.424592;True;False
Node;AmplifyShaderEditor.CommentaryNode;12;-2324.318,-859.3876;Inherit;False;1277.891;665.9703;Water Texture;8;22;21;19;17;15;13;55;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1;-2122.942,-47.01257;Inherit;False;1787.263;478.201;Water Movement;9;10;9;8;7;6;5;4;3;2;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-2318.875,-479.0466;Inherit;False;Property;_SpeedWater;Speed Water;6;0;Create;True;0;0;0;False;0;False;0;-13.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;24;-2029.091,-1500.611;Inherit;False;1665.236;576.6536;Depth;8;32;31;30;29;28;27;26;25;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;2;-2072.942,37.97992;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;26;-1928.878,-1076.75;Inherit;False;Property;_Depth;Depth;7;0;Create;True;0;0;0;False;0;False;0;3.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-2294.463,-364.0535;Inherit;False;Property;_DensityAmount;Density Amount;1;0;Create;True;0;0;0;False;0;False;0;12.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;15;-2236.411,-603.4053;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;4;-1795.266,33.38983;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;3;-2034.1,230.9552;Inherit;False;Property;_SpeedWeave;Speed Weave;0;0;Create;True;0;0;0;False;0;False;0;-2.09;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;28;-1591.518,-1177.957;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;17;-1924.097,-699.9705;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;2;False;2;FLOAT;9;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;16;-1798.782,-428.4556;Inherit;False;Property;_Foam;Foam;4;0;Create;True;0;0;0;False;0;False;0;-2.63;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;29;-1979.091,-1429.611;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;30;-1304.925,-1274.941;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;18;-1623.094,-407.847;Inherit;False;Property;_FoamColor;Foam Color;3;1;[HDR];Create;True;0;0;0;False;0;False;0.2728729,0.7873107,0.7924528,0;0.009658977,0.01485997,0.01931795,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;19;-1642.1,-684.354;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-1550.753,2.987549;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;6;-1520.684,234.2987;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-1293.84,58.2887;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;31;-960.8215,-1139.667;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;21;-2274.318,-809.3876;Inherit;False;Property;_WaterColor;Water Color;5;0;Create;True;0;0;0;False;0;False;0.366901,0.6339706,0.8018868,0;0.2663312,0.4689245,0.5943396,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-1393.719,-443.6882;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SinOpNode;9;-1156.872,55.4494;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1122.53,302.2184;Inherit;False;Property;_Weaves;Weaves;2;0;Create;True;0;0;0;False;0;False;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;32;-623.2042,-1149.675;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-1284.685,-803.8047;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-606.3423,-435.4869;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-625.1121,138.0593;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;55;-2057.094,-348.5002;Inherit;False;Property;_Color0;Color 0;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.3496351,0.7915862,0.9150943,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;27;-1774.468,-1297.332;Inherit;False;FLOAT;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-219.4662,-141.4352;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;25;-1969.685,-1323.787;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;122.678,-8.289057;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;WaterShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;13;0
WireConnection;4;0;2;0
WireConnection;28;1;26;0
WireConnection;17;1;15;0
WireConnection;17;2;14;0
WireConnection;30;0;29;0
WireConnection;30;1;28;0
WireConnection;19;0;17;0
WireConnection;19;1;16;0
WireConnection;5;0;4;0
WireConnection;5;1;4;2
WireConnection;6;0;3;0
WireConnection;7;0;5;0
WireConnection;7;1;6;0
WireConnection;31;0;30;0
WireConnection;20;0;19;0
WireConnection;20;1;18;0
WireConnection;9;0;7;0
WireConnection;32;2;31;0
WireConnection;22;0;21;0
WireConnection;22;1;20;0
WireConnection;23;0;32;0
WireConnection;23;1;22;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;27;0;25;0
WireConnection;33;0;23;0
WireConnection;33;1;10;0
WireConnection;0;0;33;0
WireConnection;0;11;10;0
ASEEND*/
//CHKSM=9DC1EC60A1CBA611B5A10B52AA928348928A9301