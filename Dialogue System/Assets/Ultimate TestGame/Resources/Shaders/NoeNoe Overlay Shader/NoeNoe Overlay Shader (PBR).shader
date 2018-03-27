// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34020,y:32577,varname:node_3138,prsc:2|diff-2300-OUT,spec-4566-OUT,gloss-1851-OUT,normal-4793-RGB,emission-2869-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:33039,y:32044,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1689,x:31565,y:31470,ptovrint:False,ptlb:Tile Overlay,ptin:_TileOverlay,varname:node_1689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1431-OUT;n:type:ShaderForge.SFN_Tex2d,id:9151,x:32702,y:32361,ptovrint:False,ptlb:Main texture (RGB),ptin:_MainTex,varname:node_9151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7815,x:33039,y:32206,varname:node_7815,prsc:2|A-2845-OUT,B-7241-RGB,C-4517-OUT;n:type:ShaderForge.SFN_Vector1,id:4517,x:33039,y:32338,varname:node_4517,prsc:2,v1:1;n:type:ShaderForge.SFN_Desaturate,id:5160,x:33273,y:32044,varname:node_5160,prsc:2|COL-7815-OUT,DES-3971-OUT;n:type:ShaderForge.SFN_Vector1,id:3971,x:33273,y:31995,varname:node_3971,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Desaturate,id:9411,x:33476,y:32044,varname:node_9411,prsc:2|COL-5160-OUT,DES-8933-OUT;n:type:ShaderForge.SFN_OneMinus,id:8933,x:33273,y:32206,varname:node_8933,prsc:2|IN-7241-A;n:type:ShaderForge.SFN_ViewVector,id:2089,x:30819,y:31184,cmnt:View Direction UV Mapping by Maki and Snail,varname:node_2089,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:4810,x:30819,y:31322,varname:node_4810,prsc:2,cc1:2,cc2:0,cc3:1,cc4:-1|IN-2089-OUT;n:type:ShaderForge.SFN_Pi,id:2632,x:30852,y:31460,varname:node_2632,prsc:2;n:type:ShaderForge.SFN_Negate,id:3403,x:30990,y:31460,varname:node_3403,prsc:2|IN-2632-OUT;n:type:ShaderForge.SFN_ArcCos,id:1044,x:30990,y:31332,varname:node_1044,prsc:2|IN-4810-B;n:type:ShaderForge.SFN_Divide,id:1808,x:31166,y:31470,varname:node_1808,prsc:2|A-1044-OUT,B-3403-OUT;n:type:ShaderForge.SFN_ArcTan2,id:5315,x:30990,y:31184,varname:node_5315,prsc:2,attp:2|A-4810-R,B-4810-G;n:type:ShaderForge.SFN_Append,id:6711,x:31166,y:31332,varname:node_6711,prsc:2|A-5315-OUT,B-1808-OUT;n:type:ShaderForge.SFN_OneMinus,id:9020,x:31340,y:31332,varname:node_9020,prsc:2|IN-6711-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4903,x:31340,y:31470,varname:node_4903,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9020-OUT;n:type:ShaderForge.SFN_Multiply,id:2300,x:33464,y:32567,varname:node_2300,prsc:2|A-9411-OUT,B-3981-OUT,C-5241-OUT,D-7199-OUT;n:type:ShaderForge.SFN_Multiply,id:2845,x:32868,y:32206,varname:node_2845,prsc:2|A-3107-OUT,B-6612-OUT;n:type:ShaderForge.SFN_Lerp,id:3107,x:32702,y:32082,varname:node_3107,prsc:2|A-7920-OUT,B-1401-OUT,T-8759-OUT;n:type:ShaderForge.SFN_Vector1,id:7920,x:32151,y:32352,varname:node_7920,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:6612,x:32702,y:32206,varname:node_6612,prsc:2|A-7920-OUT,B-9151-RGB,T-6835-OUT;n:type:ShaderForge.SFN_Clamp01,id:8759,x:32477,y:32072,varname:node_8759,prsc:2|IN-6611-OUT;n:type:ShaderForge.SFN_OneMinus,id:9601,x:31988,y:32209,varname:node_9601,prsc:2|IN-6611-OUT;n:type:ShaderForge.SFN_Add,id:748,x:32151,y:32209,varname:node_748,prsc:2|A-7920-OUT,B-9601-OUT;n:type:ShaderForge.SFN_Clamp01,id:6835,x:32314,y:32209,varname:node_6835,prsc:2|IN-748-OUT;n:type:ShaderForge.SFN_Slider,id:6611,x:32157,y:32139,ptovrint:False,ptlb:Crossfade Surface / Overlay,ptin:_CrossfadeSurfaceOverlay,varname:node_6611,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Append,id:1986,x:31571,y:31183,varname:node_1986,prsc:2|A-4097-OUT,B-3842-OUT;n:type:ShaderForge.SFN_Time,id:47,x:31571,y:31332,varname:node_47,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7401,x:31752,y:31193,varname:node_7401,prsc:2|A-1986-OUT,B-47-T;n:type:ShaderForge.SFN_Add,id:1431,x:31752,y:31332,varname:node_1431,prsc:2|A-7401-OUT,B-4903-OUT;n:type:ShaderForge.SFN_Slider,id:2514,x:31414,y:30967,ptovrint:False,ptlb:Tile Speed X,ptin:_TileSpeedX,varname:node_2514,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:4097,x:31752,y:30903,varname:node_4097,prsc:2|A-9795-OUT,B-2514-OUT;n:type:ShaderForge.SFN_Vector1,id:9795,x:31571,y:30903,varname:node_9795,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:3842,x:31752,y:31043,varname:node_3842,prsc:2|A-9795-OUT,B-5322-OUT;n:type:ShaderForge.SFN_Slider,id:5322,x:31414,y:31048,ptovrint:False,ptlb:Tile Speed Y,ptin:_TileSpeedY,varname:node_5322,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_ViewVector,id:4308,x:31022,y:31835,varname:node_4308,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:769,x:31022,y:31656,varname:node_769,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-4308-OUT;n:type:ShaderForge.SFN_Multiply,id:5696,x:31180,y:31775,varname:node_5696,prsc:2|A-769-G,B-41-OUT;n:type:ShaderForge.SFN_Vector1,id:41,x:30880,y:31822,varname:node_41,prsc:2,v1:-1;n:type:ShaderForge.SFN_Append,id:7373,x:31180,y:31656,varname:node_7373,prsc:2|A-769-R,B-5696-OUT,C-769-B;n:type:ShaderForge.SFN_Cubemap,id:1516,x:31340,y:31656,ptovrint:False,ptlb:Cubemap Overlay,ptin:_CubemapOverlay,varname:node_7018,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0|DIR-7373-OUT;n:type:ShaderForge.SFN_Multiply,id:1401,x:32424,y:31525,varname:node_1401,prsc:2|A-5595-OUT,B-8561-OUT;n:type:ShaderForge.SFN_Lerp,id:5595,x:32270,y:31525,varname:node_5595,prsc:2|A-6078-OUT,B-4914-OUT,T-9751-OUT;n:type:ShaderForge.SFN_Vector1,id:6078,x:31799,y:31794,varname:node_6078,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:8561,x:32270,y:31656,varname:node_8561,prsc:2|A-6078-OUT,B-1689-RGB,T-7789-OUT;n:type:ShaderForge.SFN_Clamp01,id:9751,x:32100,y:31525,varname:node_9751,prsc:2|IN-6331-OUT;n:type:ShaderForge.SFN_OneMinus,id:3312,x:31799,y:31656,varname:node_3312,prsc:2|IN-6331-OUT;n:type:ShaderForge.SFN_Add,id:7321,x:31950,y:31656,varname:node_7321,prsc:2|A-6078-OUT,B-3312-OUT;n:type:ShaderForge.SFN_Clamp01,id:7789,x:32100,y:31656,varname:node_7789,prsc:2|IN-7321-OUT;n:type:ShaderForge.SFN_Slider,id:6331,x:31720,y:31561,ptovrint:False,ptlb:Crossfade Tile / Cubemap,ptin:_CrossfadeTileCubemap,varname:_CrossfadeSurfaceOverlay_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:4914,x:31565,y:31656,varname:node_4914,prsc:2|A-1516-RGB,B-9128-OUT;n:type:ShaderForge.SFN_Vector1,id:9128,x:31565,y:31779,varname:node_9128,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:6792,x:34298,y:32618,ptovrint:False,ptlb:Emission Map,ptin:_EmissionMap,varname:node_6792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1116,x:34298,y:32789,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_1116,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:2869,x:34603,y:32762,varname:node_2869,prsc:2|A-6792-RGB,B-1116-OUT;n:type:ShaderForge.SFN_Tex2d,id:4793,x:34298,y:32449,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_4793,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:554,x:34298,y:32307,varname:node_554,prsc:2|A-1689-A,B-9151-A,C-6552-OUT;n:type:ShaderForge.SFN_Slider,id:6552,x:34219,y:32237,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_6552,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:4566,x:34597,y:32528,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_4566,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:1851,x:34588,y:32622,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_1851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:7199,x:32960,y:32586,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_1700,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_NormalVector,id:5668,x:31225,y:32717,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:7406,x:31225,y:33043,varname:node_7406,prsc:2;n:type:ShaderForge.SFN_Dot,id:5976,x:31418,y:32717,varname:node_5976,prsc:2,dt:0|A-5668-OUT,B-4076-XYZ;n:type:ShaderForge.SFN_Append,id:6270,x:31733,y:32717,varname:node_6270,prsc:2|A-2170-OUT,B-1187-OUT;n:type:ShaderForge.SFN_Vector1,id:1187,x:31577,y:32836,varname:node_1187,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:3789,x:31896,y:32717,varname:node_6405,prsc:2,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False|UVIN-6270-OUT,TEX-8851-TEX;n:type:ShaderForge.SFN_Desaturate,id:7682,x:32062,y:32717,varname:node_7682,prsc:2|COL-3789-RGB;n:type:ShaderForge.SFN_Add,id:309,x:32227,y:32717,varname:node_309,prsc:2|A-7682-OUT,B-7284-OUT;n:type:ShaderForge.SFN_Vector1,id:7284,x:32062,y:32836,varname:node_7284,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Posterize,id:895,x:32570,y:32717,varname:node_895,prsc:2|IN-390-OUT,STPS-2376-OUT;n:type:ShaderForge.SFN_Vector1,id:2376,x:32393,y:32833,varname:node_2376,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:390,x:32393,y:32717,varname:node_390,prsc:2|IN-309-OUT;n:type:ShaderForge.SFN_Clamp01,id:2170,x:31577,y:32717,varname:node_2170,prsc:2|IN-5976-OUT;n:type:ShaderForge.SFN_Subtract,id:3981,x:32721,y:32717,varname:node_3981,prsc:2|A-895-OUT,B-3629-OUT;n:type:ShaderForge.SFN_Vector1,id:3629,x:32570,y:32833,varname:node_3629,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector4Property,id:4076,x:31225,y:32881,ptovrint:False,ptlb:Static Toon Light,ptin:_StaticToonLight,varname:node_8394,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_SwitchProperty,id:5241,x:32939,y:32970,ptovrint:False,ptlb:Dynamic Toon Lighting,ptin:_DynamicToonLighting,varname:node_215,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8199-OUT,B-4758-OUT;n:type:ShaderForge.SFN_Dot,id:5442,x:31418,y:33043,varname:node_5442,prsc:2,dt:0|A-5668-OUT,B-7406-OUT;n:type:ShaderForge.SFN_Append,id:4118,x:31733,y:33043,varname:node_4118,prsc:2|A-803-OUT,B-4190-OUT;n:type:ShaderForge.SFN_Vector1,id:4190,x:31577,y:33162,varname:node_4190,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:3544,x:31896,y:33043,varname:_Ramp_copy,prsc:2,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False|UVIN-4118-OUT,TEX-8851-TEX;n:type:ShaderForge.SFN_Desaturate,id:1387,x:32062,y:33043,varname:node_1387,prsc:2|COL-3544-RGB;n:type:ShaderForge.SFN_Add,id:5764,x:32227,y:33043,varname:node_5764,prsc:2|A-1387-OUT,B-926-OUT;n:type:ShaderForge.SFN_Vector1,id:926,x:32062,y:33162,varname:node_926,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Posterize,id:5546,x:32570,y:33043,varname:node_5546,prsc:2|IN-3326-OUT,STPS-6079-OUT;n:type:ShaderForge.SFN_Vector1,id:6079,x:32393,y:33159,varname:node_6079,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:3326,x:32393,y:33043,varname:node_3326,prsc:2|IN-5764-OUT;n:type:ShaderForge.SFN_Clamp01,id:803,x:31577,y:33043,varname:node_803,prsc:2|IN-5442-OUT;n:type:ShaderForge.SFN_Subtract,id:4758,x:32721,y:33043,varname:node_4758,prsc:2|A-5546-OUT,B-5707-OUT;n:type:ShaderForge.SFN_Vector1,id:5707,x:32570,y:33159,varname:node_5707,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:8851,x:31733,y:32876,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_5894,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:8199,x:32721,y:32987,varname:node_8199,prsc:2,v1:1;proporder:7241-9151-6611-1689-2514-5322-1516-6331-5241-4076-8851-6792-1116-7199-4793-4566-1851-6552;pass:END;sub:END;*/

