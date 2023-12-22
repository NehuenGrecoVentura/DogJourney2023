// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "s_Grass"
{
	Properties
	{
		_GrassTexture("Grass Texture", 2D) = "white" {}
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 13.6
		_Float1("Float 1", Float) = 4
		_Float0("Float 0", Float) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _Float0;
		uniform float _Float1;
		uniform sampler2D _GrassTexture;
		uniform float4 _GrassTexture_ST;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertexNormal = v.normal.xyz;
			float mulTime99 = _Time.y * 3.0;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 temp_cast_1 = (_SinTime.w).xx;
			float2 uv_TexCoord100 = v.texcoord.xy * ase_vertex3Pos.xy + temp_cast_1;
			float lerpResult104 = lerp( cos( mulTime99 ) , 0.0 , ( 1.0 - uv_TexCoord100.x ));
			float4 appendResult106 = (float4(0.0 , 0.0 , ( ( ( ( 1.0 - v.texcoord.xy ) * _Float0 ) * ase_vertexNormal.z ) + ( lerpResult104 / _Float1 ) )));
			v.vertex.xyz += appendResult106.xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_GrassTexture = i.uv_texcoord * _GrassTexture_ST.xy + _GrassTexture_ST.zw;
			float4 tex2DNode1 = tex2D( _GrassTexture, uv_GrassTexture );
			o.Albedo = tex2DNode1.rgb;
			o.Alpha = tex2DNode1.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
718;73;863;651;1983.737;1732.549;3.752362;True;False
Node;AmplifyShaderEditor.CommentaryNode;95;-782.5458,420.5423;Inherit;False;1341.31;812.994;Movimiento del pasto;11;105;104;103;102;101;100;99;98;97;96;115;Movimiento del paso;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-732.5458,714.5298;Inherit;False;Constant;_Viento;Viento;4;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;97;-608.1925,1054.537;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinTimeNode;96;-712.4829,848.165;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;100;-242.7556,886.4903;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;114;-955.2505,161.0332;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;99;-422.6916,720.1287;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;113;-600.7448,160.588;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-361.2764,169.9516;Inherit;False;Property;_Float0;Float 0;8;0;Create;True;0;0;0;False;0;False;0.5;0.79;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;102;-229.0425,720.1903;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;101;74.7485,910.6405;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;103;194.0345,789.2189;Inherit;False;Property;_Float1;Float 1;7;0;Create;True;0;0;0;False;0;False;4;13.56;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;108;-45.21417,-85.29626;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;111;-40.9649,40.49854;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;104;-60.46159,664.9787;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;105;251.9685,658.1912;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;164.0542,-79.26507;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;56;-511.7564,-928.7629;Inherit;False;371;280;Albedo y (A) Opacity;1;1;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;57;-2146.631,339.1358;Inherit;False;273;229;Manejo los vértices del modelo;1;42;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;53;-2338.833,-115.6982;Inherit;False;499.9932;410.2519;Posición del jugador;3;26;25;24;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;54;-2256.023,590.135;Inherit;False;461;200;Utilizando este nodo podemos generar una máscara;1;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;59;-2335.888,867.4614;Inherit;False;752.2188;245.6584;Podríamos utilizarlo para usar una misma dirección para todos los pastos. Es decir, viento;1;58;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;112;207.5253,317.2588;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector3Node;24;-2288.833,-65.69817;Inherit;False;Property;_MyPos;MyPos;1;0;Create;True;0;0;0;False;0;False;0,0,0;2.361787,-0.2658792,1.122599;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PosVertexDataNode;42;-2096.631,389.1358;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;25;-2013.839,-12.44629;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;115;-33.60877,469.3516;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;26;-2279.84,115.5537;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2133.693,647.368;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-455.5925,-872.5992;Inherit;True;Property;_GrassTexture;Grass Texture;0;0;Create;True;0;0;0;False;0;False;-1;80ec8b27e8b294a47a95a405d990f0cb;8db432d822f32ea479cc5be31b031711;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;58;-2053.71,934.1199;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;106;465.9206,19.24978;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;746.6682,-341.9202;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;s_Grass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;13.6;10;25;False;1;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;2;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;100;0;97;0
WireConnection;100;1;96;4
WireConnection;99;0;98;0
WireConnection;113;0;114;0
WireConnection;102;0;99;0
WireConnection;101;0;100;1
WireConnection;108;0;113;0
WireConnection;108;1;109;0
WireConnection;104;0;102;0
WireConnection;104;2;101;0
WireConnection;105;0;104;0
WireConnection;105;1;103;0
WireConnection;110;0;108;0
WireConnection;110;1;111;3
WireConnection;112;0;110;0
WireConnection;112;1;105;0
WireConnection;25;0;24;0
WireConnection;25;1;26;0
WireConnection;106;2;112;0
WireConnection;0;0;1;0
WireConnection;0;9;1;4
WireConnection;0;11;106;0
ASEEND*/
//CHKSM=01000984147B60C933CA90D844123EA9FB9E2217