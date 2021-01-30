Shader "Unlit/TriangleRenderer"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Coord1 ("Top", Vector) = (0,0,0,0)
        _Coord2 ("Right", Vector) = (0,0,0,0)
        _Coord3 ("Left", Vector) = (0,0,0,0)

        _Color1("Color1", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (1,1,1,1)
        _Color3("Color3", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        // draw after all opaque geometry has been drawn
        Pass 
        {

            ZWrite Off // don't write to depth buffer 
           // in order not to occlude other objects

            Blend SrcAlpha OneMinusSrcAlpha // use alpha blending

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _Coord1;
            float3 _Coord2;
            float3 _Coord3;

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;

            float3 GetBarycentric(float3 p)
            {
                //Get tris
                float3 a = _Coord1;
                float3 b = _Coord2;
                float3 c = _Coord3;

                float3 v0 = b - a;
                float3 v1 = c - a;
                float3 v2 = p - a;

                float3 v0b = c - b;
                float3 v2b = p - b;

                float d00 = dot(v0, v0);
                float d01 = dot(v0, v1);
                float d11 = dot(v1, v1);
                float d20 = dot(v2, v0);
                float d21 = dot(v2, v1);
                float i = 1.0 / (d00 * d11 - d01 * d01);

                float d00b = dot(v0b, v0b);
                float d01b = dot(v0b, -v0);
                float d20b = dot(v2b, v0b);
                float d21b = dot(v2b, -v0);
                float ib = 1.0 / (d00b * d11 - d01b * d01b);


                float3 r;
                r.x = (d11 * d20 - d01 * d21) * i;
                r.y = (d00 * d21 - d01 * d20) * i;
                r.z = (d00b * d21b - d01b * d20b) * ib;
                //r.z = 1.0f - r.x - r.z; // we need r.z to go negative for a quick and dirty triangle

                return r;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                 float3 barycentric = GetBarycentric(float3(i.uv.x, i.uv.y, 0));
                 float4 col = barycentric.x * _Color1 + barycentric.y * _Color2 + barycentric.z * _Color3;
                 if (barycentric.x > 0 && barycentric.y > 0 && barycentric.z > 0)
                 {
                     col.a = 1;
                 }
                 else
                 {
                     col.a = 0;
                 }
                 // sample the texture
                 // fixed4 col = fixed4(barycentric.x, barycentric.y, barycentric.z, alpha);// tex2D(_MainTex, i.uv);
                 // apply fog
                 UNITY_APPLY_FOG(i.fogCoord, col);
                 return col;
             }
             ENDCG
         }
    }
}
