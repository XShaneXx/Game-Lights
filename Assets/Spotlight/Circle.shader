Shader "Custom/SpotlightShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Range ("Spotlight Range", Range(0, 10)) = 5
        _SpotlightCenter ("Spotlight Center", Vector) = (0.5, 0.5, 0, 0)
        _Width ("Circle Width", Range(0.1, 2)) = 1
        _Height ("Circle Height", Range(0.1, 2)) = 1
        _EdgeSmoothness ("Edge Smoothness", Range(0, 1)) = 0.1

        _Range2 ("Spotlight Range 2", Range(0, 10)) = 5
        _SpotlightCenter2 ("Spotlight Center 2", Vector) = (0.5, 0.5, 0, 0)
        _Width2 ("Circle Width 2", Range(0.1, 2)) = 1
        _Height2 ("Circle Height 2", Range(0.1, 2)) = 1
        _EdgeSmoothness2 ("Edge Smoothness 2", Range(0, 1)) = 0.1
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
            float _Range, _Range2;
            float2 _SpotlightCenter, _SpotlightCenter2;
            float _Width, _Width2;
            float _Height, _Height2;
            float _EdgeSmoothness, _EdgeSmoothness2;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float aspectRatio = _ScreenParams.x / _ScreenParams.y;

                // First circle
                float2 adjustedUV1 = float2(i.uv.x * _Width, i.uv.y * _Height * aspectRatio);
                float2 adjustedCenter1 = float2(_SpotlightCenter.x * _Width, _SpotlightCenter.y * _Height * aspectRatio);
                float distFromCenter1 = length(adjustedUV1 - adjustedCenter1);
                float mask1 = smoothstep(_Range, _Range - _EdgeSmoothness, distFromCenter1);

                // Second circle
                float2 adjustedUV2 = float2(i.uv.x * _Width2, i.uv.y * _Height2 * aspectRatio);
                float2 adjustedCenter2 = float2(_SpotlightCenter2.x * _Width2, _SpotlightCenter2.y * _Height2 * aspectRatio);
                float distFromCenter2 = length(adjustedUV2 - adjustedCenter2);
                float mask2 = smoothstep(_Range2, _Range2 - _EdgeSmoothness2, distFromCenter2);

                // Combine the masks
                float combinedMask = max(mask1, mask2);

                return fixed4(0, 0, 0, 1 - combinedMask);
            }
            ENDCG
        }
    }
}
