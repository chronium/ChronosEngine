#version 330
 
layout (location = 0) in vec3 vpos;
layout (location = 1) in vec3 vcolor;

out vec4 color;
uniform mat4 modelview;
 
void main() {
	gl_Position = modelview * vec4(vpos, 1.0);
	color = vec4(vcolor, 1.0);
}