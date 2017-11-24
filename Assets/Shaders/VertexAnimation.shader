 Shader "Custom/StandardVertexAnim" {
     Properties {

         _Color ("Color", Color) = (1,1,1,1)
         _MainTex ("Albedo (RGB)", 2D) = "white" {}
		 _Glossiness("Smoothness", Range(0,1)) = 0.5
		 _Metallic("Metallic", Range(0,1)) = 0.0
		 _Amount("Amount", Range(-100,100)) = 1.0

		 _VertexAnimOffset("VertexAnimOffset", Range(-100,100)) = 1.0
         _WaveValueX1 ("WaveValueX1", Range(-100,100)) = 1.0
         _WaveValueX2 ("WaveValueX2", Range(-100,100)) = 1.0
         _WaveValueX3 ("WaveValueX3", Range(-100,100)) = 1.0
		 _WaveValueY1("WaveValueY1", Range(-100,100)) = 1.0
		 _WaveValueY2("WaveValueY2", Range(-100,100)) = 1.0
		 _WaveValueY3("WaveValueY3", Range(-100,100)) = 1.0
		 _WaveValueZ1("WaveValueZ1", Range(-100,100)) = 1.0
		 _WaveValueZ2("WaveValueZ2", Range(-100,100)) = 1.0
		 _WaveValueZ3("WaveValueZ3", Range(-100,100)) = 1.0

		 _BubbleValue1("BubbleValue1", Range(-100,100)) = 1.0
		 _BubbleValue2("BubbleValue2", Range(-100,100)) = 1.0
		 _BubbleValue3("BubbleValue3", Range(-100,100)) = 1.0

	     _FatValue1("FatValue1", Range(-100,100)) = 1.0



		 
     }
     SubShader {
         Tags { "RenderType"="Opaque" }
         LOD 200
        			Cull Off

         CGPROGRAM
         // Physically based Standard lighting model, and enable shadows on all light types
        // #pragma surface surf Standard fullforwardshadows
 		#pragma surface surf Standard vertex:vert fullforwardshadows
         // Use shader model 3.0 target, to get nicer looking lighting
         #pragma target 3.0
 
         sampler2D _MainTex;
 
         struct Input {
             float2 uv_MainTex;
         };
 
         half _Glossiness;
         half _Metallic;
         fixed4 _Color;
         float _Amount;

		 float _VertexAnimOffset;
		 float totalOffset;
         float _WaveValueX1;
         float _WaveValueX2;
         float _WaveValueX3;
		 float _WaveValueY1;
		 float _WaveValueY2;
		 float _WaveValueY3;
		 float _WaveValueZ1;
		 float _WaveValueZ2;
		 float _WaveValueZ3;

		 float _BubbleValue1;
		 float _BubbleValue2;
		 float _BubbleValue3;

		 float _FatValue1;
	//	 float _FatValue2;
	//	 float _FatValue3;
	


		 float _BubbleValueGlobal;
 
         void surf (Input IN, inout SurfaceOutputStandard o) {
             // Albedo comes from a texture tinted by color
             fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
             o.Albedo = c.rgb;
             // Metallic and smoothness come from slider variables
             o.Metallic = _Metallic;
             o.Smoothness = _Glossiness;
             o.Alpha = c.a;
         }
        
         void vert (inout appdata_full v) {
           //  v.vertex.xz += v.normal.xz * _Amount * abs(sin(_Time * 10)) * v.color.x;
           //  v.vertex.y += _Amount * abs(sin(_Time * 200)) * v.color.y;

           //Fat Mesh
		  totalOffset = _Time + _VertexAnimOffset;
          v.vertex.xyz += v.normal * _FatValue1;

           //Wave Mesh
        //   v.vertex.x += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;
     //   v.vertex.x += sin((v.vertex.x + _Time * _Value3 ) * _Value2 ) * _Value1;
    //     v.vertex.y += sin((v.vertex.y + _Time * _Value3 ) * _Value2 ) * _Value1;
		 
			 // X Axis Sin Wave Deformation
         v.vertex.z += sin((v.vertex.z + totalOffset * _WaveValueX3 ) * _WaveValueX2 ) * _WaveValueX1;


		 // Y Axis Sin Wave Deformation
		 v.vertex.z += sin((v.vertex.z + totalOffset * _WaveValueY3) * _WaveValueY2) * _WaveValueY1;


		 // Z Axis Sin Wave Deformation
		 v.vertex.z += sin((v.vertex.z + totalOffset * _WaveValueZ3) * _WaveValueZ2) * _WaveValueZ1;

       //Bubbling Mesh
       v.vertex.xyz += v.normal * ( sin((v.vertex.x + totalOffset * _BubbleValue3) * _BubbleValue2) + cos((v.vertex.z + totalOffset* _BubbleValue3) * _BubbleValue2)) * _BubbleValue1;

         }
 
         ENDCG
     }
     FallBack "Diffuse"
     }