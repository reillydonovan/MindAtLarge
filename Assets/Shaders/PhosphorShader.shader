Shader "Custom/PhosphorShader" {
    Properties
    {
        _ModCount ("Mod Count", float) = 20.0
        _SinScale ("Sin Scale", float) = 0.6
        _Color_Time_Mult ("Color Time Multiplier",Vector) = (1.1,0.5,0.8)
        _Color_U_Mult ("ColorU Multiplier",Vector) = (5,3,2)
        _Color_V_Mult ("ColorV Multiplier",Vector) = (5,3,2)
        _Color_Offset ("Color Offset",Vector) = (1,2,3)

        _Distor_Time_Mult ("Distortion Time Multiplier",Vector) = (1.1,0.5,0.8)
        _Distor_U_Mult ("Distortion U Multiplier",Vector) = (5,3,2)
        _Distor_V_Mult ("Distortion V Multiplier",Vector) = (5,3,2)
        _Distor_Offset ("Distortion Offset",Vector) = (1,2,3)

        _Distor_Spacing ("Distortion Spacing",Vector) = (5,5,0)
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

            static const float PI = 3.1415926535897932384626433832795028841971693;
            static const float TWO_PI = 6.28318530718;

            float _ModCount;
            float _SinScale;

            float3 _Color_Time_Mult;
            float3 _Color_U_Mult;
            float3 _Color_V_Mult;
            float3 _Color_Offset;

            float3 _Distor_Time_Mult;
            float3 _Distor_U_Mult;
            float3 _Distor_V_Mult;
            float3 _Distor_Offset;
            float3 _Distor_Spacing;

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


                float3 offT = _Distor_Offset + 
                              (_Time[1] * _Distor_Time_Mult) + 
                              (IN.globalTexcoord.x * _Distor_U_Mult + IN.globalTexcoord.y * _Distor_V_Mult);

                float xOff = _SinScale * cos(offT.x) * cos(offT.y);
                float yOff = _SinScale * sin(offT.x) * sin(offT.y);

                //build up a boolean multiplier
                int bint = ((_Time[1] + (IN.globalTexcoord.x + xOff) * _ModCount) % _Distor_Spacing.x);
                bint = bint ==1;
                bint *= (_Time[1] + (IN.globalTexcoord.y + yOff) * _ModCount) % _Distor_Spacing.y;
                bint = bint ==1;

                //float b = (IN.globalTexcoord.x*5)%1;
                float3 sint = sinM(t);
                return bint*float4(sint[0],sint[1],sint[2],0);
            }
            ENDCG
        }
    }
	
	FallBack "Diffuse"
}
