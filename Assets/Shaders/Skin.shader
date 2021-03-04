// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:39565,y:32635,varname:node_4013,prsc:2|diff-6636-OUT;n:type:ShaderForge.SFN_Max,id:1064,x:32662,y:32437,varname:node_1064,prsc:2|A-3312-R,B-3312-G,C-3312-B;n:type:ShaderForge.SFN_Min,id:8317,x:32662,y:32597,varname:node_8317,prsc:2|A-3312-R,B-3312-G,C-3312-B;n:type:ShaderForge.SFN_Divide,id:398,x:33218,y:32427,cmnt:Lighting,varname:node_398,prsc:2|A-483-OUT,B-1946-OUT;n:type:ShaderForge.SFN_Vector1,id:1946,x:32933,y:32581,varname:node_1946,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:483,x:32896,y:32437,varname:node_483,prsc:2|A-1064-OUT,B-8317-OUT;n:type:ShaderForge.SFN_Subtract,id:5798,x:32909,y:32176,varname:node_5798,prsc:2|A-1064-OUT,B-8317-OUT;n:type:ShaderForge.SFN_If,id:9594,x:33800,y:31879,varname:node_9594,prsc:2|A-1358-OUT,B-5798-OUT,GT-2656-OUT,EQ-2656-OUT,LT-1974-OUT;n:type:ShaderForge.SFN_If,id:1974,x:33583,y:32437,varname:node_1974,prsc:2|A-398-OUT,B-5777-OUT,GT-7062-OUT,EQ-2844-OUT,LT-2844-OUT;n:type:ShaderForge.SFN_Vector1,id:5777,x:33368,y:32471,varname:node_5777,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Divide,id:2844,x:33339,y:32625,varname:node_2844,prsc:2|A-5798-OUT,B-483-OUT;n:type:ShaderForge.SFN_Subtract,id:8725,x:32979,y:32801,varname:node_8725,prsc:2|A-2146-OUT,B-1064-OUT;n:type:ShaderForge.SFN_Vector1,id:2146,x:32769,y:32801,varname:node_2146,prsc:2,v1:2;n:type:ShaderForge.SFN_Divide,id:7062,x:33371,y:32801,varname:node_7062,prsc:2|A-5798-OUT,B-5516-OUT;n:type:ShaderForge.SFN_Subtract,id:5516,x:33155,y:32801,varname:node_5516,prsc:2|A-8725-OUT,B-8317-OUT;n:type:ShaderForge.SFN_Subtract,id:6087,x:32886,y:33239,varname:node_6087,prsc:2|A-1064-OUT,B-3312-R;n:type:ShaderForge.SFN_Subtract,id:5065,x:32886,y:33381,varname:node_5065,prsc:2|A-1064-OUT,B-3312-G;n:type:ShaderForge.SFN_Subtract,id:2775,x:32886,y:33519,varname:node_2775,prsc:2|A-1064-OUT,B-3312-B;n:type:ShaderForge.SFN_Divide,id:9515,x:33178,y:33239,varname:node_9515,prsc:2|A-6087-OUT,B-5798-OUT;n:type:ShaderForge.SFN_Divide,id:3891,x:33178,y:33388,varname:node_3891,prsc:2|A-5065-OUT,B-5798-OUT;n:type:ShaderForge.SFN_Divide,id:7490,x:33178,y:33531,varname:node_7490,prsc:2|A-2775-OUT,B-5798-OUT;n:type:ShaderForge.SFN_Subtract,id:1901,x:33561,y:33241,varname:node_1901,prsc:2|A-7490-OUT,B-3891-OUT;n:type:ShaderForge.SFN_Subtract,id:4689,x:33768,y:33415,varname:node_4689,prsc:2|A-1526-OUT,B-7490-OUT;n:type:ShaderForge.SFN_Subtract,id:6593,x:33768,y:33564,varname:node_6593,prsc:2|A-3258-OUT,B-7191-OUT;n:type:ShaderForge.SFN_Add,id:1526,x:33561,y:33415,varname:node_1526,prsc:2|A-9515-OUT,B-3436-OUT;n:type:ShaderForge.SFN_Vector1,id:3436,x:33418,y:33449,varname:node_3436,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:4228,x:33418,y:33609,varname:node_4228,prsc:2,v1:4;n:type:ShaderForge.SFN_Add,id:3258,x:33561,y:33564,varname:node_3258,prsc:2|A-3891-OUT,B-4228-OUT;n:type:ShaderForge.SFN_If,id:957,x:34237,y:32996,varname:node_957,prsc:2|A-3312-R,B-9575-OUT,GT-1901-OUT,EQ-1901-OUT,LT-4286-OUT;n:type:ShaderForge.SFN_If,id:4286,x:34125,y:33203,varname:node_4286,prsc:2|A-3312-G,B-7026-OUT,GT-4689-OUT,EQ-4689-OUT,LT-6593-OUT;n:type:ShaderForge.SFN_Vector1,id:8612,x:34351,y:33137,varname:node_8612,prsc:2,v1:6;n:type:ShaderForge.SFN_Divide,id:7879,x:34541,y:33001,cmnt:Hue,varname:node_7879,prsc:2|A-957-OUT,B-8612-OUT;n:type:ShaderForge.SFN_If,id:4303,x:34971,y:32998,varname:node_4303,prsc:2|A-3526-OUT,B-1078-OUT,GT-3526-OUT,EQ-3526-OUT,LT-8038-OUT;n:type:ShaderForge.SFN_Vector1,id:1078,x:34781,y:33083,varname:node_1078,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:8038,x:34797,y:33216,varname:node_8038,prsc:2|A-7879-OUT,B-2905-OUT;n:type:ShaderForge.SFN_Vector1,id:2905,x:34496,y:33213,varname:node_2905,prsc:2,v1:1;n:type:ShaderForge.SFN_If,id:7428,x:35178,y:32998,varname:node_7428,prsc:2|A-4303-OUT,B-3006-OUT,GT-2003-OUT,EQ-4303-OUT,LT-4303-OUT;n:type:ShaderForge.SFN_Subtract,id:2003,x:35078,y:33230,varname:node_2003,prsc:2|A-4303-OUT,B-4714-OUT;n:type:ShaderForge.SFN_Multiply,id:2190,x:35879,y:32123,varname:node_2190,prsc:2|A-5605-OUT,B-3850-OUT;n:type:ShaderForge.SFN_Vector1,id:3850,x:35657,y:32205,varname:node_3850,prsc:2,v1:2;n:type:ShaderForge.SFN_Subtract,id:7002,x:36203,y:32124,varname:node_7002,prsc:2|A-2190-OUT,B-7643-OUT;n:type:ShaderForge.SFN_Vector1,id:7643,x:36018,y:32201,varname:node_7643,prsc:2,v1:1;n:type:ShaderForge.SFN_Abs,id:9471,x:36425,y:32124,varname:node_9471,prsc:2|IN-7002-OUT;n:type:ShaderForge.SFN_Subtract,id:4750,x:36634,y:32042,varname:node_4750,prsc:2|A-2595-OUT,B-9471-OUT;n:type:ShaderForge.SFN_Multiply,id:7738,x:36881,y:31943,varname:node_7738,prsc:2|A-9594-OUT,B-4750-OUT;n:type:ShaderForge.SFN_Multiply,id:6894,x:35909,y:33012,varname:node_6894,prsc:2|A-7428-OUT,B-4020-OUT;n:type:ShaderForge.SFN_Vector1,id:4020,x:35651,y:33292,varname:node_4020,prsc:2,v1:6;n:type:ShaderForge.SFN_Fmod,id:8849,x:36334,y:32555,varname:node_8849,prsc:2|A-6894-OUT,B-7735-OUT;n:type:ShaderForge.SFN_Vector1,id:7735,x:36228,y:32712,varname:node_7735,prsc:2,v1:2;n:type:ShaderForge.SFN_Subtract,id:975,x:36595,y:32546,varname:node_975,prsc:2|A-8849-OUT,B-3453-OUT;n:type:ShaderForge.SFN_Vector1,id:3453,x:36380,y:32790,varname:node_3453,prsc:2,v1:1;n:type:ShaderForge.SFN_Abs,id:6542,x:36810,y:32546,varname:node_6542,prsc:2|IN-975-OUT;n:type:ShaderForge.SFN_Subtract,id:2540,x:36997,y:32484,varname:node_2540,prsc:2|A-703-OUT,B-6542-OUT;n:type:ShaderForge.SFN_Multiply,id:197,x:37248,y:32209,varname:node_197,prsc:2|A-7738-OUT,B-2540-OUT;n:type:ShaderForge.SFN_Append,id:3104,x:37957,y:31741,varname:node_3104,prsc:2|A-7738-OUT,B-197-OUT,C-2547-OUT;n:type:ShaderForge.SFN_Vector1,id:2547,x:37462,y:32632,varname:node_2547,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:2259,x:37957,y:31927,varname:node_2259,prsc:2|A-197-OUT,B-7738-OUT,C-2547-OUT;n:type:ShaderForge.SFN_Append,id:9856,x:37957,y:32105,varname:node_9856,prsc:2|A-2547-OUT,B-7738-OUT,C-197-OUT;n:type:ShaderForge.SFN_Append,id:5785,x:37957,y:32278,varname:node_5785,prsc:2|A-2547-OUT,B-197-OUT,C-7738-OUT;n:type:ShaderForge.SFN_Append,id:4724,x:37957,y:32444,varname:node_4724,prsc:2|A-197-OUT,B-2547-OUT,C-7738-OUT;n:type:ShaderForge.SFN_Append,id:8182,x:37957,y:32597,varname:node_8182,prsc:2|A-7738-OUT,B-2547-OUT,C-197-OUT;n:type:ShaderForge.SFN_If,id:1365,x:38608,y:32769,varname:node_1365,prsc:2|A-6894-OUT,B-2444-OUT,GT-2938-OUT,EQ-2938-OUT,LT-3104-OUT;n:type:ShaderForge.SFN_Vector1,id:2444,x:38301,y:32802,varname:node_2444,prsc:2,v1:1;n:type:ShaderForge.SFN_If,id:2938,x:38608,y:32935,varname:node_2938,prsc:2|A-6894-OUT,B-1427-OUT,GT-7836-OUT,EQ-7836-OUT,LT-2259-OUT;n:type:ShaderForge.SFN_Vector1,id:1427,x:38301,y:32971,varname:node_1427,prsc:2,v1:2;n:type:ShaderForge.SFN_If,id:1165,x:38608,y:33285,varname:node_1165,prsc:2|A-6894-OUT,B-6522-OUT,GT-90-OUT,EQ-90-OUT,LT-5785-OUT;n:type:ShaderForge.SFN_Vector1,id:6522,x:38301,y:33321,varname:node_6522,prsc:2,v1:4;n:type:ShaderForge.SFN_Vector1,id:2169,x:38301,y:33155,varname:node_2169,prsc:2,v1:3;n:type:ShaderForge.SFN_If,id:7836,x:38608,y:33119,varname:node_7836,prsc:2|A-6894-OUT,B-2169-OUT,GT-1165-OUT,EQ-1165-OUT,LT-9856-OUT;n:type:ShaderForge.SFN_Vector1,id:3939,x:38301,y:33479,varname:node_3939,prsc:2,v1:5;n:type:ShaderForge.SFN_If,id:90,x:38608,y:33443,varname:node_90,prsc:2|A-6894-OUT,B-3939-OUT,GT-8182-OUT,EQ-8182-OUT,LT-4724-OUT;n:type:ShaderForge.SFN_Divide,id:7789,x:37848,y:31471,varname:node_7789,prsc:2|A-7738-OUT,B-8674-OUT;n:type:ShaderForge.SFN_Subtract,id:4780,x:38055,y:31418,varname:node_4780,prsc:2|A-5605-OUT,B-7789-OUT;n:type:ShaderForge.SFN_Vector1,id:8674,x:37583,y:31541,varname:node_8674,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:9488,x:39005,y:32486,varname:node_9488,prsc:2|A-7839-OUT,B-1365-OUT;n:type:ShaderForge.SFN_Vector1,id:1358,x:33263,y:31713,varname:node_1358,prsc:2,v1:0.0001;n:type:ShaderForge.SFN_Vector1,id:2656,x:33247,y:31795,varname:node_2656,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:7839,x:38515,y:31409,varname:node_7839,prsc:2|A-4780-OUT,B-4780-OUT,C-4780-OUT;n:type:ShaderForge.SFN_Slider,id:1256,x:32785,y:31907,ptovrint:False,ptlb:Lightness,ptin:_Lightness,varname:node_1256,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-0.3,cur:0.03495146,max:0.3;n:type:ShaderForge.SFN_Add,id:5605,x:33493,y:32176,varname:node_5605,prsc:2|A-1256-OUT,B-398-OUT;n:type:ShaderForge.SFN_Add,id:3526,x:34735,y:32814,varname:node_3526,prsc:2|A-6380-OUT,B-7879-OUT;n:type:ShaderForge.SFN_Slider,id:6380,x:34352,y:32743,ptovrint:False,ptlb:Hue,ptin:_Hue,varname:node_6380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:3312,x:32180,y:32812,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3312,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4f9991f5a629c4c148d8920ea728e27a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Relay,id:7191,x:33493,y:33835,varname:node_7191,prsc:2|IN-9515-OUT;n:type:ShaderForge.SFN_Relay,id:4714,x:34786,y:33399,varname:node_4714,prsc:2|IN-2905-OUT;n:type:ShaderForge.SFN_Relay,id:3006,x:34948,y:32882,varname:node_3006,prsc:2|IN-2905-OUT;n:type:ShaderForge.SFN_Relay,id:2595,x:36183,y:32011,varname:node_2595,prsc:2|IN-7643-OUT;n:type:ShaderForge.SFN_Relay,id:703,x:36568,y:32448,varname:node_703,prsc:2|IN-3453-OUT;n:type:ShaderForge.SFN_Relay,id:9575,x:32982,y:33012,varname:node_9575,prsc:2|IN-1064-OUT;n:type:ShaderForge.SFN_Relay,id:7026,x:32982,y:33072,varname:node_7026,prsc:2|IN-1064-OUT;n:type:ShaderForge.SFN_Tex2d,id:6515,x:39185,y:32818,ptovrint:False,ptlb:ShiftMask,ptin:_ShiftMask,varname:node_6515,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:6636,x:39359,y:32512,varname:node_6636,prsc:2|A-9488-OUT,B-432-OUT,T-6515-A;n:type:ShaderForge.SFN_Relay,id:5794,x:32936,y:34066,varname:node_5794,prsc:2|IN-3312-RGB;n:type:ShaderForge.SFN_Relay,id:432,x:38449,y:34093,varname:node_432,prsc:2|IN-5794-OUT;proporder:1256-6380-3312-6515;pass:END;sub:END;*/

