#version 120
in vec3 vpos;
in vec2 vtexcoord;
in vec4 vertcolor;
in vec3 vnormal;

uniform mat4 MVP;
uniform mat4 model;

varying vec2 texCoord;
varying vec4 vertColor;
varying vec3 normal0;
varying vec3 worldPos0;

void main() {
	gl_Position = MVP * vec4(vpos, 1.0);
	texCoord = vtexcoord;
	vertColor = vertcolor;
	normal0 = (model * vec4(vnormal, 0.0)).xyz;
	worldPos0 = (model * vec4(vpos, 1.0)).xyz;
}