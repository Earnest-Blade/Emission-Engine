vertex:
	#version 410
	
	layout(location = 0) in vec3 iPosition;
	layout(location = 1) in vec2 iTextCoords;
	
	out vec2 oTextCoords;
	out float oAlpha;
	
	uniform mat4 uProjection;
	uniform mat4 uView;
	uniform mat4 uTransform;
	
	uniform float alpha;
	
	void main(void)
	{
		oTextCoords = iTextCoords;
		oAlpha = alpha;
		
		gl_Position = vec4(iPosition, 1.0) * uTransform * uView * uProjection;
	}

fragment:
	#version 410

	in vec2 oTextCoords;
	in float oAlpha;

	out vec4 oFrag;

	uniform sampler2D uSampler;

	void main(void)
	{
		vec4 color = texture2D(uSampler, oTextCoords);
		oFrag = color;
	}

