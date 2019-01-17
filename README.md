# SpriteProcessorScript

<b> A Unity Editor script for automatically slicing sprite sheets. </b>

Note: Will only work when manually triggering "Reimport" on a file (right-click in the inspector), not when dragging a file into the Unity editor for the first time.

Place this script in a folder titled "Editor" in your Unity project directory.

In order for this to work the grandparent directory must be named "AutoProcessedSpriteSheets" (somewhere in your project's "Assets" directory, of course)

The immediate parent directory must be named the desired horizontal tile size in pixels, plus underscore, plus the desired vertical tile size in pixels, plus underscore,  plus the desired pixels per unit . eg. "16_12_100".

Following along this example, your full asset path should look somethings like this: "/Assets/AutoProcessedSpriteSheet/16__12_100/heroSpriteSheet.png"

Then simply right-click and select 'Reimport'. It should be that simple! With any luck, your sprite sheet will now be automatically sliced into tiles of 16 by 12 pixels, and the PixelsPerUnit should be set to 100!

Many of the settings are for pixel art games (FilterMode = Point, etc). Anything places into the "AutoProcessedSpriteSheets" directory without another child directory will have these settings applied. However, feel free to edit the script to suit the needs of your project.