Shader "GGAvatar Shaders/Skin" {
    Properties {
        _Lightness ("Lightness", Range(-0.3, 0.3)) = 0.03495146
        _Hue ("Hue", Range(0, 1)) = 0
        _MainTex ("MainTex", 2D) = "white" {}
        _ShiftMask ("ShiftMask", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Lightness;
            uniform float _Hue;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _ShiftMask; uniform float4 _ShiftMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_1064 = max(max(_MainTex_var.r,_MainTex_var.g),_MainTex_var.b);
                float node_8317 = min(min(_MainTex_var.r,_MainTex_var.g),_MainTex_var.b);
                float node_483 = (node_1064+node_8317);
                float node_398 = (node_483/2.0); // Lighting
                float node_5605 = (_Lightness+node_398);
                float node_5798 = (node_1064-node_8317);
                float node_9594_if_leA = step(0.0001,node_5798);
                float node_9594_if_leB = step(node_5798,0.0001);
                float node_1974_if_leA = step(node_398,0.5);
                float node_1974_if_leB = step(0.5,node_398);
                float node_2844 = (node_5798/node_483);
                float node_2656 = 0.0;
                float node_7643 = 1.0;
                float node_7738 = (lerp((node_9594_if_leA*lerp((node_1974_if_leA*node_2844)+(node_1974_if_leB*(node_5798/((2.0-node_1064)-node_8317))),node_2844,node_1974_if_leA*node_1974_if_leB))+(node_9594_if_leB*node_2656),node_2656,node_9594_if_leA*node_9594_if_leB)*(node_7643-abs(((node_5605*2.0)-node_7643))));
                float node_4780 = (node_5605-(node_7738/2.0));
                float node_957_if_leA = step(_MainTex_var.r,node_1064);
                float node_957_if_leB = step(node_1064,_MainTex_var.r);
                float node_4286_if_leA = step(_MainTex_var.g,node_1064);
                float node_4286_if_leB = step(node_1064,_MainTex_var.g);
                float node_3891 = ((node_1064-_MainTex_var.g)/node_5798);
                float node_9515 = ((node_1064-_MainTex_var.r)/node_5798);
                float node_7490 = ((node_1064-_MainTex_var.b)/node_5798);
                float node_4689 = ((node_9515+2.0)-node_7490);
                float node_1901 = (node_7490-node_3891);
                float node_7879 = (lerp((node_957_if_leA*lerp((node_4286_if_leA*((node_3891+4.0)-node_9515))+(node_4286_if_leB*node_4689),node_4689,node_4286_if_leA*node_4286_if_leB))+(node_957_if_leB*node_1901),node_1901,node_957_if_leA*node_957_if_leB)/6.0); // Hue
                float node_3526 = (_Hue+node_7879);
                float node_4303_if_leA = step(node_3526,0.0);
                float node_4303_if_leB = step(0.0,node_3526);
                float node_2905 = 1.0;
                float node_4303 = lerp((node_4303_if_leA*(node_7879+node_2905))+(node_4303_if_leB*node_3526),node_3526,node_4303_if_leA*node_4303_if_leB);
                float node_7428_if_leA = step(node_4303,node_2905);
                float node_7428_if_leB = step(node_2905,node_4303);
                float node_6894 = (lerp((node_7428_if_leA*node_4303)+(node_7428_if_leB*(node_4303-node_2905)),node_4303,node_7428_if_leA*node_7428_if_leB)*6.0);
                float node_1365_if_leA = step(node_6894,1.0);
                float node_1365_if_leB = step(1.0,node_6894);
                float node_3453 = 1.0;
                float node_197 = (node_7738*(node_3453-abs((fmod(node_6894,2.0)-node_3453))));
                float node_2547 = 0.0;
                float node_2938_if_leA = step(node_6894,2.0);
                float node_2938_if_leB = step(2.0,node_6894);
                float node_7836_if_leA = step(node_6894,3.0);
                float node_7836_if_leB = step(3.0,node_6894);
                float node_1165_if_leA = step(node_6894,4.0);
                float node_1165_if_leB = step(4.0,node_6894);
                float node_90_if_leA = step(node_6894,5.0);
                float node_90_if_leB = step(5.0,node_6894);
                float3 node_8182 = float3(node_7738,node_2547,node_197);
                float3 node_90 = lerp((node_90_if_leA*float3(node_197,node_2547,node_7738))+(node_90_if_leB*node_8182),node_8182,node_90_if_leA*node_90_if_leB);
                float3 node_1165 = lerp((node_1165_if_leA*float3(node_2547,node_197,node_7738))+(node_1165_if_leB*node_90),node_90,node_1165_if_leA*node_1165_if_leB);
                float3 node_7836 = lerp((node_7836_if_leA*float3(node_2547,node_7738,node_197))+(node_7836_if_leB*node_1165),node_1165,node_7836_if_leA*node_7836_if_leB);
                float3 node_2938 = lerp((node_2938_if_leA*float3(node_197,node_7738,node_2547))+(node_2938_if_leB*node_7836),node_7836,node_2938_if_leA*node_2938_if_leB);
                float4 _ShiftMask_var = tex2D(_ShiftMask,TRANSFORM_TEX(i.uv0, _ShiftMask));
                float3 diffuseColor = lerp((float3(node_4780,node_4780,node_4780)+lerp((node_1365_if_leA*float3(node_7738,node_197,node_2547))+(node_1365_if_leB*node_2938),node_2938,node_1365_if_leA*node_1365_if_leB)),_MainTex_var.rgb,_ShiftMask_var.a);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float _Lightness;
            uniform float _Hue;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _ShiftMask; uniform float4 _ShiftMask_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_1064 = max(max(_MainTex_var.r,_MainTex_var.g),_MainTex_var.b);
                float node_8317 = min(min(_MainTex_var.r,_MainTex_var.g),_MainTex_var.b);
                float node_483 = (node_1064+node_8317);
                float node_398 = (node_483/2.0); // Lighting
                float node_5605 = (_Lightness+node_398);
                float node_5798 = (node_1064-node_8317);
                float node_9594_if_leA = step(0.0001,node_5798);
                float node_9594_if_leB = step(node_5798,0.0001);
                float node_1974_if_leA = step(node_398,0.5);
                float node_1974_if_leB = step(0.5,node_398);
                float node_2844 = (node_5798/node_483);
                float node_2656 = 0.0;
                float node_7643 = 1.0;
                float node_7738 = (lerp((node_9594_if_leA*lerp((node_1974_if_leA*node_2844)+(node_1974_if_leB*(node_5798/((2.0-node_1064)-node_8317))),node_2844,node_1974_if_leA*node_1974_if_leB))+(node_9594_if_leB*node_2656),node_2656,node_9594_if_leA*node_9594_if_leB)*(node_7643-abs(((node_5605*2.0)-node_7643))));
                float node_4780 = (node_5605-(node_7738/2.0));
                float node_957_if_leA = step(_MainTex_var.r,node_1064);
                float node_957_if_leB = step(node_1064,_MainTex_var.r);
                float node_4286_if_leA = step(_MainTex_var.g,node_1064);
                float node_4286_if_leB = step(node_1064,_MainTex_var.g);
                float node_3891 = ((node_1064-_MainTex_var.g)/node_5798);
                float node_9515 = ((node_1064-_MainTex_var.r)/node_5798);
                float node_7490 = ((node_1064-_MainTex_var.b)/node_5798);
                float node_4689 = ((node_9515+2.0)-node_7490);
                float node_1901 = (node_7490-node_3891);
                float node_7879 = (lerp((node_957_if_leA*lerp((node_4286_if_leA*((node_3891+4.0)-node_9515))+(node_4286_if_leB*node_4689),node_4689,node_4286_if_leA*node_4286_if_leB))+(node_957_if_leB*node_1901),node_1901,node_957_if_leA*node_957_if_leB)/6.0); // Hue
                float node_3526 = (_Hue+node_7879);
                float node_4303_if_leA = step(node_3526,0.0);
                float node_4303_if_leB = step(0.0,node_3526);
                float node_2905 = 1.0;
                float node_4303 = lerp((node_4303_if_leA*(node_7879+node_2905))+(node_4303_if_leB*node_3526),node_3526,node_4303_if_leA*node_4303_if_leB);
                float node_7428_if_leA = step(node_4303,node_2905);
                float node_7428_if_leB = step(node_2905,node_4303);
                float node_6894 = (lerp((node_7428_if_leA*node_4303)+(node_7428_if_leB*(node_4303-node_2905)),node_4303,node_7428_if_leA*node_7428_if_leB)*6.0);
                float node_1365_if_leA = step(node_6894,1.0);
                float node_1365_if_leB = step(1.0,node_6894);
                float node_3453 = 1.0;
                float node_197 = (node_7738*(node_3453-abs((fmod(node_6894,2.0)-node_3453))));
                float node_2547 = 0.0;
                float node_2938_if_leA = step(node_6894,2.0);
                float node_2938_if_leB = step(2.0,node_6894);
                float node_7836_if_leA = step(node_6894,3.0);
                float node_7836_if_leB = step(3.0,node_6894);
                float node_1165_if_leA = step(node_6894,4.0);
                float node_1165_if_leB = step(4.0,node_6894);
                float node_90_if_leA = step(node_6894,5.0);
                float node_90_if_leB = step(5.0,node_6894);
                float3 node_8182 = float3(node_7738,node_2547,node_197);
                float3 node_90 = lerp((node_90_if_leA*float3(node_197,node_2547,node_7738))+(node_90_if_leB*node_8182),node_8182,node_90_if_leA*node_90_if_leB);
                float3 node_1165 = lerp((node_1165_if_leA*float3(node_2547,node_197,node_7738))+(node_1165_if_leB*node_90),node_90,node_1165_if_leA*node_1165_if_leB);
                float3 node_7836 = lerp((node_7836_if_leA*float3(node_2547,node_7738,node_197))+(node_7836_if_leB*node_1165),node_1165,node_7836_if_leA*node_7836_if_leB);
                float3 node_2938 = lerp((node_2938_if_leA*float3(node_197,node_7738,node_2547))+(node_2938_if_leB*node_7836),node_7836,node_2938_if_leA*node_2938_if_leB);
                float4 _ShiftMask_var = tex2D(_ShiftMask,TRANSFORM_TEX(i.uv0, _ShiftMask));
                float3 diffuseColor = lerp((float3(node_4780,node_4780,node_4780)+lerp((node_1365_if_leA*float3(node_7738,node_197,node_2547))+(node_1365_if_leB*node_2938),node_2938,node_1365_if_leA*node_1365_if_leB)),_MainTex_var.rgb,_ShiftMask_var.a);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
