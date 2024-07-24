Shader "Custom/TextureShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // 定义一个纹理属性
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex; // 声明纹理采样器

        struct Input
        {
            float2 uv_MainTex; // 纹理坐标
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // 采样纹理并设置为表面输出颜色
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
