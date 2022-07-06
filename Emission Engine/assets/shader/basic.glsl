vertex:
	#version 410
	
	layout(location = 0) in vec3 iPosition;
	layout(location = 1) in vec2 iTextCoords;
	layout(location = 2) in vec3 iNormals;

	out vec2 oTextCoords;
	out vec3 oNormals;
	out vec3 oFragPos;
	out float oAlpha;
	
	uniform mat4 uProjection;
	uniform mat4 uView;
	uniform mat4 uTransform;
	uniform mat4 uTransformizedTransform;
	
	uniform float alpha;
	
	void main(void)
	{
	    // Define outputs
		oTextCoords = iTextCoords;
		oAlpha = alpha;
		oNormals = iNormals * mat3(uTransformizedTransform);
		
		// Define position
		oFragPos = vec3(uTransform * vec4(iPosition, 1.0));
		gl_Position = vec4(iPosition, 1.0) * uTransform * uView * uProjection;
	}

fragment:
	#version 410

	in vec2 oTextCoords;
	in vec3 oNormals;
	in vec3 oFragPos;
	in float oAlpha;	

	out vec4 oFrag;

	uniform sampler2D texture0;
	uniform sampler2D texture1;
	
	uniform vec3 uLightColor;
	uniform vec3 uCameraPosition;
	uniform vec3 uLightPosition;
	
	uniform float uAmbient;
	uniform float uDiffuse;
	uniform float uSpecular;
	uniform float uShininess;

    vec3 ambient(void)
    {
        return uAmbient * uLightColor;
    }

    vec3 diffuse(vec3 norm, vec3 lightDir)
    {
        float diff = max(dot(norm, lightDir), 0.0);
        return diff * uLightColor * uDiffuse;
    }

    vec3 specular(vec3 viewDir, vec3 reflectDir)
    {
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), uShininess);
        return uSpecular * spec * uLightColor;
    }

	void main(void)
	{
		vec3 color = vec3(texture2D(texture0, oTextCoords));
		//vec3 color = vec3(1.0, 0.5, 0.31);		
	    //vec3 color = vec3(mix(texture2D(texture0, oTextCoords), texture2D(texture1, oTextCoords), 0.5));
		
		vec3 norm = normalize(oNormals);
		vec3 lightDir = normalize(uLightPosition - oFragPos);
		
		vec3 viewDir = normalize(uCameraPosition - oFragPos);
		vec3 reflectDir = reflect(-lightDir, norm);
		
        vec3 res = (ambient() + diffuse(norm, lightDir) + specular(viewDir, reflectDir)) * color;
		oFrag = vec4(res, oAlpha);
	}

