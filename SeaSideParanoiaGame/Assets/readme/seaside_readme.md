# Seaside Paranoia Documentation

This readme is for my teammates (the artists and designers mainly) to understand how to create their own yarn scripts and how to setup their assets.


## Asset Settings

### Sprites

The sample sprites and my test sprites used a width of 600 pixels with pixel per unity of 115.

*BUT*, Syrus has done some testing and **1000 x 1200** pixels is a nicer canvas to draw on.

Some settings on the sprite should be:
- Texture Type: Sprite (2D and UI)
- Pixels Per Unit: 115 (this is the Unity default and we're just gonna work with it)
- Pivot: Bottom

### Audio

### Backgrounds

The screen size is set to 1920 x 1080 (1080p).

### UI

The screen size is set to 1920 x 1080 (1080p).

## Scripting

They have great documentation for that on their website. You can [learn the basics here](https://docs.yarnspinner.dev/write-yarn-scripts/scripting-fundamentals). You can also [learn more advanced techniques here](https://docs.yarnspinner.dev/write-yarn-scripts/advanced-scripting).

### Locations

The screen dimensions are currently 1920 x 1080 (1080p).

The origin (0, 0) is the bottom-left corner of the screen.

Here is a list of location you can put or move actors to and where they place the sprite:
- leftedge (0% of `screen_width`)
- bottomedge (0% of `screen_height`)
- loweredge (0% of `screen_height`)
- left (25% of `screen_width`)
- bottom (25% of `screen_height`)
- lower (25% of `screen_height`)
- center (50% of `screen_width`)
- middle (50% of `screen_width`)
- right (75% of `screen_width`)
- top (75% of `screen_height`)
- upper (75% of `screen_height`)
- rightedge (100% of `screen_width`)
- topedge (100% of `screen_height`)
- upperedge (100% of `screen_height`)
- offleft (-33% of `screen_width`)
- offright (133% of `screen_width`)

### Colors

The visual novel manager uses HTML colors. According to [this W3School tutorial](https://www.w3schools.com/html/html_colors.asp), they can from a set of predefined color names or a hex code.

We should stick to predefined names or hexcode:
- predefined names: Unity supports a small portion of the 140 standard HTML color names. You can find that list [here](https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html) or below:
        - red
        - cyan
        - blue
        - darkblue
        - lightblue
        - purple
        - yellow
        - lime
        - fuchsia
        - white
        - silver
        - grey
        - black
        - orange
        - brown
        - maroon
        - green
        - olive
        - navy
        - teal
        - aqua
        - magenta
- hex code allows you to specific red, green, blue, and alpha components in the format `#RRGGBBAA`.
    - Example: #ffff0077 is a translucent yellow. 

## Commands

Many of the commands are from the Visual Novel sample the YarnSpinner team made and the instructions are copied from the `VisualNovel_Readme.md` that came along with it.

### BASIC COMMANDS

#### `<<Scene (spriteName)>>`
set background image to (spriteName)

---

#### `<<Draw (spriteName), (x=0.5), (y=0.5)>>`
show (spriteName) at (x,y) in normalized screenspace (0.0-1.0)
- 0.0 is like 0% left / bottom of screen, 1.0 is like 100% right / top of screen
- for x or y, you can also use keywords like "right", "upper", "top" (0.75) or "middle", "center" (0.5) or "left", "lower", "bottom" (0.25)
- when x and/or y are omitted (e.g. `<<Show TreeSprite>>`) then they default to values of 0.5... thus, omitting both x and y would set the position to (0.5, 0.5) in normalized screenspace (center of the screen)

---

#### `<<Hide (actorOrSpriteName)>>`
hide and delete any actor called (actorOrSpriteName) or any sprites called (actorOrSpriteName)
- this command supports wildcards (*)
    - example 1: `<<Hide Sally*>>` will hide all actors and sprites with names that start with "Sally"
    - example 2: `<<Hide *Cat>>` will hide all objects with names that end with "Cat"

---

#### `<<HideAll>>`
hides all sprites and actors, takes no parameters

---

#### `<<Act (actorName), (spriteName), (x=0.5), (y=0.5), (HTMLcolor=black) >>`
very useful for persistent characters... kind of like `<<Show>>` except it also has (actorName), which links the sprite to that actorName
- anytime a character named (actorName) talks in a Yarn script, this sprite will automatically highlight
- also good for changing expressions, e.g. calling `<<Act Dog, dog_happy, left>>` and then `<<Act Dog, dog_sad>>` will automatically swap the sprite and preserve its positioning... this is better than tediously calling `<<Show dog_happy>>` and then `<<Hide dog_happy>>` and then `<<Show dog_sad>>`, etc.
- (HTMLcolor) is a web hex color (like "#ffffff") or a common color keyword (like "white")... here, it affects the color of the name label in VNDialogueUI, but I suppose you could use it for anything

---


### SPRITE TRANSFORM COMMANDS

#### `<<Flip (actorOrSpriteName), (xPosition=0.0)>>`
horizontally flips an actor or sprite, in case you need to make your characters look around or whatever
- if (xPosition) is defined, it will force the actor to face a specific direction

---

#### `<<Move (actorOrSpriteName), (xPosition=0.5), (yPosition=0.5), (moveTime=1.0)>>`
animates / slides an actor or sprite toward a new position, good for animating walking or running
- (moveTime) is how long it will take to move from current position to new position, in seconds
- for notes on (xPosition) and (yPosition), see entry for `<<Show>>` above
- KNOWN BUG: not recommended to use this at the same time as a `<<Shake>>` call... let the previous call finish, first

---

#### `<<Shake (actorOrSpriteName), (shakeTime=0.5)>>`
shakes an actor or a sprite, good for when someone is surprised or angry or laughing
- if shakeTime isn't specified, the default value 0.5 means "shake this object at 50% strength for 0.5 seconds"
- a shakeTime=1.0 means "shake this object at 100% for 1.0 seconds", and so on
- KNOWN BUG: not recommended to use this at the same time as a `<<Move>>` call... let the previous call finish, first

---


### CAMERA COMMANDS

#### `<<Fade (HTMLcolor=black), (startOpacity=0.0), (endOpacity=1.0), (fadeTime=1.0)>>`
fades the screen to a certain color, could be a fade in or a fade out depending on how you use it
- startOpacity and endOpacity are numbers 0.0-1.0 (like 0%-100%)
- by default with no parameters (`<<Fade>>`) will fade to black over 1.0 seconds
- (HTMLcolor) is a web hex color (like "#ffffff") or a common color keyword (like "white")

---

#### `<<FadeIn (fadeTime=1.0)>>`
useful shortcut for fading in, no matter what the previous fade settings were... equivalent to calling something like `<<Fade black, -1, 0.0, 1.0>>` (where startOpacity=-1 means it will reuse the last fade command's colors and opacity)

---

#### `<<CamOffset (xOffset=0), (yOffset=0), (moveTime=0.25)>>`
pans the camera based on x/y offset from the center over "moveTime" seconds
- example: ``<<CamOffset 0, 0, 0.1>>` centers the camera (default position) over 0.1 seconds
- note that the camera doesn't actually move, but fakes a camera movement by moving sprites
- by default, the background image is fixed like a skybox; if you want the background to pan too, then parent it to the "Sprites" game object in the prefab

---


### AUDIO COMMANDS

#### `<<PlayAudio (audioClipName),(volume),("loop")>>`
plays an audio clip named (audioClipName) at volume (0.0-1.0)
- if "loop" is present, it will loop the audio repeatedly until stopped; otherwise, the sound will end automatically

---

#### `<<StopAudio (audioClipName)>>`
stop playing any active audio clip named (audioClipName)

---

#### `<<StopAudioAll>>`
stops playing all sounds, no parameters




