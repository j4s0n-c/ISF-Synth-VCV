# trowaSoft ISF-Synth-VCV
ISF based video synthesis/manipulation plugin for VCV Rack.

## Plugin Closed Beta

Beta testing is currently by invite only. **Please** do not share the binaries without prior permission.

We will not post any builds here for download. Binaries will be sent directly by email.

**Invited Beta testers:** Please use the [issue tracker](https://github.com/j4s0n-c/ISF-Synth-VCV/issues) and tags 
to submit bug / enhancement (feature requests) / question (general feedback).

## NOTES
+ These modules will **not run in VST mode** as they are intended to be constantly generating textures and thus need a valid OpenGL context to run (they need access to the graphics card).
+ It is recommended that you have a decent video card for these modules (actually even processor and RAM). 
+ The modules transport textures to each other via a 'Texture CV' port which looks like a yellow composite RCA jack.
+ This documentation and plugin is still a work in progress. Not everything may be documented here. The names of modules and features may change.
  Also, there are some other modules that are still in development that aren't in this test build--this may be more like an alpha.
+ To install the plugin: Copy the relevant ".vcvplugin" file for your hardware to your plugins folder. Then open Rack.

	-Windows: "C:\Users\\**your_name**\\AppData\Local\Rack2\plugins-win-x64"

	-Apple: "/Users/**your_name**/Library/Application Support/Rack2/plugins-mac-**your_cpu**/"

## Module List
The current pack includes:

**Texture Loading**:
+ [isfLoader](#isfLoader) - Load your own Interactive Shader Format (ISF) files to generate textures in Rack.
+ [imageLoader](#imageLoader) - Load an image (png, jpg, gif) to texture.

**Texture Viewing and Transmitting**:
+ [textureMonitor](#textureMonitor) - Display for generated/loaded textures. Can receive the texture "wirelessly" from a [texOut](#texOut) module or from an external texture/frame sharing program (i.e. via Spout).
+ [texOut](#texOut) - "Transmit" a texture wirelessly to the a [textureMonitor](#textureMonitor) or to an external texture/frame sharing program (i.e. via Spout).
+ [texIn](#texIn) - (Currently only Windows) Receive a texture/frame from Spout and make it available as a CV output.

**Generation Modules**: These modules generate textures.
+ [WAVE](#WAVE) - Multi-function video oscillator. Generate shapes similiar to analog shape generators. 
+ [audioToImage & audioToImage2Ch](#audioToImage--audioToImage2Ch) - Generate texture inputs from audio for use in ISFs that need an *audio* or *audioFFT* input.

**Filter Modules**: These modules take in textures and manipulate them to output another texture.
+ [BLEND](#BLEND) - Blend two (2) textures based off various blending modes.
+ [COLOR](#COLOR) - Colorize an input texture.
+ [texRGBY](#texRGBY) - Extract the RGBY color channels of a texture.  
+ [texCMYK](#texCMYK) - Extract the CMYK color channels of a texture.

## Texture Loading
### isfLoader
![isfLoader](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/isfLoader.png?raw=true)
**isfLoader** is a simple ISF file loader for VCV Rack. It reads an ISF file and links CV inputs and parameters (knobs, switches, etc) to the ISF inputs. 
ISF stands for "Interactive Shader Format"; for more information about ISFs, please visit: https://isf.video/.

#### User Controls
+ **Load File** : Load an ISF file.
+ **Reload File** : Reload the current ISF file.
+ **Edit File** : Open the ISF file with the default program on your machine for editing. 
+ **SPD** : For ISFs that use time, the speed control (-200% to +200%). Default is 100%.
+ **RST** : For ISFs that use time, reset the shader's clock to 0.
+ 19 scalar parameters (knobs, switches) that can be mapped to ISF non-image inputs. 
  Some ISF parameter data types are vectors (and take up > 1 parameter):
  1. *color* = 4 scalar parameters (Red, Green, Blue, Alpha)
  2. *position* = 2 scalar parameters (x, y)

  If there are more than 19 scalar parameters channels defined in the ISF, only the first 19 will be loaded.

#### CV Inputs
+ **SPD** : For ISFs that use time, the speed control (-200% to +200%). Default is 100%.
+ **RST** : For ISFs that use time, reset the shader's clock to 0.
+ 3 'Texture CV' Inputs that can be mapped to ISF image inputs.
+ 19 CV Inputs (mono) that can be mapped to ISF non-image inputs.

  If there are more than 19 scalar parameters defined in the ISF, only the first 19 will be loaded.

#### Context Menu
+ **Initialize Parameters** : Reset ISF parameter values *only*. Will be reset to the defaults defined in the ISF file. 
  Retains the currently loaded ISF file.
+ **Load File** : Load an ISF file.
+ **Preview** : Show or hide the preview.

#### CV Outputs
+ **OUT** : (Texture CV) The resulting output texture.

---

### imageLoader
![imageLoader](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/imageLoader.png?raw=true)

**imageLoader** is a simple image loader for VCV Rack. It loads the given image into a texture and then outputs the texture information via CV output.
It supports png, jpg, and gif (including animated). Note that images will be setup on a canvas of the default texture size (currently 1080p).

#### User Controls & CV Inputs
+ **Load File** : (Control only) Load an image file.
+ **Zoom** : Zoom in/out (20% - 500%).
+ **X Offset** : Moves the image on the canvas (x-direction).
+ **Y Offset** : Moves the image on the canvas (y-direction).
+ **Center X** : Centers the image on the canvas (x-direction).
+ **Center Y** : Centers the image on the canvas (y-direction).
+ **Repeat** : Tiling/Repeat mode:
  - None
  - X&Y (Normal)
  - X Only (Normal)
  - Y Only (Normal)
  - X&Y (Mirrored)
  - X Only (Mirrored)
  - Y Only (Mirrored)
+ **Mirror X** : Mirrors the image (x-direction).
+ **Mirror Y** : Mirrors the image (y-direction).
#### CV Outputs
+ **OUT** : (Texture CV) The resulting output texture.

---

## Texture Viewing and Transmitting

### textureMonitor

**textureMonitor** is a monitor for your textures. It can display textures from a 'Texture CV' input, wirelessly from a [texOut](#texOut) or from other programs via texture/frame sharing like Spout (or from other modules that support texture sharing). 
**Currently texture sharing is only supported in Windows (Spout).**

**NOTE:** The CV input will be hidden if you select a wireless mode or shown if you select the wired mode. 
This is technically not very hardware-like, but instead of making two (2) monitors (wired and wireless versions), this has just been combined into one module. Please use your imagination that these are really two separate modules.

#### Controls
+ **Resize Handle** : Change the width by grabbing the right hand side of the module.
	+ Double-clicking will return the module to the default width.
	+ (Context Menu) Select a predefined width to roughly appoximate an aspect ratio.
+ (Context Menu) Select **Input Mode**:
    + **Wired** : Use the CV input to use for the input source. The CV input will be shown in this mode. 
    + **Wireless** : Use a 'wirelessly' broadcasted texture from a [texOut](#texOut) module or a frame share sender (if supported). The CV input will be hidden in this mode.
+ (Context Menu) **External Window**: Display the texture in an external window. 
The external window will allow you to render in full screen mode. 
Keyboard shortcuts for the window include:
    + **F11** : Toggle full-screen.
    + **F** : Toggle full-screen. Currently can't detect Fn key, so just using F will toggle full-screen.
    + **ALT** + **ENTER** : Toggle full-screen. 
    + **ALT** + **F4** : Close window.
    + **ESC** : If in full-screen mode, returns to windowed mode. If in windowed mode, minimizes the window.
+ **NOTE:** If placing the **External Window** on another monitor that is driven by a *different* graphics device other than the same graphics device Rack is using, the window may fail to access the texture.

**WIRELESS Options:**
If the **Input Mode** is set to **Wireless**, then these additional options will appear in the Context Menu:
+ (Context Menu) Select a 'wirelessly' broadcasted texture from a [texOut](#texOut).
+ (Context Menu) Select a frame-share sender as the source. 
    + [Windows] Select a Spout sender. Currently only Windows/Spout is supported.
+ (Context Menu) Rx Scaling:
	+ None : Don't do any scaling. Texture will be rendered as is and may not take up the entire module screen or be cropped.
	+ Fill (Skew) : Fill the module screen with the texture in the x and y directions even if the dimesions are skewed/stretched/squished.
	+ Fill (Width) : Fill to match the width of the module screen. The texture may be cropped in the y-direction or not fill the screen vertically.
	+ Fill (Height) : Fill to match the height of the module screen. The texture may be cropped in the x-direction or not fill the screen horizontally.
	+ Fill (Width & Height) : Fill to match the width and height of the module screen while keeping the aspect ratio. May not fill up the entire canvas in x or y.

**Texture Sharing (External Output) Options:**
Use the context menu to configure the texture sending properites.
+ **Sender Enable/Disable** : Enable or disable the frame sharer sender.
+ **Output Resolution** : Select the resolution of the output texture.
+ **Output Scaling** :
	+ None : Don't do any scaling. Texture will be rendered as is and may not take up the entire module screen or be cropped.
	+ Fill (Skew) : Fill the module screen with the texture in the x and y directions even if the dimesions are skewed/stretched/squished.
	+ Fill (Width) : Fill to match the width of the module screen. The texture may be cropped in the y-direction or not fill the screen vertically.
	+ Fill (Height) : Fill to match the height of the module screen. The texture may be cropped in the x-direction or not fill the screen horizontally.
	+ Fill (Width & Height) : Fill to match the width and height of the module screen while keeping the aspect ratio. May not fill up the entire canvas in x or y.


---

### texOut

**texOut** takes an incoming 'Texture CV' and sends to an external frame sharer like Spout 
or broadcasts it 'wirelessly' internally within Rack so that a [textureMonitor](textureMonitor) may receive it.
Currently, external framesharing is only supported in Spout (Windows). 

#### User Controls
**Screen** : Click on the screen to set the sender name. Defaults to 'Texture #{n}'.

#### Context Menu
Use the context menu to configure the texture sending properites.
+ **Sender Enable/Disable** : Enable or disable the frame sharer sender.
+ **Output Resolution** : Select the resolution of the output texture.
+ **Output Scaling** :
	+ None : Don't do any scaling. Texture will be rendered as is and may not take up the entire module screen or be cropped.
	+ Fill (Skew) : Fill the module screen with the texture in the x and y directions even if the dimesions are skewed/stretched/squished.
	+ Fill (Width) : Fill to match the width of the module screen. The texture may be cropped in the y-direction or not fill the screen vertically.
	+ Fill (Height) : Fill to match the height of the module screen. The texture may be cropped in the x-direction or not fill the screen horizontally.
	+ Fill (Width & Height) : Fill to match the width and height of the module screen while keeping the aspect ratio. May not fill up the entire canvas in x or y.

---

### texIn 

**texIn** takes an incoming frame/texture from an external sharer like Spout and outputs a 'Texture CV' to use in other modules.
Currently, external framesharing is only supported in Spout (Windows). 

#### User Controls
**Screen** : Click on the screen to select a sender to use as the source.

#### Context Menu
Use the context menu to configure the texture receiving properites.
+ **Sender** : Select a frame share sender from the list.
+ **Rx Scaling** : Select the scaling method for the received texture. The texture will be placed on a canvas of the default size.
	+ None : Don't do any scaling. Texture will be rendered as is and may not take up the entire module screen or be cropped.
	+ Fill (Skew) : Fill the module screen with the texture in the x and y directions even if the dimesions are skewed/stretched/squished.
	+ Fill (Width) : Fill to match the width of the module screen. The texture may be cropped in the y-direction or not fill the screen vertically.
	+ Fill (Height) : Fill to match the height of the module screen. The texture may be cropped in the x-direction or not fill the screen horizontally.
	+ Fill (Width & Height) : Fill to match the width and height of the module screen while keeping the aspect ratio. May not fill up the entire canvas in x or y.


## Generation Modules

### WAVE
![WAVE: multi-function video oscillator](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/WAVE_2.png?raw=true)

**WAVE** is a multi-function video oscillator. It is a blendable sine-, triangle-, ramp-, or square-shaped, 
horizontal or vertical, black-to-white gradient generator. It features built-in mirror and flip effects.

#### User Controls & CV Inputs
+ **WAVE** : The waveform type (sine, triangle, ramp, square). The output is blended between two types if the input is in between. If the corresponding input is active, then this user control is ignored.
+ **H/V** : Switch between horizontal or vertical. If the corresponding input is active, then this user control is ignored.
+ **MIRROR** : Turn mirroring on or off. If the corresponding input is active, then this user control is ignored.
+ **FLIP** : Flip the image. If the corresponding input is active, then this user control is ignored.
+ **FREQ** : (CV + Knob) The waveform frequency.
+ **PHASE** : (CV + Knob) The waveform frequency.
+ **SPEED** : (CV + Knob) Animation speed/time control (speed up or slow down).
+ **AMPLITUDE** : (CV + Knob) The waveform amplitude.
+ **BIAS** : (CV + Knob) The waveform offset.
#### CV Outputs
+ **OUT** : (Texture CV) The resulting output texture.


---

### audioToImage & audioToImage2Ch
![audio2Image](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/audio2Image.png?raw=true)

**audioToImage** generates textures from audio CVs for us in ISFs that need and *audio* or *audioFFT* input.
This comes in two flavors: single channel (mono) and stereo (2-channel).

#### CV Inputs
+ **AUDIO**/**AUDIO CH 1** : Audio Channel input CV. Channel 1 for the 2-channel module.
+ **AUDIO CH 2** : (2-channel module only) Audio Channel 2 input CV.
#### CV Outputs
+ **AUDIO** : (Texture CV) The resulting audio output texture.
+ **FFT** : (Texture CV) The resulting FFT output texture.
+ **FRAME** : Trigger when new frame is generated.
#### Context Menu
+ **Sample Size** : The sample buffer size.
+ **Sample Rate** : The sample rate.

---

## Filter Modules

### BLEND
![BLEND: 2-channel texture mixer with selectable blend mode](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/BLEND_2.png?raw=true)

**BLEND** is a 2-channel texture mixer with selectable blend mode. 
It allows mixing between the two input textures and the final blend (crossfader and CV-controllable).

#### User Controls
+ **BLEND MODE** : The blending mode (see available [Blend Modes](#blend-modes)). If the corresponding input is active, then this user control is ignored.    
  - Hold left-click on the screen and mouse-up or mouse-down to change the mode.    
  - (Context Menu) Select the mode from the list.   
+ **<** / **>** : Buttons decrement or increment the selected blending mode. 
+ **MIX** : The mix between input texture A, the final blend, and input texture B. If the corresponding input is active, then this user control is ignored.
#### CV Inputs
+ **BLEND MODE** : Blend mode. If this input is active, then the user control is ignored.
+ **MIX** : The mix between A, A+B, and B for the final texture output. If this input is active, then the user control is ignored.
+ **A** : Texture input A (background).
+ **B** : Texture input B (foreground).
#### CV Outputs
+ **OUT** : (Texture CV) The resulting output texture.

#### Blend Modes
Current blend modes:
+ Source-Over (Normal)
+ Darken (Min)
+ Lighten (Max)
+ Multiply
+ Screen
+ Color Burn
+ Color Dodge
+ Linear Burn (Black Subtract)
+ Linear Dodge (White Add)
+ Overlay
+ Soft-Light
+ Hard-Light
+ Vivid-Light
+ Linear-Light
+ Pin-Light
+ Contrast
+ Contrast (Centering)
+ Vector x2
+ Average
+ Difference
+ Exclusion
+ White Subtract
+ Divide
+ Hue
+ Saturation
+ Color
+ Luminosity
+ Noise

---

### COLOR
![COLOR: bi-polar colorizer](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/WAVE_COLOR_2.png?raw=true)

**COLOR** is a bi-polar colorizer with control of the brightness and contrast of red, green, and blue channels with invert switches on each channel.

#### User Controls & Input CVs
+ **IN** : (CV only) (Texture CV) The input texture.
+ For each channel (Red, Green, Blue), CV and user controls for:
    + **BRIGHT**: (CV + Knob) Brightness for the channel. 
    + **CONTRAST**: (CV + Knob) Contrast for the channel.
    + **INV**: Invert the color. If the CV input is active, the switch is ignored.
#### Output CVs
+ **OUT** : (Texture CV) The resulting output texture.

---

### texRGBY
![texRGBY](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/texRGBY.png?raw=true)

**texRGBY** separates the incoming texture into red, green, blue, and luma video textures.

#### User Controls & Input CVs
+ **IN** : (CV only) (Texture CV) The input texture.
+ For each channel RGBY (Red, Green, Blue, Luma):
    + **BRIGHT**: (CV + Knob) Brightness for the channel.
    + **CONTRAST**: (CV + Knob) Contrast for the channel.
#### Output CVs
+ **R** : (Texture CV) The Red output texture.
+ **G** : (Texture CV) The Green output texture.
+ **B** : (Texture CV) The Blue output texture.
+ **Y** : (Texture CV) The Luma output texture.

---

### texCMYK
![texCMYK](https://github.com/j4s0n-c/ISF-Synth-VCV/blob/main/screenshots/texCMYK.png?raw=true)

**texCMYK** separates the incoming texture into cyan, magenta, yellow, and black video textures.

#### User Controls & Input CVs
+ **IN** : (CV only) (Texture CV) The input texture.
+ For each channel CMYK (Cyan, Magenta, Yellow, Key):
    + **BRIGHT**: (CV + Knob) Brightness for the channel.
    + **CONTRAST**: (CV + Knob) Contrast for the channel.
#### Output CVs
+ **C** : (Texture CV) The Cyan output texture.
+ **M** : (Texture CV) The Magenta output texture.
+ **Y** : (Texture CV) The Yellow output texture.
+ **K** : (Texture CV) The Key (Black) output texture.

---

# Third-party Libraries
+ [VVISF & VVGL](https://github.com/mrRay/VVISF-GL) - For loading and rendering ISFs.
+ [Spout2](https://github.com/leadedge/Spout2) - (Windows) For texture sharing.

# Other Resources
+ Official ISF Site: [isf.video](https://isf.video/) - Documentation, examples, development resources, and ISF files (pretty much everything ISF).
+ Open Source ISF Files: [VIDVOX's ISF-Files github](https://github.com/Vidvox/ISF-Files)
+ Frame-sharing (Windows): Download [Spout](https://spout.zeal.co/)

# Licenses
+ Third-party Libraries (all in one file): [LICENSES.md](LICENSES.md) 
+ [Spout2](https://raw.githubusercontent.com/leadedge/Spout2/master/LICENSE)
+ [VVISF & VVGL](https://raw.githubusercontent.com/mrRay/VVISF-GL/master/LICENSE.txt)


