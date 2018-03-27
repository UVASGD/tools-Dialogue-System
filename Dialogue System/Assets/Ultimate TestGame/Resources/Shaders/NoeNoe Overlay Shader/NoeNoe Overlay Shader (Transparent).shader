// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34020,y:32577,varname:node_3138,prsc:2|normal-4793-RGB,emission-2869-OUT,custl-2300-OUT,alpha-554-OUT,olcol-894-RGB;n:type:ShaderForge.SFN_Color,id:7241,x:33039,y:32044,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1689,x:31565,y:31470,ptovrint:False,ptlb:Tile Overlay,ptin:_TileOverlay,varname:node_1689,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1431-OUT;n:type:ShaderForge.SFN_Tex2d,id:9151,x:32702,y:32361,ptovrint:False,ptlb:Main texture (RGB),ptin:_MainTex,varname:node_9151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1700,x:33051,y:32757,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_1700,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_Multiply,id:7815,x:33039,y:32206,varname:node_7815,prsc:2|A-2845-OUT,B-7241-RGB,C-4517-OUT;n:type:ShaderForge.SFN_NormalVector,id:8679,x:31864,y:33235,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:2544,x:31864,y:33587,varname:node_2544,prsc:2;n:type:ShaderForge.SFN_Dot,id:8445,x:32057,y:33235,varname:node_8445,prsc:2,dt:0|A-8679-OUT,B-8394-XYZ;n:type:ShaderForge.SFN_Append,id:5668,x:32372,y:33235,varname:node_5668,prsc:2|A-6756-OUT,B-9311-OUT;n:type:ShaderForge.SFN_Vector1,id:9311,x:32216,y:33354,varname:node_9311,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:6405,x:32535,y:33235,varname:node_6405,prsc:2,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False|UVIN-5668-OUT,TEX-2423-TEX;n:type:ShaderForge.SFN_Desaturate,id:6833,x:32701,y:33235,varname:node_6833,prsc:2|COL-6405-RGB;n:type:ShaderForge.SFN_Add,id:6343,x:32866,y:33235,varname:node_6343,prsc:2|A-6833-OUT,B-6991-OUT;n:type:ShaderForge.SFN_Vector1,id:6991,x:32701,y:33354,varname:node_6991,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Posterize,id:2233,x:33209,y:33235,varname:node_2233,prsc:2|IN-9048-OUT,STPS-4857-OUT;n:type:ShaderForge.SFN_Vector1,id:4857,x:33032,y:33351,varname:node_4857,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:9048,x:33032,y:33235,varname:node_9048,prsc:2|IN-6343-OUT;n:type:ShaderForge.SFN_Vector1,id:4517,x:33039,y:32338,varname:node_4517,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:6756,x:32216,y:33235,varname:node_6756,prsc:2|IN-8445-OUT;n:type:ShaderForge.SFN_Desaturate,id:5160,x:33273,y:32044,varname:node_5160,prsc:2|COL-7815-OUT,DES-3971-OUT;n:type:ShaderForge.SFN_Vector1,id:3971,x:33273,y:31995,varname:node_3971,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Desaturate,id:9411,x:33476,y:32044,varname:node_9411,prsc:2|COL-5160-OUT,DES-8933-OUT;n:type:ShaderForge.SFN_OneMinus,id:8933,x:33273,y:32206,varname:node_8933,prsc:2|IN-7241-A;n:type:ShaderForge.SFN_LightColor,id:951,x:32288,y:32834,varname:node_951,prsc:2;n:type:ShaderForge.SFN_Code,id:6511,x:32455,y:32750,cmnt:ShadeSH9 node lifted from Cubeds shaders,varname:node_6511,prsc:2,code:cgBlAHQAdQByAG4AIABTAGgAYQBkAGUAUwBIADkAKABoAGEAbABmADQAKABuAG8AcgBtAGEAbAAsACAAMQAuADAAKQApADsACgA=,output:2,fname:Function_node_3693,width:247,height:113,input:2,input_1_label:normal|A-8487-OUT;n:type:ShaderForge.SFN_Multiply,id:9979,x:32603,y:32881,varname:node_9979,prsc:2|A-951-RGB,B-2967-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:2967,x:32288,y:32955,varname:node_2967,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:6092,x:33201,y:32838,varname:node_6092,prsc:2|IN-7052-OUT;n:type:ShaderForge.SFN_Vector3,id:8487,x:32288,y:32750,varname:node_8487,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Add,id:7052,x:33051,y:32838,varname:node_7052,prsc:2|A-9207-OUT,B-9979-OUT;n:type:ShaderForge.SFN_ViewVector,id:2089,x:30819,y:31184,cmnt:View Direction UV Mapping by Maki and Snail,varname:node_2089,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:4810,x:30819,y:31322,varname:node_4810,prsc:2,cc1:2,cc2:0,cc3:1,cc4:-1|IN-2089-OUT;n:type:ShaderForge.SFN_Pi,id:2632,x:30852,y:31460,varname:node_2632,prsc:2;n:type:ShaderForge.SFN_Negate,id:3403,x:30990,y:31460,varname:node_3403,prsc:2|IN-2632-OUT;n:type:ShaderForge.SFN_ArcCos,id:1044,x:30990,y:31332,varname:node_1044,prsc:2|IN-4810-B;n:type:ShaderForge.SFN_Divide,id:1808,x:31166,y:31470,varname:node_1808,prsc:2|A-1044-OUT,B-3403-OUT;n:type:ShaderForge.SFN_ArcTan2,id:5315,x:30990,y:31184,varname:node_5315,prsc:2,attp:2|A-4810-R,B-4810-G;n:type:ShaderForge.SFN_Append,id:6711,x:31166,y:31332,varname:node_6711,prsc:2|A-5315-OUT,B-1808-OUT;n:type:ShaderForge.SFN_OneMinus,id:9020,x:31340,y:31332,varname:node_9020,prsc:2|IN-6711-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4903,x:31340,y:31470,varname:node_4903,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9020-OUT;n:type:ShaderForge.SFN_Multiply,id:2300,x:33769,y:32834,varname:node_2300,prsc:2|A-9411-OUT,B-1700-OUT,C-6092-OUT,D-7132-OUT,E-215-OUT;n:type:ShaderForge.SFN_Multiply,id:2845,x:32868,y:32206,varname:node_2845,prsc:2|A-3107-OUT,B-6612-OUT;n:type:ShaderForge.SFN_Lerp,id:3107,x:32702,y:32082,varname:node_3107,prsc:2|A-7920-OUT,B-1401-OUT,T-8759-OUT;n:type:ShaderForge.SFN_Vector1,id:7920,x:32151,y:32352,varname:node_7920,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:6612,x:32702,y:32206,varname:node_6612,prsc:2|A-7920-OUT,B-9151-RGB,T-6835-OUT;n:type:ShaderForge.SFN_Clamp01,id:8759,x:32477,y:32072,varname:node_8759,prsc:2|IN-6611-OUT;n:type:ShaderForge.SFN_OneMinus,id:9601,x:31988,y:32209,varname:node_9601,prsc:2|IN-6611-OUT;n:type:ShaderForge.SFN_Add,id:748,x:32151,y:32209,varname:node_748,prsc:2|A-7920-OUT,B-9601-OUT;n:type:ShaderForge.SFN_Clamp01,id:6835,x:32314,y:32209,varname:node_6835,prsc:2|IN-748-OUT;n:type:ShaderForge.SFN_Slider,id:6611,x:32157,y:32139,ptovrint:False,ptlb:Crossfade Surface / Overlay,ptin:_CrossfadeSurfaceOverlay,varname:node_6611,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Append,id:1986,x:31571,y:31183,varname:node_1986,prsc:2|A-4097-OUT,B-3842-OUT;n:type:ShaderForge.SFN_Time,id:47,x:31571,y:31332,varname:node_47,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7401,x:31752,y:31193,varname:node_7401,prsc:2|A-1986-OUT,B-47-T;n:type:ShaderForge.SFN_Add,id:1431,x:31752,y:31332,varname:node_1431,prsc:2|A-7401-OUT,B-4903-OUT;n:type:ShaderForge.SFN_Slider,id:2514,x:31414,y:30967,ptovrint:False,ptlb:Tile Speed X,ptin:_TileSpeedX,varname:node_2514,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:4097,x:31752,y:30903,varname:node_4097,prsc:2|A-9795-OUT,B-2514-OUT;n:type:ShaderForge.SFN_Vector1,id:9795,x:31571,y:30903,varname:node_9795,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:3842,x:31752,y:31043,varname:node_3842,prsc:2|A-9795-OUT,B-5322-OUT;n:type:ShaderForge.SFN_Slider,id:5322,x:31414,y:31048,ptovrint:False,ptlb:Tile Speed Y,ptin:_TileSpeedY,varname:node_5322,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Subtract,id:7132,x:33360,y:33235,varname:node_7132,prsc:2|A-2233-OUT,B-2385-OUT;n:type:ShaderForge.SFN_Vector1,id:2385,x:33209,y:33351,varname:node_2385,prsc:2,v1:0.5;n:type:ShaderForge.SFN_SwitchProperty,id:9207,x:32880,y:32730,ptovrint:False,ptlb:No Light Shading,ptin:_NoLightShading,varname:node_9207,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6511-OUT,B-3482-OUT;n:type:ShaderForge.SFN_Vector1,id:3482,x:32880,y:32667,varname:node_3482,prsc:2,v1:0;n:type:ShaderForge.SFN_ViewVector,id:4308,x:31022,y:31835,varname:node_4308,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:769,x:31022,y:31656,varname:node_769,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-4308-OUT;n:type:ShaderForge.SFN_Multiply,id:5696,x:31180,y:31775,varname:node_5696,prsc:2|A-769-G,B-41-OUT;n:type:ShaderForge.SFN_Vector1,id:41,x:30880,y:31822,varname:node_41,prsc:2,v1:-1;n:type:ShaderForge.SFN_Append,id:7373,x:31180,y:31656,varname:node_7373,prsc:2|A-769-R,B-5696-OUT,C-769-B;n:type:ShaderForge.SFN_Cubemap,id:1516,x:31340,y:31656,ptovrint:False,ptlb:Cubemap Overlay,ptin:_CubemapOverlay,varname:node_7018,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,pvfc:0|DIR-7373-OUT;n:type:ShaderForge.SFN_Multiply,id:1401,x:32424,y:31525,varname:node_1401,prsc:2|A-5595-OUT,B-8561-OUT;n:type:ShaderForge.SFN_Lerp,id:5595,x:32270,y:31525,varname:node_5595,prsc:2|A-6078-OUT,B-4914-OUT,T-9751-OUT;n:type:ShaderForge.SFN_Vector1,id:6078,x:31799,y:31794,varname:node_6078,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:8561,x:32270,y:31656,varname:node_8561,prsc:2|A-6078-OUT,B-1689-RGB,T-7789-OUT;n:type:ShaderForge.SFN_Clamp01,id:9751,x:32100,y:31525,varname:node_9751,prsc:2|IN-6331-OUT;n:type:ShaderForge.SFN_OneMinus,id:3312,x:31799,y:31656,varname:node_3312,prsc:2|IN-6331-OUT;n:type:ShaderForge.SFN_Add,id:7321,x:31950,y:31656,varname:node_7321,prsc:2|A-6078-OUT,B-3312-OUT;n:type:ShaderForge.SFN_Clamp01,id:7789,x:32100,y:31656,varname:node_7789,prsc:2|IN-7321-OUT;n:type:ShaderForge.SFN_Slider,id:6331,x:31720,y:31561,ptovrint:False,ptlb:Crossfade Tile / Cubemap,ptin:_CrossfadeTileCubemap,varname:_CrossfadeSurfaceOverlay_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:4914,x:31565,y:31656,varname:node_4914,prsc:2|A-1516-RGB,B-9128-OUT;n:type:ShaderForge.SFN_Vector1,id:9128,x:31565,y:31779,varname:node_9128,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector4Property,id:8394,x:31864,y:33399,ptovrint:False,ptlb:Static Toon Light,ptin:_StaticToonLight,varname:node_8394,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_SwitchProperty,id:215,x:33582,y:33499,ptovrint:False,ptlb:Dynamic Toon Lighting,ptin:_DynamicToonLighting,varname:node_215,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6296-OUT,B-5474-OUT;n:type:ShaderForge.SFN_Tex2d,id:6792,x:34298,y:32618,ptovrint:False,ptlb:Emission Map,ptin:_EmissionMap,varname:node_6792,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:1116,x:34298,y:32789,ptovrint:False,ptlb:Emission,ptin:_Emission,varname:node_1116,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:2869,x:34603,y:32762,varname:node_2869,prsc:2|A-6792-RGB,B-1116-OUT;n:type:ShaderForge.SFN_Tex2d,id:4793,x:34298,y:32449,ptovrint:False,ptlb:Normal Map,ptin:_NormalMap,varname:node_4793,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ViewPosition,id:2862,x:34290,y:32900,varname:node_2862,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:1311,x:34290,y:33015,varname:node_1311,prsc:2;n:type:ShaderForge.SFN_Distance,id:5541,x:34455,y:32900,varname:node_5541,prsc:2|A-2862-XYZ,B-1311-XYZ;n:type:ShaderForge.SFN_Multiply,id:5276,x:34603,y:32900,varname:node_5276,prsc:2|A-5541-OUT,B-8257-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9899,x:34290,y:33165,ptovrint:False,ptlb:Outline Width,ptin:_OutlineWidth,varname:node_9899,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1805,x:34603,y:33049,ptovrint:False,ptlb:Screen-Space Outline,ptin:_ScreenSpaceOutline,varname:node_1805,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-8257-OUT,B-5276-OUT;n:type:ShaderForge.SFN_Color,id:894,x:33901,y:33094,ptovrint:False,ptlb:Outline Color,ptin:_OutlineColor,varname:node_894,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:8257,x:34455,y:33049,varname:node_8257,prsc:2|A-9899-OUT,B-2838-OUT;n:type:ShaderForge.SFN_Vector1,id:2838,x:34290,y:33223,varname:node_2838,prsc:2,v1:0.001;n:type:ShaderForge.SFN_Multiply,id:554,x:34298,y:32307,varname:node_554,prsc:2|A-1689-A,B-9151-A,C-6552-OUT;n:type:ShaderForge.SFN_Slider,id:6552,x:34219,y:32237,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_6552,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:6296,x:33360,y:33521,varname:node_6296,prsc:2,v1:1;n:type:ShaderForge.SFN_Dot,id:3726,x:32057,y:33577,varname:node_3726,prsc:2,dt:0|A-8679-OUT,B-2544-OUT;n:type:ShaderForge.SFN_Append,id:2264,x:32372,y:33577,varname:node_2264,prsc:2|A-2133-OUT,B-1375-OUT;n:type:ShaderForge.SFN_Vector1,id:1375,x:32216,y:33696,varname:node_1375,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Tex2d,id:6516,x:32535,y:33577,varname:_Ramp_copy,prsc:2,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False|UVIN-2264-OUT,TEX-2423-TEX;n:type:ShaderForge.SFN_Desaturate,id:1319,x:32701,y:33577,varname:node_1319,prsc:2|COL-6516-RGB;n:type:ShaderForge.SFN_Add,id:3354,x:32866,y:33577,varname:node_3354,prsc:2|A-1319-OUT,B-3914-OUT;n:type:ShaderForge.SFN_Vector1,id:3914,x:32701,y:33696,varname:node_3914,prsc:2,v1:0.8;n:type:ShaderForge.SFN_Posterize,id:6250,x:33209,y:33577,varname:node_6250,prsc:2|IN-2569-OUT,STPS-7062-OUT;n:type:ShaderForge.SFN_Vector1,id:7062,x:33032,y:33693,varname:node_7062,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:2569,x:33032,y:33577,varname:node_2569,prsc:2|IN-3354-OUT;n:type:ShaderForge.SFN_Clamp01,id:2133,x:32216,y:33577,varname:node_2133,prsc:2|IN-3726-OUT;n:type:ShaderForge.SFN_Subtract,id:5474,x:33360,y:33577,varname:node_5474,prsc:2|A-6250-OUT,B-5551-OUT;n:type:ShaderForge.SFN_Vector1,id:5551,x:33209,y:33693,varname:node_5551,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:2423,x:32372,y:33400,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_2423,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dda255c6e7ca25c4da940fccb27f832d,ntxv:0,isnm:False;proporder:7241-9151-6611-1689-2514-5322-1516-6331-215-8394-2423-9207-6792-1116-1700-4793-9899-894-1805-6552;pass:END;sub:END;*/

