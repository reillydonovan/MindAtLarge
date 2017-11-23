// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SphericalHarmonic" 
{
		    
	Properties
    {
		// Color property for material inspector, default to white
		_Color ("Main Color", Color) = (1,1,1,1)

		//there are two waves that can be applied to and modify the surface of the spere
	    //there is no reason that these have to be the only two waves, but I cannot decide 
	    //on a approach to easily make ANY number of them in the editor...

        //set of vars exposed to create waves parallel to the 'latitude'
   		// of the sphere
		xMod1YOffset ("xMod1YOffset", Float) = 2
		xMod1Scale("xMod1Scale", Float) = 3
		xMod1TimeResponse("xMod1TimeResponse", Float) = 4
		xMod1Period("xMod1Period", Float) = 5
		xMod1PhaseOffset("xMod1PhaseOffset", Float) = 6 

		//set of vars exposed to create waves parallel to the 'longitude'
		// of the sphere
		yMod1YOffset("yMod1YOffset", Float) = 2 
		yMod1Scale("yMod1Scale", Float) = 3
		yMod1TimeResponse("yMod1TimeResponse", Float) = 4
		yMod1Period("yMod1Period", Float) = 5 
		yMod1PhaseOffset("yMod1PhaseOffset", Float) = 6
		_MainTex ("Texture", 2D) = "white" {}
    }
	SubShader
	{
		Pass
	    {
			CGPROGRAM
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
		      #pragma vertex vert
		      #pragma fragment frag

			float _time;
			fixed4 _Color;

			sampler2D _MainTex;

			float xMod1YOffset;
			float xMod1Scale;
			float xMod1TimeResponse;
			float xMod1Period;
			float xMod1PhaseOffset;
			float yMod1YOffset;
			float yMod1Scale;
			float yMod1TimeResponse;
			float yMod1Period;
			float yMod1PhaseOffset;

			//get radius applies waves along phi and theta based on the public variables
			//optimization note:
			// this would not be impossible to code as a shader... however, getting multiple 
			// waves affecting the surface at once might take some careful thinking...
			float GetRadius(float phi, float theta, float time = 0)
			{
				//x1 + x2*sin(x3*t + tht*x4 + x5) + y1 + y2*sin(y3*t + phi*y4 + y5)

				//y2*sin(y4*x+y5+t*y3)+y1+ 
				//x2*sin(x4*y+x5+t*x3)+x1
				//dTheta/df = xMod1Scale*xMod1Period*cos(xMod1Period*theta+xMod1PhaseOffset+time*xMod1TimeResponse)
				//dPhi/df = yMod1Scale*yMod1Period*cos(yMod1Period*phi+yMod1PhaseOffset+time*yMod1TimeResponse)
				return xMod1YOffset + xMod1Scale*sin(xMod1TimeResponse*time + theta*xMod1Period + xMod1PhaseOffset) +
					   yMod1YOffset + yMod1Scale*sin(yMod1TimeResponse*time + phi*yMod1Period + yMod1PhaseOffset);
			}

			float3 getNorm(float phi, float theta)
			{
				return float3( sin(2*phi) * cos(theta),
							   sin(phi) * sin(theta),
							   cos(phi));
			}
			struct appdata 
			{
		        float4 vertex : POSITION;
				float3 uv : TEXCOORD0;
				float3 phiTheta : TEXCOORD1;
		    };


			struct v2f
			{
				float2 uv : TEXCOORD0;
				float normal: NORMAL ;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
		    {
				v2f o;
				float radius = GetRadius(v.phiTheta.x, v.phiTheta.y, _Time.y);
				o.vertex =  float4(radius * sin(v.phiTheta.x) * cos(v.phiTheta.y),
								   radius * sin(v.phiTheta.x) * sin(v.phiTheta.y),
								   radius * cos(v.phiTheta.x),1);
		        o.normal = normalize(o.vertex);
				o.vertex = UnityObjectToClipPos(o.vertex);//UnityObjectToClipPos(o.vertex);
				//getNorm(v.phiTheta.x, v.phiTheta.y);				 

		        o.uv = v.uv; //TRANSFORM_TEX(v.uv, _MainTex);
		        UNITY_TRANSFER_FOG(o,o.vertex);
		        return o;
		    }



		    fixed4 frag (v2f i) : SV_Target
		    {
		        // sample the texture
		        fixed4 col = tex2D(_MainTex, i.uv);
		        // apply fog
		        UNITY_APPLY_FOG(i.fogCoord, col);
	
		        return col;
		    }
			ENDCG
		}
	//FallBack "Diffuse"
	}
}