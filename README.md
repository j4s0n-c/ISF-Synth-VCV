# ISF-Synth-VCV
ISF based video synthesis/manipulation plugins for VCV Rack

Beta testers: please use the issue tracker and tags to submit bug / enhancement (feature requests) / question (general feedback)

## **Welcome! Here's what we have working so far...** 

### "BASE" modules:

-WAVE (multi-function video oscillator)

-COLOR (bi-polar colorizer)

-BLEND (2 channel mixer with selectable blend mode)

-SCREEN (resizable video texture monitor with window / Spout output)

### "LOADER" modules:

-imageLoader (multi-format image loader with built-in effects)

-isfLoader (can run/control/edit a single ISF generator/transition/effect file)

### "TEXTURE NETWORK" modules:

-textureOut (creates a "wireless" / spout / maybe Syphon, v4l sender)

-textureIn (receives a "wireless" / spout / maybe Syphon, v4l sender)

### "UTILITY" modules:

-audioTexture
  creates a "waveform" of the incoming audio as a black&white texture
  
-texture2RGB
  separates the incoming texture into 
  
-texture2CMY


# Preliminary documentation for currently available modules:

## "BASE" modules:
### WAVE: multi-function video oscillator
![WAVE_2](https://github.com/j4s0n-c/ISF-Synth-VCV/assets/4063528/5af1f0e9-5021-4761-8eae-5ff4d8c0ddcc)

-Blendable Sin, Tri, Ramp, Square shaped horizontal or vertical black to white gradient generator.

-Frequency, Phase, Animation Speed, Amp Level, Bias parameters.

-Built-in mirror and flip effects.

### COLOR: bi-polar colorizer
![WAVE_COLOR_2](https://github.com/j4s0n-c/ISF-Synth-VCV/assets/4063528/85216d44-55eb-40fc-ad83-be70eda1a8d2)

- Bi-polar brightness and contrast of red, green, and blue channels with invert switches on each channel.

### BLEND: 2 channel texture mixer with selectable blend mode
![BLEND_2](https://github.com/j4s0n-c/ISF-Synth-VCV/assets/4063528/399bbd69-3bfc-47c2-b6d0-3fcaa980262d)

- 2 tuxture inputs mixed through one of many (28 so far) blend modes
  
- blend mode and crossfader are cv controllable.
  
- already have a workig ISF for a "Pendulum" like "video rate" crossfader with a texture input for the "MIX" CV

  
### All parameters can be manipulated with panel UI controls and CV inputs, up to audio rate, and the ones that make sense are bi-polar. Some parameters can be upgraded to video capable inputs later, where it makes sense... let us know.
