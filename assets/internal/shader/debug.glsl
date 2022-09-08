vertex:
    #version 410

	layout(location = 0) in vec3 iPosition;
		
	uniform mat4 uProjection;
	uniform mat4 uView;
	uniform mat4 uTransform;
	
	void main(void)
	{
		gl_Position = vec4(iPosition, 1.0) * uTransform * uView * uProjection ;
	}

fragment:
    #version 410

	out vec4 oFrag;
	
	uniform vec3 uColor;

	void main(void)
	{
		oFrag = vec4(uColor, 1.0);
	}

