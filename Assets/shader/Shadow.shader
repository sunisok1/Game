Shader "Custom/BasicShadowWithBlur"
{
    Properties
    {
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 0.5) // 阴影颜色
        _Radius ("Blur Radius", Range(0, 1)) = 0.5            // 模糊半径
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            fixed4 _ShadowColor;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 计算中心距离（径向渐变）
                float dist = length(i.uv - 0.5); // 将UV坐标中心设为0.5，0.5
                float alpha = smoothstep(_Radius, _Radius - 0.1, dist); // 根据半径计算模糊

                // 返回阴影颜色，乘以模糊的alpha值
                return fixed4(_ShadowColor.rgb, _ShadowColor.a * alpha);
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/VertexLit"
}
