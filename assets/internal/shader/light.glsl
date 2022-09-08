define:
    struct DirectionalLight
    {
        vec3 position;
        vec3 rotation;
        vec3 color;
        float strength;

    };
    
    struct PointLight
    {
        vec3 position;
        vec3 rotation;
        vec3 color;
        float strength;
        
        //float distance;
        float radius;
    };
    
    struct SpotLight
    {
        vec3 position;
        vec3 rotation;
        vec3 color;
        float strength;
        
        float cutoff;
    };

fragment:
    // import code from camera shader
	#include assets/internal/shader/camera.glsl
	// end of imported code from camera shader

    // import code from material shader
	#include assets/internal/shader/material.glsl
	// end of imported code from material shader

    vec3 calcDirectionalLight(Material mat, DirectionalLight light, vec2 texCoords, vec3 normals, vec3 fragPosition)
    {
        vec3 norm = normalize(normals);
        vec3 lightDirection = normalize(-light.rotation);
        vec3 viewDirection = normalize(camera.position - fragPosition);
        vec3 reflectDirection = reflect(-lightDirection, norm);

        vec3 ambient = mat.ambient * light.color * vec3(texture2D(material.diffuseMap, texCoords));
        
        float diff = max(dot(norm, lightDirection), 0.0);
        vec3 diffuse = mat.diffuse * diff * light.color * light.strength * vec3(texture2D(material.diffuseMap, texCoords));

        float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), mat.shininess);
        vec3 specular = material.specular * spec * light.color * vec3(texture2D(material.specularMap, texCoords));

        return ambient + diffuse + specular;
    }

    vec3 calcPointLight(Material mat, PointLight light, vec2 texCoords, vec3 normals, vec3 fragPosition)
    {
        vec3 norm = normalize(normals);
        vec3 lightDirection = normalize(light.position - fragPosition);
        vec3 viewDirection = normalize(camera.position - fragPosition);
        vec3 reflectDirection = reflect(-lightDirection, norm);

        vec3 ambient = mat.ambient * light.color * vec3(texture2D(material.diffuseMap, texCoords));
        
        float diff = max(dot(norm, lightDirection), 0.0);
        vec3 diffuse = mat.diffuse * diff * light.color * light.strength * vec3(texture2D(material.diffuseMap, texCoords));

        float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), mat.shininess);
        vec3 specular = material.specular * spec * light.color * vec3(texture2D(material.specularMap, texCoords));

        float distance = distance(fragPosition, light.position);
        float attenuation = clamp(1.0 - sqrt(distance) / sqrt(light.radius), 0.0, 1.0);

        return attenuation * (ambient + diffuse + specular);
    }

    vec3 calcSpotLight(Material mat, SpotLight light, vec2 texCoords, vec3 normals, vec3 fragPosition)
    {
        vec3 norm = normalize(normals);
        vec3 lightDirection = normalize(light.position - fragPosition);
        vec3 viewDirection = normalize(camera.position - fragPosition);
        vec3 reflectDirection = reflect(-lightDirection, norm);

        vec3 ambient = mat.ambient * light.color * vec3(texture2D(material.diffuseMap, texCoords));

        float theta = dot(lightDirection, normalize(-light.rotation));
        
        if(theta > light.cutoff)
        {
            float diff = max(dot(norm, lightDirection), 0.0);
            vec3 diffuse = mat.diffuse * diff * light.color * light.strength * vec3(texture2D(material.diffuseMap, texCoords));

            float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), mat.shininess);
            vec3 specular = material.specular * spec * light.color * vec3(texture2D(material.specularMap, texCoords));

            return ambient + diffuse + specular;
        }
        else
        {
            return ambient;
        }
    }
    
    