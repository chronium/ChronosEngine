#version 330
layout (location = 0) in vec3 vpos;
layout (location = 1) in vec2 vtexcoord;
layout (location = 2) in vec4 vertcolor;
layout (location = 3) in vec3 vnormal;

uniform mat4 MVP;
uniform mat4 model;

out vec2 texCoord;
out vec4 vertColor;
out vec3 normal0;
out vec3 worldPos0;

void main() {
	gl_Position = MVP * vec4(vpos, 1.0);
	texCoord = vtexcoord;
	vertColor = vertcolor;
	normal0 = (model * vec4(vnormal, 0.0)).xyz;
	worldPos0 = (model * vec4(vpos, 1.0)).xyz;
}