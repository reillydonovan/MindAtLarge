Shader "Custom/ScrollShader" {
	Properties {
		_MainTint ("Diffuse Tint", Color) = (1,1,1,1)
		_NormalTex ("Normal Map", 2D) = "bump" {}
		_NormalMapIntensity ("Normal Map Intensity", Range(0,1)) = 1
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_ScrollXSpeed("X Scroll Speed", Range(-10,10)) = 2
		_ScrollYSpeed("Y Scroll Speed", Range(-10,10)) = 2
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float2 uv_NormalTex;
		};

		half _Glossiness;
		half _Metallic;
		float4 _MainTint;

		fixed _ScrollXSpeed;
		fixed _ScrollYSpeed;
		sampler2D _MainTex;
		sampler2D _NormalTex;
		fixed _NormalMapIntensity;

		//fixed _NormalMapIntensity;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//create a separate variable to store our UVS before we pass them to the text2D() function



			//float3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
			fixed3 n = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex)).rgb;
			n.x *= _NormalMapIntensity;
			n.y *= _NormalMapIntensity;


			fixed2 scrolledUV = IN.uv_MainTex;
			fixed2 scrollNM = IN.uv_NormalTex;

			fixed xScrollValue = _ScrollXSpeed * _Time;
			fixed yScrollValue = _ScrollYSpeed * _Time;

			scrolledUV += fixed2(xScrollValue, yScrollValue);
			scrollNM += fixed2(xScrollValue, yScrollValue);
			//normalMap += fixed2(xScrollValue, yScrollValue);

			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			half4 c  = tex2D (_MainTex, scrolledUV);

			o.Albedo = c.rgb * _MainTint;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Normal = n.rgb;
			o.Normal = normalize(n);
			o.Alpha = c.a;

		}
		ENDCG
	}
	FallBack "Diffuse"
}