Shader "NoeNoe/NoeNoe Overlay Shader (Transparent)" {
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
        [MaterialToggle] _NoLightShading ("No Light Shading", Float ) = 0
        _EmissionMap ("Emission Map", 2D) = "white" {}
        _Emission ("Emission", Range(0, 10)) = 0
        _Intensity ("Intensity", Range(0, 10)) = 1
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _OutlineWidth ("Outline Width", Float ) = 0
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        [MaterialToggle] _ScreenSpaceOutline ("Screen-Space Outline", Float ) = 0
        _Opacity ("Opacity", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _TileOverlay; uniform float4 _TileOverlay_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Intensity;
            float3 Function_node_3693( float3 normal ){
            return ShadeSH9(half4(normal, 1.0));
            
            }
            
            uniform float _CrossfadeSurfaceOverlay;
            uniform float _TileSpeedX;
            uniform float _TileSpeedY;
            uniform fixed _NoLightShading;
            uniform samplerCUBE _CubemapOverlay;
            uniform float _CrossfadeTileCubemap;
            uniform float4 _StaticToonLight;
            uniform fixed _DynamicToonLighting;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Emission;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _Opacity;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                float3 emissive = (_EmissionMap_var.rgb*_Emission);
                float node_7920 = 1.0;
                float node_6078 = 1.0;
                float3 node_769 = viewDirection.rgb;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_TileSpeedX),(node_9795*_TileSpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _TileOverlay_var = tex2D(_TileOverlay,TRANSFORM_TEX(node_1431, _TileOverlay));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_5668 = float2(saturate(dot(normalDirection,_StaticToonLight.rgb)),0.2);
                float4 node_6405 = tex2D(_Ramp,TRANSFORM_TEX(node_5668, _Ramp));
                float node_4857 = 3.0;
                float2 node_2264 = float2(saturate(dot(normalDirection,lightDirection)),0.2);
                float4 _Ramp_copy = tex2D(_Ramp,TRANSFORM_TEX(node_2264, _Ramp));
                float node_7062 = 3.0;
                float3 finalColor = emissive + (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*_Intensity*saturate((lerp( Function_node_3693( float3(0,1,0) ), 0.0, _NoLightShading )+(_LightColor0.rgb*attenuation)))*(floor(saturate((dot(node_6405.rgb,float3(0.3,0.59,0.11))+0.8)) * node_4857) / (node_4857 - 1)-0.5)*lerp( 1.0, (floor(saturate((dot(_Ramp_copy.rgb,float3(0.3,0.59,0.11))+0.8)) * node_7062) / (node_7062 - 1)-0.5), _DynamicToonLighting ));
                return fixed4(finalColor,(_TileOverlay_var.a*_MainTex_var.a*_Opacity));
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _TileOverlay; uniform float4 _TileOverlay_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Intensity;
            float3 Function_node_3693( float3 normal ){
            return ShadeSH9(half4(normal, 1.0));
            
            }
            
            uniform float _CrossfadeSurfaceOverlay;
            uniform float _TileSpeedX;
            uniform float _TileSpeedY;
            uniform fixed _NoLightShading;
            uniform samplerCUBE _CubemapOverlay;
            uniform float _CrossfadeTileCubemap;
            uniform float4 _StaticToonLight;
            uniform fixed _DynamicToonLighting;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Emission;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _Opacity;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_7920 = 1.0;
                float node_6078 = 1.0;
                float3 node_769 = viewDirection.rgb;
                float node_9795 = 0.1;
                float4 node_47 = _Time + _TimeEditor;
                float3 node_4810 = viewDirection.brg;
                float2 node_1431 = ((float2((node_9795*_TileSpeedX),(node_9795*_TileSpeedY))*node_47.g)+(1.0 - float2(((atan2(node_4810.r,node_4810.g)/6.28318530718)+0.5),(acos(node_4810.b)/(-1*3.141592654)))).rg);
                float4 _TileOverlay_var = tex2D(_TileOverlay,TRANSFORM_TEX(node_1431, _TileOverlay));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float2 node_5668 = float2(saturate(dot(normalDirection,_StaticToonLight.rgb)),0.2);
                float4 node_6405 = tex2D(_Ramp,TRANSFORM_TEX(node_5668, _Ramp));
                float node_4857 = 3.0;
                float2 node_2264 = float2(saturate(dot(normalDirection,lightDirection)),0.2);
                float4 _Ramp_copy = tex2D(_Ramp,TRANSFORM_TEX(node_2264, _Ramp));
                float node_7062 = 3.0;
                float3 finalColor = (lerp(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),dot(lerp(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),dot(((lerp(float3(node_7920,node_7920,node_7920),(lerp(float3(node_6078,node_6078,node_6078),(texCUBE(_CubemapOverlay,float3(node_769.r,(node_769.g*(-1.0)),node_769.b)).rgb*2.0),saturate(_CrossfadeTileCubemap))*lerp(float3(node_6078,node_6078,node_6078),_TileOverlay_var.rgb,saturate((node_6078+(1.0 - _CrossfadeTileCubemap))))),saturate(_CrossfadeSurfaceOverlay))*lerp(float3(node_7920,node_7920,node_7920),_MainTex_var.rgb,saturate((node_7920+(1.0 - _CrossfadeSurfaceOverlay)))))*_Color.rgb*1.0),float3(0.3,0.59,0.11)),(-0.5)),float3(0.3,0.59,0.11)),(1.0 - _Color.a))*_Intensity*saturate((lerp( Function_node_3693( float3(0,1,0) ), 0.0, _NoLightShading )+(_LightColor0.rgb*attenuation)))*(floor(saturate((dot(node_6405.rgb,float3(0.3,0.59,0.11))+0.8)) * node_4857) / (node_4857 - 1)-0.5)*lerp( 1.0, (floor(saturate((dot(_Ramp_copy.rgb,float3(0.3,0.59,0.11))+0.8)) * node_7062) / (node_7062 - 1)-0.5), _DynamicToonLighting ));
                return fixed4(finalColor * (_TileOverlay_var.a*_MainTex_var.a*_Opacity),0);
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _Emission;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                o.Emission = (_EmissionMap_var.rgb*_Emission);
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
