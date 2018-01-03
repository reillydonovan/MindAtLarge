Shader "Custom/PhosphorShader" {
    Properties
    {
        _Val ("Val", float) = 20.0
        _Color_Time_Mult ("ColorTime",Vector) = (1.1,0.5,0.8)
        _Color_U_Mult ("ColorU Mults",Vector) = (5,3,2)
        _Color_V_Mult ("ColorV Mults",Vector) = (5,3,2)
        _Color_Offset ("Color Offset",Vector) = (1,2,3)
        _Time_mult ("TimeMult", Vector) =  (0,0,0) 
     }

     SubShader
     {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            float _Val;
            float2 _Time_mult;

            float3 _Color_Time_Mult;
            float3 _Color_U_Mult;
            float3 _Color_V_Mult;
            float3 _Color_Offset;
            //float4 _MyVector;
            static const float PI = 3.1415926535897932384626433832795028841971693;
            static const float TWO_PI = 6.28318530718;
            float sinM(float t)
            {
                return (1.0f+sin(t))/2.0f;
            }

            float3 sinM(float3 t)
            {
                return (1.0f+sin(t))/2.0f;
            }

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float3 t = _Color_Offset + _Time[1] * _Color_Time_Mult + 
                           ( _Color_U_Mult * IN.globalTexcoord.x + _Color_V_Mult * IN.globalTexcoord.y ) * TWO_PI;
                float offT1 = _Time[1] + (IN.globalTexcoord.x + IN.globalTexcoord.y) * 20;
                float offT2 = 1.1*_Time[1] + (-3*IN.globalTexcoord.x + 2*IN.globalTexcoord.y) * 4 -10;
                float xOff = 0.6 * cos(offT1) * cos(offT2);
                float yOff = 0.6 * sin(offT1) * sin(offT2);
                int bint = (_Time[1] + (IN.globalTexcoord.x + xOff) * _Val) % 2;
                bint    *= (_Time[1] + (IN.globalTexcoord.y + yOff) * _Val) % 2;

                //float b = (IN.globalTexcoord.x*5)%1;
                    float3 sint = sinM(t);
                    return bint*float4(sint[0],sint[1],sint[2],0);
            }
            ENDCG
        }
    }
	
	FallBack "Diffuse"
}
