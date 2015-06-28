#version 120
in vec3 vpos;
in vec2 vtexcoord;
in vec4 vertcolor;
in vec3 vnormal;

uniform mat4 mvp;
uniform mat4 mv;

varying vec2 texCoord;
varying vec4 vertColor;
varying vec3 normal0;

void main() {
	gl_Position = mvp * vec4(vpos, 1.0);
	texCoord = vtexcoord;
	vertColor = vertcolor;
	normal0 = (mv * vec4(vnormal, 0.0)).xyz;
}