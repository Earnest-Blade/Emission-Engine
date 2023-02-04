define:
    #version 410

vertex:
    layout(location = 0) in vec3 iPosition;
    layout(location = 1) in vec3 iNormals;
    layout(location = 2) in vec2 iTexCoords;

    out vec2 oTexCoords;
    out vec3 oNormals;
    out vec3 oFragPosition;

    uniform mat4 uTransform;
    uniform mat4 uView;
    uniform mat4 uProjection;

    void main(){
        oTexCoords = iTexCoords;
        oFragPosition = vec3(uTransform * vec4(iPosition, 1.0));
        oNormals = mat3(transpose(inverse(uTransform))) * iNormals;

        gl_Position = uProjection * uView * uTransform * vec4(iPosition, 1.0);

        mat3 normalMat3 = mat3(transpose(inverse(uView * uTransform)));
    }

fragment:
    in vec2 oTexCoords;
    in vec3 oNormals;
    in vec3 oFragPosition;

    out vec4 fragColor;

    uniform sampler2D texture0;

    uniform vec3 lightPosition;
    uniform vec3 lightDirection;

    void main(){
        vec4 sampled = texture2D(texture0, oTexCoords);

        /* Variables */
        vec4 color = vec4(1, 1, 1, 1);
        vec4 ambientColor = vec4(0.5, 0.5, 0.5, 1);
        vec4 specularColor = vec4(1, 1, 1, 1);

        float glossiness = 32;
        float diffuseStrength = 0.5;
        float specularStrength = 0.5;

        vec4 rimColor = vec4(1, 0, 1, 1);
        float rimAmount = 0.7;

        /* Lighting */
        vec3 norm = normalize(oNormals);
        vec3 lightDirection = normalize(lightPosition - oFragPosition);
        vec3 viewDirection = normalize(lightDirection - oFragPosition);
        vec3 reflectDirection = reflect(-lightDirection, norm);

        float diffusion = max(dot(norm, lightDirection), 0.0);
        vec4 diffuse = diffuseStrength * diffusion * color;

        float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), glossiness);
        vec4 specular = specularStrength * spec * specularColor;

        //fragColor = texture2D(texture0, oTexCoords);
        fragColor = color * sampled * (ambientColor + diffuse + specular);
    }