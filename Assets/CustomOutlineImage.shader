//UI描边效果
Shader "Custom/OutlineImage" {
	Properties{
	_MainTex("MainTex",2D) = "white"{}
	_Color("MainColor",Color) = (1,1,1,1)
	_OutlineColor("OutlineColor",Color) = (1,1,1,1)
		//宽度
		_OutlineFactor("Outline",Range(0,100)) = 5
	}

		CGINCLUDE
#include "UnityCG.cginc"
		sampler2D _MainTex;
	float _OutlineFactor;
	float4 _OutlineColor;
	float4 _Color;

	struct v2f {
		float4 pos:SV_POSITION;
		float2 uv:TEXCOORD0;
	};
	//描边片元函数
	float4 frag_outline(v2f i) :SV_Target{
		float a = tex2D(_MainTex,i.uv).a;
		return float4(_OutlineColor.rgb,a);
	}
		//原图片片元函数
		float4 frag_ori(v2f i) : SV_Target{
			return tex2D(_MainTex,i.uv) * _Color;
	}
		//原图片顶点函数
		v2f vert_ori(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	//描边顶点函数1
	v2f vert_0(appdata_img v) {
		v2f o;
		v.vertex.x -= _OutlineFactor;
		v.vertex.y += _OutlineFactor;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	//描边顶点函数2
	v2f vert_2(appdata_img v) {
		v2f o;
		v.vertex.x += _OutlineFactor;
		v.vertex.y += _OutlineFactor;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	//描边顶点函数3
	v2f vert_6(appdata_img v) {
		v2f o;
		v.vertex.x -= _OutlineFactor;
		v.vertex.y -= _OutlineFactor;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	//描边顶点函数4
	v2f vert_4(appdata_img v) {
		v2f o;
		v.vertex.x += _OutlineFactor;
		v.vertex.y -= _OutlineFactor;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	ENDCG

		SubShader{

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest Off
		Fog{ Mode Off }
		CGPROGRAM
		#pragma vertex vert_0
		#pragma fragment frag_outline       
		ENDCG
		}

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest Off
		Fog{ Mode Off }
		CGPROGRAM
		#pragma vertex vert_2
		#pragma fragment frag_outline       
		ENDCG
		}

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest Off
		Fog{ Mode Off }
		CGPROGRAM
		#pragma vertex vert_4
		#pragma fragment frag_outline       
		ENDCG
		}

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest Off
		Fog{ Mode Off }
		CGPROGRAM
		#pragma vertex vert_6
		#pragma fragment frag_outline       
		ENDCG
		}

		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		ZTest Off
		Fog{ Mode Off }
		CGPROGRAM
		#pragma vertex vert_ori
		#pragma fragment frag_ori       
		ENDCG
		}
	}
		FallBack "Diffuse"
}

