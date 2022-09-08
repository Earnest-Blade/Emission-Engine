define:
    #version 410
    
vertex:
	layout(location = 0) in vec3 iPosition;
	layout(location = 1) in vec2 iTextCoords;
	layout(location = 2) in vec3 iNormals;

	out vec2 oTextCoords;
	out vec3 oNormals;
	out vec3 oFragPosition;
	out float oAlpha;
	
	uniform mat4 uProjection;
	uniform mat4 uView;
	uniform mat4 uTransform;
	
	uniform float alpha;
	
	void main(void)
	{
	    // Define outputs
		oTextCoords = iTextCoords;
		oAlpha = alpha;
		oNormals = iNormals * mat3(transpose(inverse(uTransform)));
		
		// Define position
		oFragPosition = vec3(uTransform * vec4(iPosition, 1.0));
		gl_Position = vec4(iPosition, 1.0) * uTransform * uView * uProjection;
	}

fragment:
	in vec2 oTextCoords;
	in vec3 oNormals;
	in vec3 oFragPosition;
	in float oAlpha;	

	out vec4 oFrag;

	uniform sampler2D texture0;

	uniform Material material;
	uniform PointLight light;

	// import code from light shader
	#include assets/internal/shader/light.glsl
	// end of impoted code from light shader

	void main(void)
	{
		vec3 color = vec3(texture2D(texture0, oTextCoords));
		//vec3 color = vec3(1.0, 0.5, 0.31);		
		
		oFrag = vec4(color, 1);
		//oFrag = vec4(calcPointLight(material, light, oTextCoords, oNormals, oFragPosition) * color, 1);
	}
    

