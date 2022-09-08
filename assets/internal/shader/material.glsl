define:
    struct Material 
    {
	    float ambient;
	    float diffuse;
	    float specular;
	    float shininess;
        sampler2D diffuseMap;
        sampler2D specularMap;
    };

fragment:
    // material