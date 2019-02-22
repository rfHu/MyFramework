// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "vDesign/ADT Base Floor"
{
	Properties
	{
		_Lmm("L(mm)", Float) = 1000
		_Wmm("W(mm)", Float) = 1000
		_Texture_L("Texture_L", Int) = 1
		_Texture_W("Texture_W", Int) = 1
		[NoScaleOffset]_Albedo("Albedo", 2D) = "white" {}
		_Albedo_Color("Albedo_Color ", Color) = (1,1,1,0)
		[NoScaleOffset]_Normal("Normal", 2D) = "bump" {}
		_Normal_Scale("Normal_Scale", Float) = 1
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.5
		_AO_Scale("AO_Scale", Range( 0 , 1)) = 1
		_Angle("Angle", Float) = 0
		_WorldPositionX("World Position X", Float) = 0
		_WorldPositionZ("World Position Z", Float) = 0
		_Emission("Emission", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float _Normal_Scale;
		uniform sampler2D _Normal;
		uniform float _WorldPositionX;
		uniform float _WorldPositionZ;
		uniform float _Angle;
		uniform float _Lmm;
		uniform float _Wmm;
		uniform float4 _Albedo_Color;
		uniform sampler2D _Albedo;
		uniform int _Texture_L;
		uniform int _Texture_W;
		uniform float _Emission;
		uniform float _Metallic;
		uniform float _Smoothness;
		uniform float _AO_Scale;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult11 = (float2(( ase_worldPos.x - _WorldPositionX ) , ( ase_worldPos.z - _WorldPositionZ )));
			float cos41 = cos( ( ( _Angle / 180.0 ) * UNITY_PI ) );
			float sin41 = sin( ( ( _Angle / 180.0 ) * UNITY_PI ) );
			float2 rotator41 = mul( appendResult11 - float2( 0,0 ) , float2x2( cos41 , -sin41 , sin41 , cos41 )) + float2( 0,0 );
			float2 appendResult28 = (float2(_Lmm , _Wmm));
			float2 temp_output_59_0 = ( appendResult28 / float2( 1000,1000 ) );
			o.Normal = UnpackScaleNormal( tex2D( _Normal, ( ( rotator41 / float2( 5,5 ) ) / temp_output_59_0 ) ), _Normal_Scale );
			float2 appendResult39 = (float2((float)_Texture_L , (float)_Texture_W));
			o.Albedo = ( _Albedo_Color * tex2D( _Albedo, ( ( rotator41 / appendResult39 ) / temp_output_59_0 ) ) ).rgb;
			o.Emission = ( float4(1,0.4661258,0.1764706,0) * _Emission ).rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			float2 temp_output_74_0 = frac( ( ( rotator41 / float2( 1,1 ) ) / temp_output_59_0 ) );
			float clampResult114 = clamp( ( sin( ( (temp_output_74_0).x * 3.14 ) ) * 50.0 ) , 0.0 , 1.0 );
			float clampResult116 = clamp( ( sin( ( (temp_output_74_0).y * 3.14 ) ) * 50.0 ) , 0.0 , 1.0 );
			float lerpResult128 = lerp( 1.0 , min( clampResult114 , clampResult116 ) , _AO_Scale);
			o.Occlusion = lerpResult128;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15600
1925;22;1906;1004;758.6534;-120.9495;1.343224;True;False
Node;AmplifyShaderEditor.CommentaryNode;89;-2852.635,-768.5168;Float;False;931.5367;470.6303;World_UV;10;41;46;11;132;134;5;44;48;136;137;;0,1,0.5448275,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-2795.325,-401.0652;Float;False;Property;_Angle;Angle;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;132;-2809.191,-710.2618;Float;False;Property;_WorldPositionX;World Position X;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;5;-2808.189,-542.9191;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;134;-2808.191,-623.2618;Float;False;Property;_WorldPositionZ;World Position Z;13;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;48;-2568.802,-408.8326;Float;False;2;0;FLOAT;0;False;1;FLOAT;180;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;136;-2570.99,-664.9493;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;137;-2575.99,-557.9493;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-2474.469,331.4603;Float;False;Property;_Wmm;W(mm);1;0;Create;True;0;0;False;0;1000;900;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-2476.469,230.4602;Float;False;Property;_Lmm;L(mm);0;0;Create;True;0;0;False;0;1000;600;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;46;-2422.299,-399.7332;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;11;-2406.474,-579.604;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;125;-1478.696,469.9316;Float;False;510;234;AO_UV;3;72;73;74;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RotatorNode;41;-2198.285,-561.8807;Float;True;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;28;-2221.298,268.3133;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;72;-1446.627,560.0208;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;59;-2017.832,264.177;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;1000,1000;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;73;-1268.939,559.1574;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FractNode;74;-1116.286,562.5135;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;126;-852.4943,433.9708;Float;False;1504;440.6001;AO;14;129;128;123;116;114;115;119;111;117;124;110;118;75;76;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ComponentMaskNode;76;-807.6144,618.1478;Float;False;False;True;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;75;-811.4301,500.5516;Float;False;True;False;False;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;21;-2294.627,-37.3109;Float;False;Property;_Texture_W;Texture_W;3;0;Create;True;0;0;False;0;1;2;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;-549.5496,502.6213;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;3.14;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;-558.2595,626.8796;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;3.14;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;20;-2293.628,-137.311;Float;False;Property;_Texture_L;Texture_L;2;0;Create;True;0;0;False;0;1;4;0;1;INT;0
Node;AmplifyShaderEditor.SinOpNode;117;-384.9867,629.52;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;124;-559.8807,753.2372;Float;False;Constant;_Float1;Float 1;11;0;Create;True;0;0;False;0;50;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;39;-2057.283,-122.2413;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SinOpNode;111;-378.1289,501.6353;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;92;-1450.313,-225.116;Float;False;338.7999;196.9;Albedo_UV;2;36;12;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-231.4904,627.3309;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;12;-1420.072,-163.3038;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;91;-1458.127,158.7962;Float;False;355.6555;208.2332;Normal_UV;2;57;56;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;-226.6325,503.4465;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;114;-65.33213,501.6467;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;116;-64.18982,626.5311;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;90;-574.7593,-694.8267;Float;False;910.8221;794.1785;Standard;7;2;22;23;24;38;1;37;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;36;-1261.54,-163.0432;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;56;-1434.137,230.2394;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;5,5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;57;-1265.449,230.376;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;37;-293.8167,-633.7548;Float;False;Property;_Albedo_Color;Albedo_Color ;5;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;139;429.0063,952.1859;Float;False;Constant;_Color0;Color 0;19;0;Create;True;0;0;False;0;1,0.4661258,0.1764706,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;138;406.0064,1136.187;Float;False;Property;_Emission;Emission;14;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMinOpNode;123;172.789,534.3405;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;129;154.5221,766.8312;Float;False;Property;_AO_Scale;AO_Scale;10;0;Create;True;0;0;False;0;1;0.183;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-538.0534,-197.4296;Float;False;Property;_Normal_Scale;Normal_Scale;7;0;Create;True;0;0;False;0;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-307.877,-447.156;Float;True;Property;_Albedo;Albedo;4;1;[NoScaleOffset];Create;True;0;0;False;0;None;cf4c478f45820cf4abd26539733688a1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;27.87248,-82.12428;Float;False;Property;_Metallic;Metallic;8;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;140;689.0067,1087.187;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;128;454.7224,530.5309;Float;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-312.8769,-249.1559;Float;True;Property;_Normal;Normal;6;1;[NoScaleOffset];Create;True;0;0;False;0;None;655002f19d44bac4298ac3708bdb74d4;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;24.87248,-9.12435;Float;False;Property;_Smoothness;Smoothness;9;0;Create;True;0;0;False;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;74.98347,-535.5547;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1212.608,116.3631;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;vDesign/ADT Base Floor;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;48;0;44;0
WireConnection;136;0;5;1
WireConnection;136;1;132;0
WireConnection;137;0;5;3
WireConnection;137;1;134;0
WireConnection;46;0;48;0
WireConnection;11;0;136;0
WireConnection;11;1;137;0
WireConnection;41;0;11;0
WireConnection;41;2;46;0
WireConnection;28;0;33;0
WireConnection;28;1;35;0
WireConnection;72;0;41;0
WireConnection;59;0;28;0
WireConnection;73;0;72;0
WireConnection;73;1;59;0
WireConnection;74;0;73;0
WireConnection;76;0;74;0
WireConnection;75;0;74;0
WireConnection;110;0;75;0
WireConnection;118;0;76;0
WireConnection;117;0;118;0
WireConnection;39;0;20;0
WireConnection;39;1;21;0
WireConnection;111;0;110;0
WireConnection;119;0;117;0
WireConnection;119;1;124;0
WireConnection;12;0;41;0
WireConnection;12;1;39;0
WireConnection;115;0;111;0
WireConnection;115;1;124;0
WireConnection;114;0;115;0
WireConnection;116;0;119;0
WireConnection;36;0;12;0
WireConnection;36;1;59;0
WireConnection;56;0;41;0
WireConnection;57;0;56;0
WireConnection;57;1;59;0
WireConnection;123;0;114;0
WireConnection;123;1;116;0
WireConnection;1;1;36;0
WireConnection;140;0;139;0
WireConnection;140;1;138;0
WireConnection;128;1;123;0
WireConnection;128;2;129;0
WireConnection;2;1;57;0
WireConnection;2;5;24;0
WireConnection;38;0;37;0
WireConnection;38;1;1;0
WireConnection;0;0;38;0
WireConnection;0;1;2;0
WireConnection;0;2;140;0
WireConnection;0;3;22;0
WireConnection;0;4;23;0
WireConnection;0;5;128;0
ASEEND*/
//CHKSM=C252B79F06ADA99ADD9493A0B5F59B7596B08449