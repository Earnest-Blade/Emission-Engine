define:
    #version 410

vertex:
    layout(location = 0) in vec4 iVertex;

    out vec2 oTexCoords;

    uniform mat4 uProjection;

    void main(){
        
        gl_Position = vec4(iVertex.xy, 0.0, 1.0);
        oTexCoords = iVertex.zw;
    }

fragment:
    in vec2 oTexCoords;

    out vec4 fragColor;

    uniform sampler2D text;
    uniform vec3 color;

    void main(){
        vec4 sampled = vec4(1.0, 1.0, 1.0, texture(text, oTexCoords).r);
        //fragColor = vec4(color, 1.0) * sampled;
        fragColor = vec4(1, 1, 1, 1);
    }
