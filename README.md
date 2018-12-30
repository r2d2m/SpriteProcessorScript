# SpriteProcessorScript

Note: Will only work when manually triggering "Reimport" on a file (right-click in the inspector), not when importing a file to Unity for the first time.

Place this in a folder title "Editor" in your Unity project directory.

In order for this to work the grandparent directory must be names "AutoProcessedSpriteSheet"
The immediate parent directory must be named the desired tile size of the sliced spritesheet in pixels, plus underscore, plus the desired pixels per unit

eg. "16_100"

It should be that simple! With any luck, your sprite sheet will now be automatically sliced.

Many of the settings are for pixel art games (FilterMode = Point, etc). Feel free to edit the script to suit the needs of your project.
