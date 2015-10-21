Shader "Custom/WireWallShader"
 {
  Properties
  {
    _LineColor ("Line Color", Color) = (1,1,1,1)
    _GridColor ("Grid Color", Color) = (1,1,1,0)
    _LineWidth ("Line Width", float) = 0.2
  }
  
  SubShader
  {
    Pass
    {
      Tags { "RenderType" = "Opaque" }
      //Blend SrcAlpha OneMinusSrcAlpha
      //AlphaTest Greater 0.5
 
 
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"
 
      uniform float4 _LineColor;
      uniform float4 _GridColor;
      uniform float _LineWidth;
 
      // vertex input: position, uv1, uv2
      struct appdata
      {
        float4 vertex : POSITION;
        float4 texcoord1 : TEXCOORD0;
        float4 color : COLOR;
        float3 normal : NORMAL;
      };
 
      struct v2f
      {
        float4 pos : POSITION;
        float4 texcoord1 : TEXCOORD0;
        float4 color : COLOR;
      };
 
      v2f vert (appdata v)
      { 
        v2f o;

        o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
        
        //float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
        //float2 offset = TransformViewToProjection(norm.xy);
 
        //o.pos.xy += offset * o.pos.z * _LineWidth;
        
        //o.texcoord1 = mul(UNITY_MATRIX_MVP*_LineWidth, v.texcoord1);
        o.texcoord1 = v.texcoord1;
        o.color = v.color;
        return o;
      }
 
      fixed4 frag(v2f i) : COLOR
      {
        fixed4 o;
 		float xscale = 1;
 		float yscale = 1;
        float lx = step(_LineWidth, i.texcoord1.x*xscale);
        float ly = step(_LineWidth, i.texcoord1.y*yscale);
        float hx = step(i.texcoord1.x, 1.0 - _LineWidth/xscale);
        float hy = step(i.texcoord1.y, 1.0 - _LineWidth/yscale);
 
        o = lerp(_LineColor, _GridColor, lx*ly*hx*hy);
 
 		
 
        return o;
      }
      ENDCG
     }
  } 
  Fallback "Vertex Colored", 1
 }