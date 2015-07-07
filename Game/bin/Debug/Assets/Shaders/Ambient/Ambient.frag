#version 330
in vec2 texCoord;
in vec4 vertColor;

uniform sampler2D diffuse;
uniform vec4 ambient;

struct BaseMaterial {
	vec4 ambient;
	vec4 specularIntensity;
	float specularPower;
};

uniform BaseMaterial material;

out vec4 fragColor;

void main() {
	fragColor = texture2D(diffuse, texCoord) 
	* vertColor * material.ambient;
}