Shader "NoeNoe/NoeNoe Overlay Shader (PBR)" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Main texture (RGB)", 2D) = "white" {}
        _CrossfadeSurfaceOverlay ("Crossfade Surface / Overlay", Range(0, 2)) = 1
        _TileOverlay ("Tile Overlay", 2D) = "white" {}
        _TileSpeedX ("Tile Speed X", Range(-1, 1)) = 0
        _TileSpeedY ("Tile Speed Y", Range(-1, 1)) = 0
        _CubemapOverlay ("Cubemap Overlay", Cube) = "_Skybox" {}
        _CrossfadeTileCubemap ("Crossfade Tile / Cubemap", Range(0, 2)) = 0
        [MaterialToggle] _DynamicToonLighting ("Dynamic Toon Lighting", Float ) = 1
        _StaticToonLight ("Static Toon Light", Vector) = (0,0,0,0)
        _Ramp ("Ramp", 2D) = "white" {}
        _EmissionMap ("Emission Map", 2D) = "white" {}
        _Emission ("Emission", Range(0, 10)) = 0
        _Intensity ("Intensity", Range(0, 10)) = 1
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Metallic ("Metallic", Range(0, 1)) = 0
        _Gloss ("Gloss", Range(0, 1)) = 0
        _Opacity ("Opacity", Range(0, 1)) = 1
    }
    SubShader {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _TileOverlay; uniform float4 _TileOverlay_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _CrossfadeSurfaceOverlay;
            uniform float _TileSpeedX;
            uniform float _TileSpeedY;
            uniform samplerCUBE _CubemapOverlay;
            uniform float _CrossfadeTileCubemap;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Emission;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _Intensity;
            uniform float4 _StaticToonLight;
            uniform fixed _DynamicToonLighting;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float node_7920 = 1.0;
                float node_6078 = 1.0;
                float3 node_769 = viewDirection.rgb;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_TileSpeedX),(node_9795*_TileSpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _TileOverlay_var = tex2D(_TileOverlay,TRANSFORM_TEX(node_1431, _TileOverlay));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_6270 = float2(saturate(dot(normalDirection,_StaticToonLight.rgb)),0.2);
                float4 node_6405 = tex2D(_Ramp,TRANSFORM_TEX(node_6270, _Ramp));
                float node_2376 = 3.0;
                float2 node_4118 = float2(saturate(dot(normalDirection,lightDirection)),0.2);
                float4 _Ramp_copy = tex2D(_Ramp,TRANSFORM_TEX(node_4118, _Ramp));
                float node_6079 = 3.0;
                float3 diffuseColor = (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*(floor(saturate((dot(node_6405.rgb,float3(0.3,0.59,0.11))+0.8)) * node_2376) / (node_2376 - 1)-0.5)*lerp( 1.0, (floor(saturate((dot(_Ramp_copy.rgb,float3(0.3,0.59,0.11))+0.8)) * node_6079) / (node_6079 - 1)-0.5), _DynamicToonLighting )*_Intensity); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                float3 emissive = (_EmissionMap_var.rgb*_Emission);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _TileOverlay; uniform float4 _TileOverlay_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _CrossfadeSurfaceOverlay;
            uniform float _TileSpeedX;
            uniform float _TileSpeedY;
            uniform samplerCUBE _CubemapOverlay;
            uniform float _CrossfadeTileCubemap;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Emission;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _Intensity;
            uniform float4 _StaticToonLight;
            uniform fixed _DynamicToonLighting;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = _Gloss;
                float perceptualRoughness = 1.0 - _Gloss;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic;
                float specularMonochrome;
                float node_7920 = 1.0;
                float node_6078 = 1.0;
                float3 node_769 = viewDirection.rgb;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_TileSpeedX),(node_9795*_TileSpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _TileOverlay_var = tex2D(_TileOverlay,TRANSFORM_TEX(node_1431, _TileOverlay));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_6270 = float2(saturate(dot(normalDirection,_StaticToonLight.rgb)),0.2);
                float4 node_6405 = tex2D(_Ramp,TRANSFORM_TEX(node_6270, _Ramp));
                float node_2376 = 3.0;
                float2 node_4118 = float2(saturate(dot(normalDirection,lightDirection)),0.2);
                float4 _Ramp_copy = tex2D(_Ramp,TRANSFORM_TEX(node_4118, _Ramp));
                float node_6079 = 3.0;
                float3 diffuseColor = (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*(floor(saturate((dot(node_6405.rgb,float3(0.3,0.59,0.11))+0.8)) * node_2376) / (node_2376 - 1)-0.5)*lerp( 1.0, (floor(saturate((dot(_Ramp_copy.rgb,float3(0.3,0.59,0.11))+0.8)) * node_6079) / (node_6079 - 1)-0.5), _DynamicToonLighting )*_Intensity); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _TileOverlay; uniform float4 _TileOverlay_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _CrossfadeSurfaceOverlay;
            uniform float _TileSpeedX;
            uniform float _TileSpeedY;
            uniform samplerCUBE _CubemapOverlay;
            uniform float _CrossfadeTileCubemap;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Emission;
            uniform float _Metallic;
            uniform float _Gloss;
            uniform float _Intensity;
            uniform float4 _StaticToonLight;
            uniform fixed _DynamicToonLighting;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                o.Emission = (_EmissionMap_var.rgb*_Emission);
                
                float node_7920 = 1.0;
                float node_6078 = 1.0;
                float3 node_769 = viewDirection.rgb;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_TileSpeedX),(node_9795*_TileSpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _TileOverlay_var = tex2D(_TileOverlay,TRANSFORM_TEX(node_1431, _TileOverlay));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_6270 = float2(saturate(dot(normalDirection,_StaticToonLight.rgb)),0.2);
                float4 node_6405 = tex2D(_Ramp,TRANSFORM_TEX(node_6270, _Ramp));
                float node_2376 = 3.0;
                float2 node_4118 = float2(saturate(dot(normalDirection,lightDirection)),0.2);
                float4 _Ramp_copy = tex2D(_Ramp,TRANSFORM_TEX(node_4118, _Ramp));
                float node_6079 = 3.0;
                float3 diffColor = (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*(floor(saturate((dot(node_6405.rgb,float3(0.3,0.59,0.11))+0.8)) * node_2376) / (node_2376 - 1)-0.5)*lerp( 1.0, (floor(saturate((dot(_Ramp_copy.rgb,float3(0.3,0.59,0.11))+0.8)) * node_6079) / (node_6079 - 1)-0.5), _DynamicToonLighting )*_Intensity);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metallic, specColor, specularMonochrome );
                float roughness = 1.0 - _Gloss;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
