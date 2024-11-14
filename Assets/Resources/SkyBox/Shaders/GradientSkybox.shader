Shader "Custom/GradientSkybox"
{
    Properties
    {
        _TopColor("Top Color", Color) = (0.5, 0.5, 0.5, 1)
        _BottomColor("Bottom Color", Color) = (0.0, 0.0, 0.0, 1)
        _Exposure("Exposure", Range(0.1, 2.0)) = 1.0
    }
        SubShader
    {
        Tags { "Queue" = "Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldDir : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _TopColor;
            float4 _BottomColor;
            float _Exposure;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldDir = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.vertex.xyz));
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float t = i.worldDir.y * 0.5 + 0.5; // Normalize to range [0, 1]
                float4 color = lerp(_BottomColor, _TopColor, t); // Interpolate between top and bottom colors
                return color * _Exposure; // Apply exposure
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}
