Shader "Custom/reflec"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Normal ("normal", 2D) = "bump" {}
        _mainTex("CamerRenderedTexture", 2D) = "white" {}

        _Height("Hegiht",Range(0,1))=0
        




    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" ="Transparent"}
        

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf NoLighting

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 2.0




        sampler2D _Normal;
        sampler2D _mainTex;

        struct Input
        {
            float2 uv_MainTex;
            fixed4 screenPos;
            fixed2 uv_Normal;
        };

        fixed4 _Color;
        fixed _Height;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed3 screenUv = IN.screenPos.xyz / IN.screenPos.w;

            fixed3 Normal = (UnpackNormal(tex2D(_Normal, fixed2(IN.uv_Normal.x+_Time.y*0.1, IN.uv_Normal.y + _Time.y*0.1))))*0.6;


            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_mainTex, fixed2(screenUv.x,1-screenUv.y-_Height)+Normal.xy*0.1f) * _Color;
            o.Albedo = c.rgb-fixed3(0.3,0.1,0);
            // Metallic and smoothness come from slider variables
            o.Alpha = c.a;
        }

        fixed4 LightingNoLighting(SurfaceOutput s,fixed3 lightDir, fixed atten)
        {
            fixed4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            return c;


        }


        ENDCG
    }
    FallBack "Diffuse"
}
