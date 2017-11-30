Shader "Custom/Geometry/Subdivision"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_HeightMap("HeightMap", 2D) = "white" {}
        _Factor ("Factor", Range(-.8, .8)) = 0.2
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
				float2 huv : TEXCOORD1;
            };
 
            struct g2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;

			sampler2D _HeightMap;
			float4 _HeightMap_ST;

            v2g vert (appdata_full v)
            {
                v2g o;
                o.vertex = v.vertex;
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.huv = TRANSFORM_TEX(v.texcoord1, _HeightMap);
                o.normal = v.normal;
                return o;
            }
 
            float _Factor;
			float decrease = 0.8;
 
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
			void methFinal( v2g IN[3], inout TriangleStream<g2f> tristream, float amt)
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
				float3 edgeB = IN[2].vertex - IN[0].vertex;
				float3 normalFace = normalize(cross(edgeA, edgeB));

				float2 huvMid = IN[0].huv + IN[1].huv + IN[2].huv;
				huvMid /= 3.;
				fixed4 ht = tex2Dlod(_HeightMap, float4(huvMid.x, huvMid.y, 0, 0));

				float4 mid = IN[0].vertex + IN[1].vertex + IN[2].vertex;
				mid /= 3.;
				mid = mid + float4(normalFace, 0) * amt * ht[0];

				float2 uvMid = IN[0].uv + IN[1].uv + IN[2].uv;
				uvMid /= 3.;

				v2g tri1[3];
				tri1[0].vertex = IN[0].vertex;
				tri1[0].huv = IN[0].huv;
				tri1[0].uv = IN[0].uv;
				tri1[1].vertex = IN[1].vertex;
				tri1[1].uv = IN[1].uv;
				tri1[1].huv = IN[1].huv;
				tri1[2].vertex = mid;
				tri1[2].uv = uvMid;
				tri1[2].huv = huvMid;

				v2g tri2[3];
				tri2[0].vertex = IN[1].vertex;
				tri2[0].uv = IN[1].uv;
				tri2[0].huv = IN[1].huv;
				tri2[1].vertex = IN[2].vertex;
				tri2[1].uv = IN[2].uv;
				tri2[1].huv = IN[2].huv;
				tri2[2].vertex = mid;
				tri2[2].uv = uvMid;
				tri2[2].huv = huvMid;

				v2g tri3[3];
				tri3[0].vertex = IN[2].vertex;
				tri3[0].uv = IN[2].uv;
				tri3[0].huv = IN[2].huv;
				tri3[1].vertex = IN[0].vertex;
				tri3[1].uv = IN[0].uv;
				tri3[1].huv = IN[0].huv;
				tri3[2].vertex = mid;
				tri3[2].uv = uvMid;
				tri3[2].huv = huvMid;

				addTri(tristream, tri1);
				addTri(tristream, tri2);
				addTri(tristream, tri3);
			}
			void meth2( v2g IN[3], inout TriangleStream<g2f> tristream, float amt)
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
				float3 edgeB = IN[2].vertex - IN[0].vertex;
				float3 normalFace = normalize(cross(edgeA, edgeB));

				float2 huvMid = IN[0].huv + IN[1].huv + IN[2].huv;
				huvMid /= 3.;
				fixed4 ht = tex2Dlod(_HeightMap, float4(huvMid.x, huvMid.y, 0, 0));

				float4 mid = IN[0].vertex + IN[1].vertex + IN[2].vertex;
				mid /= 3.;
				mid = mid + float4(normalFace, 0) * amt * ht[0];

				float2 uvMid = IN[0].uv + IN[1].uv + IN[2].uv;
				uvMid /= 3.;

				v2g tri1[3];
				tri1[0].vertex = IN[0].vertex;
				tri1[0].huv = IN[0].huv;
				tri1[0].uv = IN[0].uv;
				tri1[1].vertex = IN[1].vertex;
				tri1[1].uv = IN[1].uv;
				tri1[1].huv = IN[1].huv;
				tri1[2].vertex = mid;
				tri1[2].uv = uvMid;
				tri1[2].huv = huvMid;

				v2g tri2[3];
				tri2[0].vertex = IN[1].vertex;
				tri2[0].uv = IN[1].uv;
				tri2[0].huv = IN[1].huv;
				tri2[1].vertex = IN[2].vertex;
				tri2[1].uv = IN[2].uv;
				tri2[1].huv = IN[2].huv;
				tri2[2].vertex = mid;
				tri2[2].uv = uvMid;
				tri2[2].huv = huvMid;

				v2g tri3[3];
				tri3[0].vertex = IN[2].vertex;
				tri3[0].uv = IN[2].uv;
				tri3[0].huv = IN[2].huv;
				tri3[1].vertex = IN[0].vertex;
				tri3[1].uv = IN[0].uv;
				tri3[1].huv = IN[0].huv;
				tri3[2].vertex = mid;
				tri3[2].uv = uvMid;
				tri3[2].huv = huvMid;

				methFinal(tri1, tristream, amt*decrease);
				methFinal(tri2, tristream, amt*decrease);
				methFinal(tri3, tristream, amt*decrease);
			}

            void meth1( v2g IN[3], inout TriangleStream<g2f> tristream, float amt)
			{
				float3 edgeA = IN[1].vertex - IN[0].vertex;
                float3 edgeB = IN[2].vertex - IN[0].vertex;
                float3 normalFace = normalize(cross(edgeA, edgeB));

				float2 huvMid = IN[0].huv + IN[1].huv + IN[2].huv;
				huvMid /= 3.;
				fixed4 ht = tex2Dlod(_HeightMap, float4(huvMid.x, huvMid.y, 0, 0));
				
                float4 mid = IN[0].vertex + IN[1].vertex + IN[2].vertex;
                mid /= 3.;
				mid = mid + float4(normalFace, 0) * amt * ht[0];

				float2 uvMid = IN[0].uv + IN[1].uv + IN[2].uv;
				uvMid /= 3.;
               
				v2g tri1[3];
				tri1[0].vertex = IN[0].vertex;
				tri1[0].huv = IN[0].huv;
				tri1[0].uv     = IN[0].uv;
				tri1[1].vertex = IN[1].vertex;
				tri1[1].uv     = IN[1].uv;
				tri1[1].huv    = IN[1].huv;
				tri1[2].vertex = mid;
				tri1[2].uv     = uvMid;
				tri1[2].huv = huvMid;
				 
				v2g tri2[3];
				tri2[0].vertex = IN[1].vertex;
				tri2[0].uv     = IN[1].uv;
				tri2[0].huv = IN[1].huv;
				tri2[1].vertex = IN[2].vertex;
				tri2[1].uv = IN[2].uv;
				tri2[1].huv = IN[2].huv;
				tri2[2].vertex = mid;
				tri2[2].uv = uvMid;
				tri2[2].huv = huvMid;

				v2g tri3[3];
				tri3[0].vertex = IN[2].vertex;
				tri3[0].uv = IN[2].uv;
				tri3[0].huv = IN[2].huv;
				tri3[1].vertex = IN[0].vertex;
				tri3[1].uv = IN[0].uv;
				tri3[1].huv = IN[0].huv;
				tri3[2].vertex = mid;
				tri3[2].uv = uvMid;
				tri3[2].huv = huvMid;

				//addTri(tristream, tri1);
				//addTri(tristream, tri2);
				//addTri(tristream, tri3);
				meth2(tri1, tristream, amt*decrease);
				meth2(tri2, tristream, amt*decrease);
				meth2(tri3, tristream, amt*decrease);
			}

            [maxvertexcount(90)]
	        void geom(triangle v2g IN[3], inout TriangleStream<g2f> tristream)
            {
				meth1(IN,tristream,_Factor);
            }

			fixed4 frag (g2f i) :  SV_Target
            {
                // sample texture
                fixed4 col = tex2D(_MainTex, i.uv);


			// dot product between normal and light direction for
			// standard diffuse (Lambert) lighting
			float3 worldNormal = -UnityObjectToWorldNormal(i.normal);
			float nl = max(0.1, dot(worldNormal, _WorldSpaceLightPos0.xyz));
			// factor in the light color
			float4 tmp = nl * float4(1.0, 1.0, 1.0, 1.0);

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