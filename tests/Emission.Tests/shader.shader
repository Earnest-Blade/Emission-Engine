define:
    #version 410

vertex:
    layout(location = 0) in vec3 iPosition;
    layout(location = 1) in vec3 iNormals;
    layout(location = 2) in vec2 iTexCoords;
    
    uniform mat4 uTransform;
    uniform mat4 uView;
    uniform mat4 uProjection:

    void main(){

        gl_Position = vec4(iPosition, 1.0);
    }

    
fragment:
    
    out vec4 fragColor;

    void main(){
        fragColor = vec4(1.0, 1.0, 1.0, 1.0);
    }