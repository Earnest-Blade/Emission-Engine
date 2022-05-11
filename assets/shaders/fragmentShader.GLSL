#version 410

layout (location=0) in vec2 TextCoords;

out vec4 FragColor;

uniform sampler2D sampler;

void main(void){
	vec4 color = texture2D(sampler, TextCoords);
	FragColor = color;
	
}