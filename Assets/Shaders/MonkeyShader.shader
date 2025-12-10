// This is a simple example of custom shader

// The shader takes the color and animates its Red component so that
// it starts as 0.0 at the start of each second and rises to 1.0 towards
// the end of each second

// Here, our shader is written using HLSL (High-Level Shader Language)

Shader "Unlit/MonkeyShader"   // Name of our shader
{
    
    // Here we define the uniforms that can be edited in the Inspector panel inside Unity
    Properties {
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
    
    SubShader {
        // The material is non-transparent and is rendered at the same time as other geometry
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        
        Pass {
            CGPROGRAM  // Marks the beginning of our HLSL shader
            #include "UnityCG.cginc"
            #pragma vertex vert  // Our vertex shader entry function is called `vert`
            #pragma fragment frag  // Our fragment shader entry function is called `frag`


            // The input/output structures for our shaders
            struct VertexInput {
                float4 vertex: POSITION;
            };
            struct VertexOutput {
                float4 position: SV_POSITION;
            };


            fixed4 _Color;  // The _Color uniform that we defined above in Properties


            // Vertex shader
            VertexOutput vert(VertexInput input) {
                VertexOutput output;
                output.position = UnityObjectToClipPos(input.vertex); // Just a usual PVM transformation
                return output;
            }

            // Fragment shader
            fixed4 frag(VertexOutput input): SV_Target {
                // The uniform _Time holds duration of the application in seconds,
                // and is exposed by default by Unity as a 4D vector:
                //  * 1st component = time / 20
                //  * 2nd component = time
                //  * 3rd component = time^2
                //  * 4th component = time^3
                float t = _Time.y;
                float timeSubSecond = t - floor(t);  // Get only decimal part of the time (milliseconds)

                fixed4 color = _Color;
                color.r = color.r * timeSubSecond;

                return color;
            }

            ENDCG  // Marks the end of our HLSL shader
        }
    }
}
