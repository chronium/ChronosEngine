#version 120
in vec3 vpos;
in vec2 vtexcoord;

uniform mat4 mvp;

varying vec2 texCoord;

void main() {
	gl_Position = mvp * vec4(vpos, 1.0);
	texCoord = vtexcoord;
}