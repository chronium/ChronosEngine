#version 120
varying vec2 texCoord;
varying vec4 vertColor;
varying vec3 normal0;

uniform sampler2D diffuse;

void main() {
	gl_FragColor = texture2D(diffuse, texCoord) * vertColor *
		clamp(dot(-vec3(0, 0, 1), normal0), 0.0, 1.0);
}