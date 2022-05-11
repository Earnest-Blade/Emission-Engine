#version 410

layout (location=0) in vec3 position;
layout (location=1) in vec2 textureCoords;

uniform mat4 transformation;
uniform mat4 projection;
uniform float layer;

out vec2 TextCoords;

void main(void){
	gl_Position = transformation * vec4(position.x, position.y, layer, 1.0);
	TextCoords = textureCoords;
}