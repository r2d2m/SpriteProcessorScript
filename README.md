# SpriteProcessorScript

This script should be used in Unity projects that uses sprite-based animations.
Place this script in a directory named Editor parented by your Assets directory in your Unity project. eg. "/Assets/Editor"

Everything in the OnPreprocessTexture method will run on every asset imported to the project.
This is where you would define attributes that should remain the same for all assets. For example, spritePixelsPerUnit should generally be consistent in a given project.

However, the asset will only be sliced if the directory name is only an integer. eg. "/Assets/Resources/Sprites/16"
The script slices the spreet sheet by the number of pixels of the name of the directory. eg. "16".
A pop-up will appear if the sprite sheet was successfully sliced.
