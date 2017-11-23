Shader "Custom/Geometry/Extrude2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Factor ("Factor", Range(-.1, .1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
 
            #include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

            struct v2g
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
 
            struct g2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
           
            v2g vert (appdata_base v)
            {
                v2g o;
                o.vertex = v.vertex;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.normal = v.normal;
                return o;
            }
 
            float _Factor;
 
           	void addTri( inout TriangleStream<g2f> tristream, triangle v2g IN[3])
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
                float3 edgeB = IN[2].vertex - IN[0].vertex;
                float3 normalFace = normalize(cross(edgeA, edgeB));

                g2f outVert; 

                outVert.pos = UnityObjectToClipPos( IN[0].vertex );
                outVert.uv = IN[0].uv;
                outVert.normal = normalFace;
               // outVert.col = fixed4(1., 1., 1., 1.);
                tristream.Append(outVert);

				outVert.pos =UnityObjectToClipPos( IN[1].vertex );
                outVert.uv = IN[1].uv;
                outVert.normal = normalFace;
               // outVert.col = fixed4(1., 1., 1., 1.);
                tristream.Append(outVert);

				outVert.pos =UnityObjectToClipPos( IN[2].vertex );
                outVert.uv = IN[2].uv;
                outVert.normal = normalFace;
               // outVert.col = fixed4(1., 1., 1., 1.);
                tristream.Append(outVert);			
                tristream.RestartStrip();
			}
			void methFinal( v2g IN[3], inout TriangleStream<g2f> tristream)
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
				float3 edgeB = IN[2].vertex - IN[0].vertex;
				float3 normalFace = normalize(cross(edgeA, edgeB));

				float4 mid = IN[0].vertex + IN[1].vertex + IN[2].vertex;
				mid /= 3.;
				mid = mid + float4(normalFace, 0) * _Factor;

				float2 uvMid = IN[0].uv + IN[1].uv + IN[2].uv;
				uvMid /= 3.;
				v2g tri1[3];
				tri1[0].vertex = IN[0].vertex;
				tri1[0].uv = IN[0].uv;
				tri1[1].vertex = IN[1].vertex;
				tri1[1].uv = IN[1].uv;
				tri1[2].vertex = mid;
				tri1[2].uv = uvMid;
				v2g tri2[3];
				tri2[0].vertex = IN[1].vertex;
				tri2[0].uv = IN[1].uv;
				tri2[1].vertex = IN[2].vertex;
				tri2[1].uv = IN[2].uv;
				tri2[2].vertex = mid;
				tri2[2].uv = uvMid;

				v2g tri3[3];
				tri3[0].vertex = IN[2].vertex;
				tri3[0].uv = IN[2].uv;
				tri3[1].vertex = IN[0].vertex;
				tri3[1].uv = IN[0].uv;
				tri3[2].vertex = mid;
				tri3[2].uv = uvMid;

				g2f outVert;
				//tri 1

				addTri(tristream, tri1);
				addTri(tristream, tri2);
				addTri(tristream, tri3);
			}
			void meth2( v2g IN[3], inout TriangleStream<g2f> tristream)
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
				float3 edgeB = IN[2].vertex - IN[0].vertex;
				float3 normalFace = normalize(cross(edgeA, edgeB));

				float4 mid = IN[0].vertex + IN[1].vertex + IN[2].vertex;
				mid /= 3.;
				mid = mid + float4(normalFace, 0) * _Factor;

				float2 uvMid = IN[0].uv + IN[1].uv + IN[2].uv;
				uvMid /= 3.;

				v2g tri1[3];
				tri1[0].vertex = IN[0].vertex;
				tri1[0].uv = IN[0].uv;
				tri1[1].vertex = IN[1].vertex;
				tri1[1].uv = IN[1].uv;
				tri1[2].vertex = mid;
				tri1[2].uv = uvMid;

				v2g tri2[3];
				tri2[0].vertex = IN[1].vertex;
				tri2[0].uv = IN[1].uv;
				tri2[1].vertex = IN[2].vertex;
				tri2[1].uv = IN[2].uv;
				tri2[2].vertex = mid;
				tri2[2].uv = uvMid;

				v2g tri3[3];
				tri3[0].vertex = IN[2].vertex;
				tri3[0].uv = IN[2].uv;
				tri3[1].vertex = IN[0].vertex;
				tri3[1].uv = IN[0].uv;
				tri3[2].vertex = mid;
				tri3[2].uv = uvMid;

				//addTri(tristream, tri1);
				//addTri(tristream, tri2);
				//addTri(tristream, tri3);
				methFinal(tri1, tristream);
				methFinal(tri2, tristream);
				methFinal(tri3, tristream);
			}
            void meth1( v2g IN[3], inout TriangleStream<g2f> tristream)
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
                float3 edgeB = IN[2].vertex - IN[0].vertex;
                float3 normalFace = normalize(cross(edgeA, edgeB));

                float4 mid = IN[0].vertex + IN[1].vertex + IN[2].vertex;
                mid /= 3.;
                mid = mid + float4(normalFace, 0) * _Factor;

                float2 uvMid = IN[0].uv + IN[1].uv + IN[2].uv;
                uvMid /= 3.;

				v2g tri1[3];
				tri1[0].vertex = IN[0].vertex;
				tri1[0].uv     = IN[0].uv;
				tri1[1].vertex = IN[1].vertex;
				tri1[1].uv     = IN[1].uv;
				tri1[2].vertex = mid;
				tri1[2].uv     = uvMid;
				 
				v2g tri2[3];
				tri2[0].vertex = IN[1].vertex;
				tri2[0].uv     = IN[1].uv;
				tri2[1].vertex = IN[2].vertex;
				tri2[1].uv     = IN[2].uv;
				tri2[2].vertex = mid;
				tri2[2].uv     = uvMid;

				v2g tri3[3];
				tri3[0].vertex = IN[2].vertex;
				tri3[0].uv     = IN[2].uv;
				tri3[1].vertex = IN[0].vertex;
				tri3[1].uv     = IN[0].uv;
				tri3[2].vertex = mid;
				tri3[2].uv     = uvMid;

				//addTri(tristream, tri1);
				//addTri(tristream, tri2);
				//addTri(tristream, tri3);
				meth2(tri1, tristream);
				meth2(tri2, tristream);
				meth2(tri3, tristream);
			}

            [maxvertexcount(90)]
	        void geom(triangle v2g IN[3], inout TriangleStream<g2f> tristream)
            {
				meth1(IN,tristream);
            }

			fixed4 frag (g2f i) :  SV_Target
            {
                // sample texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // multiply by lighting
			//float4 tmp = float4(i.normal * _LightColor0, 1);
			//float4 tmp = float4(i.normal,1)	;
                //col *= tmp;
                return col;
            }
            ENDCG
        }
    }
}