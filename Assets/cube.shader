#define:
    #version 410

vertex:
    layout(location = 0) in vec3 iPosition;
    layout(location = 1) in vec2 iTexCoords;
    layout(location = 2) in vec3 iNormals;

    out vec2 oTexCoords;

    uniform mat4 uTransform;
    uniform mat4 uView;
    uniform mat4 uProjection;

    void main(){
        oTexCoords = iTexCoords;

        gl_Position = uTransform * uView * uProjection * vec4(iPosition, 1.0);
        //  * uTransform * uView * uProjection
    }

fragment:
    in vec2 oTexCoords;

    out vec4 fragColor;

    uniform sampler2D texture0;

    void main(){
        //fragColor = texture2D(texture0, oTexCoords);
        fragColor = vec4(1, 1, 1, 1);
    }
