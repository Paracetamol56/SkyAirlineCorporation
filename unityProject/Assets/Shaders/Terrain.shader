Shader "Custom/Terrain"
{
    Properties {
        // Water
        _WaterColor ("Water Color", Color) = (0,1,0,1)
        _WaterMaxHeight ("Water Max Height", Float) = 0.0
        // Sand
        _SandColor ("Sand Color", Color) = (1,1,0,1)
        _SandMaxHeight ("Sand Max Height", Float) = 0.0
        _SandBlendAmount ("Sand Blend", Range(0,1)) = 0.0
        // Grass
        _GrassColor ("Grass Color", Color) = (0,1,0,1)
        _GrassMaxHeight ("Grass Max Height", Float) = 0.0
        _GrassSlopeThreshold ("Grass Slope Threshold", Range(0,1)) = .5
        _GrassBlendDistance ("Grass Blend Distance", Float) = 0.0
        _GrassBlendAmount ("Grass Blend", Range(0,1)) = 0.0
        // Grass noise
        _NoiseBlendAmount ("Noise Blend", Range(0,1)) = 0.0
        _NoiseScale ("Noise Scale", Float) = 0.0
        _NoiseColor ("Noise Color", Color) = (0,1,0,1)
        _NoiseChannel ("Noise Channel", Range(0,3)) = 0.0
        _NoiseTex ("Noise Map", 2D) = "white" {}
        // Rock
        _RockColor ("Rock Color", Color) = (1,1,1,1)
        _RockBlendAmount ("Rock Blend", Range(0,1)) = 0.0
        // Snow
        _SnowColor ("Snow Color", Color) = (1,1,1,1)
        _SnowMinHeight ("Snow Min Height", Float) = 0.0
        _SnowSlopeThreshold ("Snow Slope Threshold", Range(0,1)) = .5
        _SnowBlendDistance ("Snow Blend Distance", Float) = 0.0
        _SnowBlendAmount ("Snow Blend", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        #include "./Includes/Triplanar.cginc"
        #include "./Includes/Math.cginc"

        struct Input {
            float3 worldPos;
            float3 worldNormal;
        };

        // Water
        fixed4 _WaterColor;
        half _WaterMaxHeight;
        // Sand
        fixed4 _SandColor;
        half _SandMaxHeight;
        half _SandBlendAmount;
        // Grass
        fixed4 _GrassColor;
        half _GrassMaxHeight;
        half _GrassSlopeThreshold;
        half _GrassBlendDistance;
        half _GrassBlendAmount;
        // Grass noise
        half _NoiseBlendAmount;
        half _NoiseScale;
        fixed4 _NoiseColor;
        int _NoiseChannel;
        sampler2D _NoiseTex;
        // Rock
        fixed4 _RockColor;
        half _RockBlendAmount;
        // Snow
        fixed4 _SnowColor;
        half _SnowMinHeight;
        half _SnowSlopeThreshold;
        half _SnowBlendDistance;
        half _SnowBlendAmount;

        void surf (Input IN, inout SurfaceOutputStandard o) {
            float height = IN.worldPos.y;
            float slope = 1 - IN.worldNormal.y; // slope = 0 when terrain is completely flat

            // Water
            if (height < _WaterMaxHeight) {
                o.Albedo = _WaterColor;
            }
            else {
                if (height < _SandMaxHeight) {
                    // Sand
                    float sandBlendHeight = _SandMaxHeight * (1 - _SandBlendAmount);
                    float sandWeight = saturate((height - sandBlendHeight) / (_SandMaxHeight - sandBlendHeight));
                    o.Albedo = lerp(_WaterColor, _SandColor, sandWeight);
                }
                else {
                    if (height < _GrassMaxHeight) {
                        // Grass

                        float noise = triplanar(IN.worldPos, IN.worldNormal, _NoiseScale, _NoiseTex)[_NoiseChannel];
                        fixed4 grassColor = lerp(_GrassColor, _NoiseColor, noise * _NoiseBlendAmount);

                        // Blend between rock and grass acording to slope
                        float slopeBlendHeight = _GrassSlopeThreshold * (1 - _GrassBlendAmount);
                        float slopeWeight = saturate((slope - slopeBlendHeight) / (_GrassSlopeThreshold - slopeBlendHeight));
                        grassColor = lerp(grassColor, _RockColor, slopeWeight);

                        if (height < _SandMaxHeight + _GrassBlendDistance) {
                            float grassBlendHeight = _SandMaxHeight + _GrassBlendDistance * (1 - _GrassBlendAmount);
                            float grassWeight = saturate((height - grassBlendHeight) / (_SandMaxHeight - grassBlendHeight));
                            o.Albedo = lerp(grassColor, _SandColor, grassWeight);
                        }
                        else if (height > _GrassMaxHeight - _GrassBlendDistance) {
                            float grassBlendHeight = _GrassMaxHeight - _GrassBlendDistance * (1 - _GrassBlendAmount);
                            float grassWeight = saturate((height - grassBlendHeight) / (_GrassMaxHeight - grassBlendHeight));
                            o.Albedo = lerp(grassColor, _RockColor, grassWeight);
                        }
                        else {
                            o.Albedo = grassColor;
                        }
                    }
                    else {
                        // Rock
                        o.Albedo = _RockColor;

                        if (height > _SnowMinHeight) {
                            // Snow
                            // Slope weight
                            float slopeBlendHeight = _SnowSlopeThreshold * (1 - _SnowBlendAmount);
                            float slopeWeight = 1 - saturate((slope - slopeBlendHeight) / (_SnowSlopeThreshold - slopeBlendHeight));
                            
                            // Height weight
                            float snowBlendHeight = _SnowMinHeight - _SnowBlendDistance * (1 - _SnowBlendAmount);
                            float snowWeight = saturate((height - snowBlendHeight) / (_SnowMinHeight + _SnowBlendDistance - snowBlendHeight));
                            o.Albedo = lerp(o.Albedo, _SnowColor, slopeWeight * snowWeight);
                        }
                    }
                }
            }
        }
        ENDCG
    }
}
