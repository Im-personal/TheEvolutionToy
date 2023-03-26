#version 330 core

out vec4 outputColor;
in vec4 vertexColor;

uniform vec4 color;

void main()
{
    outputColor = color ;
}