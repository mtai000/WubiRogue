// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/EnemyCircleSurfaceShader"
{
	Properties
	{
		_Color("Color",Color) = (1,1,1,0)
		_Center("Center",Vector) = (0.5,0.5,0,0)
		_Radius("Radius",Float) = 0.5
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            float2 _Center;
            float _Radius;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                if(distance(i.uv,_Center) < _Radius)
                    return _Color;
                return fixed4(0,0,0,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"	
}
