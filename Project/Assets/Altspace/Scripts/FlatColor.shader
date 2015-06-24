Shader "AltSpaceVR/FlatColor" {
	Properties {
		_Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader {
		Tags {"Queue"="Transparent" }
		Lighting Off Cull Off ZTest Always ZWrite Off Fog { Mode Off }
		Pass{
			CGPROGRAM
			// PRAGMAS
			#pragma vertex vert
			#pragma fragment frag
			
			// user defined properties
			uniform float4 _Color;
			
			// input Struct
			struct vertexInput{
				float4 vertex : POSITION;
			};
			
			// output Struct
			struct vertexOutput{
				float4 pos :  SV_POSITION;
			};
			
			// vertex function
			vertexOutput vert(vertexInput v){
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			// fragment function
			float4 frag(vertexOutput o) : COLOR{
				return float4(_Color.xyz + UNITY_LIGHTMODEL_AMBIENT.rgb,1.0);
			}
			ENDCG
			}
	} 
	FallBack "Diffuse"
}
