// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33507,y:32657,varname:node_4013,prsc:2|diff-4765-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:4765,x:33217,y:32655,varname:node_4765,prsc:2|H-979-OUT,S-2115-OUT,V-2060-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:968,x:32518,y:32657,varname:node_968,prsc:2|IN-749-RGB;n:type:ShaderForge.SFN_Tex2d,id:749,x:32257,y:32655,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_749,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8173c8966892d4d01a7d113aedcb52b6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:979,x:32100,y:32458,ptovrint:False,ptlb:ColorShift,ptin:_ColorShift,varname:node_979,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Vector1,id:4966,x:32407,y:32936,varname:node_4966,prsc:2,v1:1;n:type:ShaderForge.SFN_Power,id:2115,x:32860,y:32677,varname:node_2115,prsc:2|VAL-968-SOUT,EXP-1493-OUT;n:type:ShaderForge.SFN_Slider,id:5003,x:32100,y:33075,ptovrint:False,ptlb:Saturation,ptin:_Saturation,varname:node_5003,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:6.153846,max:10;n:type:ShaderForge.SFN_Divide,id:1493,x:32647,y:32954,varname:node_1493,prsc:2|A-4966-OUT,B-5003-OUT;n:type:ShaderForge.SFN_Relay,id:2060,x:33026,y:32882,varname:node_2060,prsc:2|IN-968-VOUT;proporder:749-979-5003;pass:END;sub:END;*/

Shader "GGAvatar Shaders/ClothesShader" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _ColorShift ("ColorShift", Range(0, 1)) = 0
        _Saturation ("Saturation", Range(1, 10)) = 6.153846
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _ColorShift;
            uniform float _Saturation;
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
                float4 node_968_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_968_p = lerp(float4(float4(_MainTex_var.rgb,0.0).zy, node_968_k.wz), float4(float4(_MainTex_var.rgb,0.0).yz, node_968_k.xy), step(float4(_MainTex_var.rgb,0.0).z, float4(_MainTex_var.rgb,0.0).y));
                float4 node_968_q = lerp(float4(node_968_p.xyw, float4(_MainTex_var.rgb,0.0).x), float4(float4(_MainTex_var.rgb,0.0).x, node_968_p.yzx), step(node_968_p.x, float4(_MainTex_var.rgb,0.0).x));
                float node_968_d = node_968_q.x - min(node_968_q.w, node_968_q.y);
                float node_968_e = 1.0e-10;
                float3 node_968 = float3(abs(node_968_q.z + (node_968_q.w - node_968_q.y) / (6.0 * node_968_d + node_968_e)), node_968_d / (node_968_q.x + node_968_e), node_968_q.x);;
                float3 diffuseColor = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(_ColorShift+float3(0.0,-1.0/3.0,1.0/3.0)))-1),pow(node_968.g,(1.0/_Saturation)))*node_968.b);
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
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _ColorShift;
            uniform float _Saturation;
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
                float4 node_968_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_968_p = lerp(float4(float4(_MainTex_var.rgb,0.0).zy, node_968_k.wz), float4(float4(_MainTex_var.rgb,0.0).yz, node_968_k.xy), step(float4(_MainTex_var.rgb,0.0).z, float4(_MainTex_var.rgb,0.0).y));
                float4 node_968_q = lerp(float4(node_968_p.xyw, float4(_MainTex_var.rgb,0.0).x), float4(float4(_MainTex_var.rgb,0.0).x, node_968_p.yzx), step(node_968_p.x, float4(_MainTex_var.rgb,0.0).x));
                float node_968_d = node_968_q.x - min(node_968_q.w, node_968_q.y);
                float node_968_e = 1.0e-10;
                float3 node_968 = float3(abs(node_968_q.z + (node_968_q.w - node_968_q.y) / (6.0 * node_968_d + node_968_e)), node_968_d / (node_968_q.x + node_968_e), node_968_q.x);;
                float3 diffuseColor = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(_ColorShift+float3(0.0,-1.0/3.0,1.0/3.0)))-1),pow(node_968.g,(1.0/_Saturation)))*node_968.b);
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
