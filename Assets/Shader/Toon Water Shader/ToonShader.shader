// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ToonShader"
{
	Properties
	{
		_Speed("Speed", Vector) = (0.3,0.1,0,0)
		_Foams("Foams", Float) = 0
		_Color0("Color 0", Color) = (1,1,1,0)
		_ScaleGenerator("ScaleGenerator", Float) = 2.63
		_Color1("Color 1", Color) = (0,0,0,0)
		_SpeedWeave("Speed Weave", Float) = 0
		_Weaves("Weaves", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _SpeedWeave;
		uniform float _Weaves;
		uniform float4 _Color1;
		uniform float4 _Color0;
		uniform float _Foams;
		uniform float3 _Speed;
		uniform float _ScaleGenerator;


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


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 break27 = ase_worldPos;
			float mulTime30 = _Time.y * _SpeedWeave;
			float3 temp_cast_0 = (( sin( ( ( break27.x + break27.z ) + mulTime30 ) ) * _Weaves )).xxx;
			v.vertex.xyz += temp_cast_0;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner58 = ( _Time.y * _Speed.xy + i.uv_texcoord);
			float simplePerlin2D61 = snoise( panner58*_ScaleGenerator );
			simplePerlin2D61 = simplePerlin2D61*0.5 + 0.5;
			float4 lerpResult65 = lerp( _Color1 , _Color0 , step( _Foams , simplePerlin2D61 ));
			o.Albedo = lerpResult65.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
-35;450;1906;990;889.5488;-1720.222;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;44;-1778.875,480.557;Inherit;False;1787.263;478.201;Water Movement;9;26;32;27;28;30;29;33;35;34;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;26;-1728.875,565.5495;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;-354.9692,1656.057;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;27;-1451.199,560.9594;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;32;-1690.033,758.5248;Inherit;False;Property;_SpeedWeave;Speed Weave;10;0;Create;True;0;0;0;False;0;False;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;56;-320.1823,2030.102;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;57;-332.1823,1830.101;Inherit;False;Property;_Speed;Speed;1;0;Create;True;0;0;0;False;0;False;0.3,0.1,0;1,0.1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PannerNode;58;-49.18225,1655.101;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;59;47.29675,1901.966;Inherit;False;Property;_ScaleGenerator;ScaleGenerator;5;0;Create;True;0;0;0;False;0;False;2.63;3.57;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-1206.686,530.5571;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;30;-1176.617,761.8683;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-949.7728,585.8583;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;178.2968,2018.966;Inherit;False;Property;_Foams;Foams;2;0;Create;True;0;0;0;False;0;False;0;0.74;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;61;238.8177,1649.101;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;3.59;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-778.4632,829.788;Inherit;False;Property;_Weaves;Weaves;11;0;Create;True;0;0;0;False;0;False;0;0.17;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;63;420.1175,1344.981;Inherit;False;Property;_Color0;Color 0;4;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.738,0.9001905,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;62;577.297,1650.966;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;54;-1991.317,-874.4788;Inherit;False;1665.236;576.6536;Depth;8;53;46;47;50;49;45;48;52;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;64;362.1175,1167.981;Inherit;False;Property;_Color1;Color 1;7;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.3295308,0.584525,0.7414444,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;33;-812.8051,583.019;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;25;-1766.895,-223.6204;Inherit;False;1277.891;665.9703;Water Texture;10;19;14;13;10;16;22;20;8;17;18;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-836.296,192.0789;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;65;816.1175,1218.981;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;13;-1084.677,-48.58683;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;53;-585.4302,-523.5429;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1241.359,207.3116;Inherit;False;Property;_Foam;Foam;6;0;Create;True;0;0;0;False;0;False;0;4.11;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;8;-1065.671,227.9202;Inherit;False;Property;_FoamColor;Foam Color;3;1;[HDR];Create;True;0;0;0;False;0;False;0.2728729,0.7873107,0.7924528,0;0.0523763,0.07807495,0.1037736,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;-1716.895,-173.6204;Inherit;False;Property;_WaterColor;Water Color;8;0;Create;True;0;0;0;False;0;False;0.366901,0.6339706,0.8018868,0;0.2663314,0.4689247,0.5943396,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-1891.104,-450.618;Inherit;False;Property;_Depth;Depth;12;0;Create;True;0;0;0;False;0;False;0;3.94;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;51;-187.751,232.0389;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VoronoiNode;10;-1274.054,-72.25736;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;2;False;2;FLOAT;9;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.RangedFloatNode;20;-1734.985,41.81863;Inherit;False;Property;_SpeedWater;Speed Water;9;0;Create;True;0;0;0;False;0;False;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;22;-1532.159,48.00752;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;46;-1931.911,-697.6553;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;48;-1267.151,-648.8089;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;45;-1941.317,-803.4792;Inherit;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;47;-1736.694,-671.1997;Inherit;False;FLOAT;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleSubtractOpNode;49;-1553.744,-551.8253;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;52;-923.0475,-513.5353;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1527.639,146.8783;Inherit;False;Property;_DensityAmount;Density Amount;0;0;Create;True;0;0;0;False;0;False;0;34.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-281.0453,665.6289;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-727.2623,-168.0375;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1742.369,523.2742;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;ToonShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;27;0;26;0
WireConnection;58;0;55;0
WireConnection;58;2;57;0
WireConnection;58;1;56;0
WireConnection;28;0;27;0
WireConnection;28;1;27;2
WireConnection;30;0;32;0
WireConnection;29;0;28;0
WireConnection;29;1;30;0
WireConnection;61;0;58;0
WireConnection;61;1;59;0
WireConnection;62;0;60;0
WireConnection;62;1;61;0
WireConnection;33;0;29;0
WireConnection;14;0;13;0
WireConnection;14;1;8;0
WireConnection;65;0;64;0
WireConnection;65;1;63;0
WireConnection;65;2;62;0
WireConnection;13;0;10;0
WireConnection;13;1;17;0
WireConnection;53;2;52;0
WireConnection;51;0;19;0
WireConnection;51;1;53;0
WireConnection;10;1;22;0
WireConnection;10;2;16;0
WireConnection;22;0;20;0
WireConnection;48;0;45;0
WireConnection;48;1;49;0
WireConnection;47;0;46;0
WireConnection;49;1;50;0
WireConnection;52;0;48;0
WireConnection;34;0;33;0
WireConnection;34;1;35;0
WireConnection;19;0;18;0
WireConnection;19;1;14;0
WireConnection;0;0;65;0
WireConnection;0;11;34;0
ASEEND*/
//CHKSM=CF3FB31E6780496B1D2F3EB23166EE50BD57D931