Shader "Custom/SpotlightShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _SpotlightColor ("Spotlight Color", Color) = (1, 1, 1, 1)
        _Intensity ("Spotlight Intensity", Range(0, 1)) = 0.5
        _Range ("Spotlight Range", Range(0, 10)) = 5
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _SpotlightColor;
            float _Intensity;
            float _Range;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float4 spotlightColor = _SpotlightColor * _Intensity * (1 - length(i.uv - 0.5) * 2);
                return spotlightColor;
            }
            ENDCG
        }
    }
}
