#version 330 core

layout(location = 0) in vec3 aPosition; 

out vec4 vertexColor;

uniform vec4 move;
uniform vec2 mult;

void main(void)
{
	vec3 tran = aPosition;
	tran[0]*=move[2];
	tran[1]*=move[3];
	tran[0]+=move[0];
	tran[1]+=move[1];
	
	tran[0]*=mult[0];
	tran[1]*=mult[1];

    gl_Position = vec4(tran, 1.0);
	
	vertexColor = vec4(1, 1, 1, 1.0);
}