Shader "Custom/VisualObjectSpaceNormals"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);
                float3 color = (normal + 1) * 0.5;
                float x = (color.x*1.75 <= 1) ? color.x*1.75 : 1;
                float z = (color.z*1.75 <= 1) ? color.z*1.75 : 1;
                color = normalize(color);
                return fixed4(x, z, color.y, 0);
            }
            ENDCG
        }
    }
}
