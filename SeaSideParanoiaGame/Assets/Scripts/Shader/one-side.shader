Shader "Unlit/one-side"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 0 
    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "CanUseSpriteAtlas" = "true"
            "PreviewType" = "Plane"
        }

        Lighting Off
        ZWrite Off
        Cull [_Cull]

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i, bool facing : SV_IsFrontFace) : SV_Target
            {
                float2 uv = i.uv;

                // Flip horizontally if rendering backface
                if (!facing)
                {
                    uv.x = 1.0 - uv.x;
                }

                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }

            ENDCG
        }
    }
}
