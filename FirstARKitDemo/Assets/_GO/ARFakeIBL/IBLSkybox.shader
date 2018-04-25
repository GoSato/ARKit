Shader "Skybox/IBLSkybox"
{
    Properties
    {
        _TextureY("TextureY", 2D) = "white" {}
        _TextureCbCr("TextureCbCr", 2D) = "black" {}
    }

    SubShader
    {
        Tags { "RenderType"="Background" "Queue"="Background" }

        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }

            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define PI 3.141592653589793

            struct appdata
            {
                float4 position : POSITION;
                float3 texcoord : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 position : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

            sampler2D _TextureY;
            sampler2D _TextureCbCr;

            static const float4x4 ycbcrToRGBTransform = float4x4(
                    float4(1.0, +0.0000, +1.4020, -0.7010),
                    float4(1.0, -0.3441, -0.7141, +0.5291),
                    float4(1.0, +1.7720, +0.0000, -0.8860),
                    float4(0.0, +0.0000, +0.0000, +1.0000)
                ); 

            inline float2 ToRadialCoords(float3 coords)
            {
                float3 normalizedCoords = normalize(coords);
                float latitude = acos(normalizedCoords.y);
                float longitude = atan2(normalizedCoords.z, normalizedCoords.x);
                float2 sphereCoords = float2(longitude, latitude) * float2(0.5/UNITY_PI, 1.0/UNITY_PI);
                return float2(0.5,1.0) - sphereCoords;
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos (v.position);
                o.texcoord = v.texcoord;
                return o;
            }
            
            half4 frag (v2f i) : COLOR
            {
                float2 uv = ToRadialCoords(i.texcoord);

                float y = tex2D(_TextureY, uv).r;
                float4 ycbcr = float4(y, tex2D(_TextureCbCr, uv).rg, 1.0);


                return mul(ycbcrToRGBTransform, ycbcr);
            }

            ENDCG
        }
    } 
}