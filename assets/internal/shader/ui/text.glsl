define:
    #version 410

vertex:
    layout(location = 0) in vec4 iVertex;

    out vec2 oTextCoords;
    
    uniform mat4 uProjection;

    void main(void)
    {
        gl_Position = vec4(iVertex.xy, 0.0, 1.0) * uProjection;
        oTextCoords = iVertex.zw;
    }


fragment:
    in vec2 oTextCoords;

    out vec4 oFrag;

    uniform sampler2D uText;
    uniform vec3 uTextColor;

    void main(void){
        vec4 sampled = vec4(1.0, 1.0, 1.0, texture2D(uText, oTextCoords).r);
        oFrag = vec4(1.0, 1.0, 1.0, 1.0);
    }
