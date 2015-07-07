#version 330
layout (location = 0) in vec3 vpos;
layout (location = 1) in vec2 vtexcoord;
layout (location = 2) in vec4 vertcolor;

uniform mat4 mvp;

out vec2 texCoord;
out vec4 vertColor;

void main() {
	gl_Position = mvp * vec4(vpos, 1.0);
	texCoord = vtexcoord;
	vertColor = vertcolor;
}