define:
	#version 440

vertex:
	layout(location = 0) in vec3 iPosition;
	layout(location = 2) in vec2 iTextCoords;

	uniform mat4 uProjection;
	uniform mat4 uView;
	uniform mat4 uTransform;

	out vec2 oTextCoords;

	void main(void){
		oTextCoords = iTextCoords;

		gl_Position = uProjection * uView * uTransform * vec4(iPosition, 1.0);
	}

fragment:
	in vec2 oTextCoords;

	uniform sampler2D texture0;

	out vec4 oFrag;

	void main(void){
		oFrag = texture(texture0, oTextCoords);
	}