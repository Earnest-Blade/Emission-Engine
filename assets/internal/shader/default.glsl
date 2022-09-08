define:
    #version 410

vertex:
    layout(location = 0) in vec3 iPosition;
    layout(location = 1) in vec2 iTextCoords;

    out vec2 oTextCoords;

    uniform mat4 uProjection;
	uniform mat4 uView;
	uniform mat4 uTransform;

    void main(void)
    {
        oTextCoords = iTextCoords;
		
		// Define position
		gl_Position = vec4(iPosition, 1.0) * uTransform * uView * uProjection;
    }


fragment:
    in vec2 oTextCoords;

    out vec4 oFrag;

    uniform sampler2D texture0;

    void main(void){
        vec3 color = vec3(texture2D(texture0, oTextCoords));
        
        oFrag = vec4(color, 1);
    }
