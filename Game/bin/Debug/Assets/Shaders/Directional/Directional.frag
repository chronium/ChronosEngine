#version 330

in vec2 texCoord;
in vec4 vertColor;
in vec3 normal0;
in vec3 worldPos0;

uniform sampler2D diffuse;
uniform vec3 eyePos;

struct BaseMaterial {
	vec4 ambient;
	vec4 specularIntensity;
	float specularPower;
};

uniform BaseMaterial material;

struct BaseLight {
	vec3 color;
	float intensity;	
};

struct DirectionalLight {
	BaseLight base;
	vec3 direction;
};

uniform DirectionalLight directionalLight;

vec4 calcLight(BaseLight base, vec3 direction, vec3 normal)
{
    float diffuseFactor = dot(normal, -direction);
    
    vec4 diffuseColor = vec4(0,0,0,0);
    vec4 specularColor = vec4(0,0,0,0);
    
    if(diffuseFactor > 0)
    {
        diffuseColor = vec4(base.color, 1.0) * base.intensity * diffuseFactor;
        
        vec3 directionToEye = normalize(eyePos - worldPos0);
        vec3 reflectDirection = normalize(reflect(direction, normal));
        
        float specularFactor = dot(directionToEye, reflectDirection);
        specularFactor = pow(specularFactor, material.specularPower);
        
        if(specularFactor > 0)
        {
            specularColor = vec4(base.color, 1.0) * material.specularIntensity * specularFactor * (1.0 / length(worldPos0 - eyePos));
        }
    }
    
    return diffuseColor + specularColor;
}

vec4 calcDirectionalLight(DirectionalLight directionalLight, vec3 normal)
{
    return calcLight(directionalLight.base, -directionalLight.direction, normal);
}

out vec4 fragColor;

void main() {
	fragColor = texture2D(diffuse, texCoord) * calcDirectionalLight(directionalLight, normalize(normal0));
}