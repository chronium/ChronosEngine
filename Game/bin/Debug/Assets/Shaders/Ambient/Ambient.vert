#version 120
in vec3 vpos;
in vec2 vtexcoord;
in vec4 vertcolor;

uniform mat4 mvp;

varying vec2 texCoord;
varying vec4 vertColor;

void main() {
	gl_Position = mvp * vec4(vpos, 1.0);
	texCoord = vtexcoord;
	vertColor = vertcolor;
}