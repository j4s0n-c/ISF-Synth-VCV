// SaturdayShader Week 21 : Cosplay
// by Joseph Fiola (http://www.joefiola.com)
// 2016-01-09
//modified 2022 Jac Dedoy
/*{
  "CREDIT": "Joseph Fiola",
  "DESCRIPTION": "",
  "CATEGORIES": [
    "Generator"
  ],
  "INPUTS": [
    {
      "NAME": "dotSize",
      "TYPE": "float",
      "DEFAULT": 0.01,
      "MIN": 0,
      "MAX": 0.1
    },
    {
      "NAME": "iteration",
      "TYPE": "float",
      "DEFAULT": 100,
      "MIN": 0,
      "MAX": 100
    },
    {
      "NAME": "xAmp",
      "TYPE": "float",
      "DEFAULT": 0.3,
      "MIN": -1,
      "MAX": 1
    },
    {
      "NAME": "yAmp",
      "TYPE": "float",
      "DEFAULT": 0.1,
      "MIN": -1,
      "MAX": 1
    },
    {
      "NAME": "xFactor",
      "TYPE": "float",
      "DEFAULT": 0.2,
      "MIN": 0,
      "MAX": 10
    },
    {
      "NAME": "yFactor",
      "TYPE": "float",
      "DEFAULT": 0.2,
      "MIN": 0,
      "MAX": 10
    },
    {
      "NAME": "speed",
      "TYPE": "float",
      "DEFAULT": 0.025,
      "MIN": 0,
      "MAX": 0.1
    },
    {
      "NAME": "rotateCanvas",
      "TYPE": "float",
      "DEFAULT": 1.59,
      "MIN": -3.141592653589793,
      "MAX": 3.141592653589793
    },
    {
      "NAME": "rotateParticles",
      "TYPE": "float",
      "DEFAULT": 0,
      "MIN": -1.5707963267948966,
      "MAX": 1.5707963267948966
    },
    {
      "NAME": "rotateMultiplier",
      "TYPE": "float",
      "DEFAULT": 10,
      "MIN": 0.01,
      "MAX": 10
    },
    {
      "NAME": "pos",
      "TYPE": "point2D",
      "DEFAULT": [
        0.5,
        0.5
      ],
      "MIN": [
        0,
        0
      ],
      "MAX": [
        1,
        1
      ]
    }
  ]
}*/


//rotation
vec2 rot(vec2 uv,float a){
	return vec2(
		uv.x * cos(a) - uv.y * sin(a),
		uv.y * cos(a) + uv.x * sin(a)
	);
}

float circle(vec2 uv, float size){
	return  length(uv) > size?0.0:1.0;
}


void main(){

	vec2 uv = gl_FragCoord.xy/RENDERSIZE;
	uv -= vec2(pos);						
	uv.x *= RENDERSIZE.x/RENDERSIZE.y;
	
	vec3 color = vec3(0);
	
	//rotate canvas
	uv=rot(uv,rotateCanvas);
		
	
	float i = 2.0;	
		
	for (int _i = 0; _i<50; _i++) { // for loop fix on Intel - and possible others -  by Imimot @imimothq
		
		i = float(_i);
		
		// set max number of iterations
		if (iteration < i) break;
		
		// sin() cos() animation
		vec2 st = uv - vec2(cos(23.*i * xFactor * (TIME*speed/2.)) * xAmp, tan(i * yFactor * (TIME*speed)) * yAmp);
		
		// set dotSize
		float dots = circle((st), dotSize * (i  * 0.015));
		
		//rotate particles
		uv=rot(uv,rotateParticles*rotateMultiplier);

		color += dots;
		}

	gl_FragColor = vec4(vec3(color.r,0.0,0.0),1.0);
}
