Shader "Custom/PlayerHealth"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,0,0,1) 
        _BackgroundColor ("Background Color", Color) = (0,0,0,0) // 背景颜色
        _HealthPercentage ("Health Percentage", Range(0,1)) = 1.0 // 生命值百分比
        _RingThickness ("Ring Thickness", Range(0.01,0.5)) = 0.1
        _Radius("Radius",Range(0.5,3)) = 1.0
        _Center("Center",Vector) = (0.5,0.5,0,0)
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
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _MainColor;
            float4 _BackgroundColor;
            float _HealthPercentage;
            float _RingThickness;
            float _Radius;
            float4 _Center;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * 2.0 - 1.0;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float len = distance(i.uv,_Center);

                float outerRadius = _Radius;
                float innerRadius = outerRadius - _RingThickness;


                if (len > innerRadius && len < outerRadius)
                {
                    float angle = atan2(uv.y - _Center.y, uv.x - _Center.x) / (2.0 * 3.14159) + 0.5;
                    if (angle <= _HealthPercentage)
                        return _MainColor; 
                }
                return _BackgroundColor; 
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
