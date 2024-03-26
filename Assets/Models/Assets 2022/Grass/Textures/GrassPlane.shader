// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "GrassPlane"
{
	Properties
	{
		_Grassplane("Grass plane", 2D) = "white" {}
		_Viento("Viento", Float) = 3
		_Float0("Float 0", Float) = 4
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		AlphaToMask On
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Viento;
		uniform float3 _Vector0;
		uniform float _Float0;
		uniform sampler2D _Grassplane;
		uniform float4 _Grassplane_ST;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime7 = _Time.y * _Viento;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 uv_TexCoord8 = v.texcoord.xy * ( ase_vertex3Pos * _Vector0 ).xy;
			float lerpResult12 = lerp( cos( mulTime7 ) , 0.0 , ( 1.0 - uv_TexCoord8.x ));
			float3 temp_cast_1 = (( lerpResult12 / _Float0 )).xxx;
			v.vertex.xyz += temp_cast_1;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Grassplane = i.uv_texcoord * _Grassplane_ST.xy + _Grassplane_ST.zw;
			float4 tex2DNode3 = tex2D( _Grassplane, uv_Grassplane );
			o.Albedo = tex2DNode3.rgb;
			o.Alpha = tex2DNode3.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
401;134;1906;699;2353.506;-541.3089;1.641388;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;6;-1245.361,1079.538;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;15;-1242.186,1399.829;Inherit;False;Property;_Vector0;Vector 0;3;0;Create;True;0;0;0;False;0;False;0,0,0;1,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;5;-1139.976,502.0173;Inherit;False;Property;_Viento;Viento;1;0;Create;True;0;0;0;False;0;False;3;2.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-977.7906,1078.331;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;7;-830.1219,507.6163;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;8;-650.186,673.9777;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CosOpNode;10;-636.4728,507.6779;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;9;-332.6817,698.1279;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-213.3955,576.7062;Inherit;False;Property;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;4;13.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;-493.4729,466.6779;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinTimeNode;4;-1119.913,635.6523;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-511.6512,78.61902;Inherit;True;Property;_Grassplane;Grass plane;0;0;Create;True;0;0;0;False;0;False;-1;5009a8cb0f8bd554492e2b7f3b11ba80;5009a8cb0f8bd554492e2b7f3b11ba80;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;13;-155.4615,445.6787;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;510.0331,48.39729;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;GrassPlane;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;True;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;6;0
WireConnection;16;1;15;0
WireConnection;7;0;5;0
WireConnection;8;0;16;0
WireConnection;10;0;7;0
WireConnection;9;0;8;1
WireConnection;12;0;10;0
WireConnection;12;2;9;0
WireConnection;13;0;12;0
WireConnection;13;1;11;0
WireConnection;0;0;3;0
WireConnection;0;9;3;4
WireConnection;0;11;13;0
ASEEND*/
//CHKSM=67ABA751C0AB5C0CD7942BB6439FF4A217D9BCB6