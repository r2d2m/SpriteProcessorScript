# SpriteProcessorScript

NOTE: Originally tested in Unity 5.2.2f1. Works up to 2017.4.1f1, but doesn't actually process the sprites until you reimport the assets into your project (the Reimport option shows up by right-clicking the asset within Unity).

<h2>Importing Assets for 2D Pixel Sprite Games</h2>

<p>Unity was not meant for 2D games. Especially not pixel art sprite and tile-based 2D games. The headache of importing a sprite sheet and slicing it up, especially coming from other <a href="https://haxeflixel.com/documentation/flxsprite/">more straightforward frameworks</a>, was incredibly disheartening. Imagine importing a sprite sheet into your project only to discover that you had to manually change half-a-dozen attributes each and every time? Or that, instead of slicing up a sprite sheet by simply defining a couple parameters, you had to manually slice up each frame? Well guess what, you don&rsquo;t have to imagine it, that&rsquo;s just how Unity works.</p>

<figure class="tmblr-full" data-orig-height="88" data-orig-width="516"><img alt="image" data-orig-height="88" data-orig-width="516" src="https://78.media.tumblr.com/97b265f92483420c7db443a4671bbecf/tumblr_inline_p5p6eovsOG1sph69g_540.png" /></figure>

<p>I desperately needed a solution that would return this process to the completely trivial category that it belongs in. And then I learned that my salvation lay in editor scripts.</p>

<p>Certain editor scripts execute automatically every time a certain Unity function is performed. Importing assets is one of those functions. To make an editor script, you must first simply create a new script in a directory called &ldquo;Editor&rdquo; somewhere inside the &ldquo;Assets&rdquo; directory of your project. This should be a class that extends AssetPostprocessor. The code listed in the OnPreprocessTexture and OnPostprocessTexture methods will change the behavior of how your assets are imported into your project.</p>

<p>So instead of manually editing all of these properties every time you import a sprite:</p>

<figure class="tmblr-full" data-orig-height="463" data-orig-width="330"><img alt="image" data-orig-height="463" data-orig-width="330" src="https://78.media.tumblr.com/842f3eecf0cf6895cfee75b2302a3120/tumblr_inline_p5p6f5Ug2q1sph69g_540.png" /></figure>

<p>You could just edit this in script, once:</p>

<figure class="tmblr-full" data-orig-height="155" data-orig-width="410"><img alt="image" data-orig-height="155" data-orig-width="410" src="https://78.media.tumblr.com/62e2c9fbb5b3b29a694527c4fb997ad2/tumblr_inline_p5p6fqiMee1sph69g_540.png" /></figure>

<p>Now I don&rsquo;t have to manually change filterMode to Point on every single sprite I import! Score. (FilterMode.Point keeps your texture art sharp, which is ideal for pixel art. Unity assumes you were importing a large texture to be used in a high-verisimilitude 3D game, in which case, a bilinear/trilinear filter might be better.) However, these properties will probably change from project to project, so don&rsquo;t assume the code I&rsquo;ve posted here is canon.</p>

<p>Next we&rsquo;ll use the OnPostprocessTexture() method to slice up our script for us by iterating through the texture and storing the slices in an array on the metadata of the asset (you can <a href="https://github.com/dithyrambs/SpriteProcessorScript">see the code on GitHub</a> if you want the details). The problem, however, was in deciding how to determine the dimensions of the sprites that needs to be sliced, and which of the imported assets should even be sprites in the first place. I didn&rsquo;t want to have to repeatedly determine these properties in some editor window pop-up every single time I imported a sprite (that would defeat the entire purpose of this exercise). Eventually I settled on an auto-magical solution. Simply drag your asset into a folder named after an integer somewhere in your project&rsquo;s &ldquo;Resources&rdquo; directory (eg. a directory named &ldquo;16&rdquo;), and the editor script will assume you want that asset sliced up as a sprite sheet with 16 by 16 pixel sprite dimensions.</p>

<p>I mean just look at this brilliant, sliced sprite:</p>

<figure class="tmblr-full" data-orig-height="196" data-orig-width="321"><img alt="image" data-orig-height="196" data-orig-width="321" src="https://78.media.tumblr.com/9ff747c5e3ed50f396cbf4599788d05d/tumblr_inline_p5p6g368YK1sph69g_540.png" /></figure>

<p>In general auto-magic solutions make me slightly nervous. I decided to add a little pop-up window telling the user that the sprite was sliced, so the user was aware that something just happened.</p>

<figure class="tmblr-full" data-orig-height="147" data-orig-width="512"><img alt="image" data-orig-height="147" data-orig-width="512" src="https://78.media.tumblr.com/f22bc3f1dac3059c1b9331203bd4692d/tumblr_inline_p5p6gfHd3F1sph69g_540.png" /></figure>


Original post:
http://gamasutra.com/blogs/AlexBelzer/20180319/315600/Working_with_Pixel_Art_Sprites_in_Unity_Importing_Assets.php
