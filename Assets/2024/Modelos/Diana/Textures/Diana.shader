// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Diana"
{
	Properties
	{
		_Diana_Diana_BaseColor("Diana_Diana_BaseColor", 2D) = "white" {}
		_Diana_Diana_Normal("Diana_Diana_Normal", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Diana_Diana_Normal;
		uniform float4 _Diana_Diana_Normal_ST;
		uniform sampler2D _Diana_Diana_BaseColor;
		uniform float4 _Diana_Diana_BaseColor_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Diana_Diana_Normal = i.uv_texcoord * _Diana_Diana_Normal_ST.xy + _Diana_Diana_Normal_ST.zw;
			o.Normal = tex2D( _Diana_Diana_Normal, uv_Diana_Diana_Normal ).rgb;
			float2 uv_Diana_Diana_BaseColor = i.uv_texcoord * _Diana_Diana_BaseColor_ST.xy + _Diana_Diana_BaseColor_ST.zw;
			o.Albedo = tex2D( _Diana_Diana_BaseColor, uv_Diana_Diana_BaseColor ).rgb;
			o.Smoothness = 0.0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
174;788;1906;600;815.834;-33.56262;1;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-474,4;Inherit;True;Property;_Diana_Diana_BaseColor;Diana_Diana_BaseColor;0;0;Create;True;0;0;0;False;0;False;-1;ddc3cb7d1d1d9f242a17bc73af5c8399;ddc3cb7d1d1d9f242a17bc73af5c8399;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-476.4617,230.4137;Inherit;True;Property;_Diana_Diana_Normal;Diana_Diana_Normal;1;0;Create;True;0;0;0;False;0;False;-1;09a2635b36a3c0341907ab28ff1e04e3;09a2635b36a3c0341907ab28ff1e04e3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-342.5254,445.0906;Inherit;False;Constant;_Smoothness;Smoothness;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;5;261,12;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Diana;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;1;0
WireConnection;5;1;2;0
WireConnection;5;4;3;0
ASEEND*/
//CHKSM=9D791C583381695C444555DA269F6494BA77BFEA