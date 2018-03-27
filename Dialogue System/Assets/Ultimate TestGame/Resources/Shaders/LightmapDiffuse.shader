// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Custom/Diffuse Lightmap" {

  Properties {
    _MainTex ("Texture 1", 2D) = "white" {}
  }

  SubShader {
    Tags { "RenderType" = "Opaque" }

    Pass {
      // Disable lighting, we're only using the lightmap
      Lighting Off

      CGPROGRAM
      // Must be a vert/frag shader, not a surface shader: the necessary variables
      // won't be defined yet for surface shaders.
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct v2f {
        float4 pos : SV_POSITION;
        float2 uv0 : TEXCOORD0;
        float2 uv1 : TEXCOORD1;
      };

      struct appdata_lightmap {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
        float2 texcoord1 : TEXCOORD1;
      };

      // These are prepopulated by Unity
      // sampler2D unity_Lightmap;
      // float4 unity_LightmapST;

      sampler2D _MainTex;
      float4 _MainTex_ST; // Define this since its expected by TRANSFORM_TEX; it is also pre-populated by Unity.

      v2f vert(appdata_lightmap i) {
        v2f o;
        o.pos = UnityObjectToClipPos(i.vertex);

        // UnityCG.cginc - Transforms 2D UV by scale/bias property
        // #define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)
        o.uv0 = TRANSFORM_TEX(i.texcoord, _MainTex);

        // Use `unity_LightmapST` NOT `unity_Lightmap_ST`
        o.uv1 = i.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
        return o;
      }

      half4 frag(v2f i) : COLOR {
        half4 main_color = tex2D(_MainTex, i.uv0);

        // Decodes lightmaps:
        // - doubleLDR encoded on GLES
        // - RGBM encoded with range [0;8] on other platforms using surface shaders
        // inline fixed3 DecodeLightmap(fixed4 color) {
        // #if defined(SHADER_API_GLES) && defined(SHADER_API_MOBILE)
          // return 2.0 * color.rgb;
        // #else
          // return (8.0 * color.a) * color.rgb;
        // #endif
        // }

        main_color.rgb *= DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv1));
        return main_color;
      }
      ENDCG
    }
  }
}