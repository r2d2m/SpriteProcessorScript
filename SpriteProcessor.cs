using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Note: Will only work when manually triggering "Reimport" on a file, not when importing a file to Unity for the first time.
/// In order for this to work the grandparent directory must be names "AutoProcessedSpriteSheets"
/// The immediate parent directory must be named the desired horizontal tile size of the sliced spritesheet in pixels, plus underscore...
/// plus the desired horizontal tile size of the sliced spritesheet in pixels, plus underscore...
/// plus the desired pixels per unit
///     eg. "16__12_100"
/// </summary>
public class SpriteProcessor : AssetPostprocessor
{
    bool continueProcess = false;
    int ppu = 16;
    int spriteSizeX = 16;
    int spriteSizeY = 16;

    void OnPreprocessTexture()
    {
        if (Directory.GetParent(assetPath).Parent.Name != "AutoProcessedSpriteSheets") return;

        TextureImporter ti = (TextureImporter)assetImporter;
        ti.textureType = TextureImporterType.Sprite;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.mipmapEnabled = false;
        ti.filterMode = FilterMode.Point;

        string[] parentDir = Directory.GetParent(assetPath).Name.Split('_');

        //If we can't parse the parent directory values as integers, don't continue processing the sprite
        if ( System.Int32.TryParse(parentDir[0], out spriteSizeX) && System.Int32.TryParse(parentDir[1], out spriteSizeY) && System.Int32.TryParse(parentDir[2], out ppu) )
        {
            ti.spritePixelsPerUnit = ppu;
            continueProcess = true;
        }
    }

    public void OnPostprocessTexture(Texture2D texture)
    {
        //If we failed requirement during the PreProcess stage, return
        if (!continueProcess) return;

        if ( spriteSizeX > 0 && spriteSizeY > 0)
        {
            int colCount = texture.width / spriteSizeX;
            int rowCount = texture.height / spriteSizeY;
            string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            int i = 0;

            List<SpriteMetaData> metas = new List<SpriteMetaData>();

            for (int r = rowCount - 1; r > -1; r--)
            {
                for (int c = 0; c < colCount; c++)
                {
                    Debug.LogWarning("spriteSizeY  " + spriteSizeY);

                    SpriteMetaData meta = new SpriteMetaData();
                    meta.rect = new Rect(c * spriteSizeX, r * spriteSizeY, spriteSizeX, spriteSizeY);
                    meta.name = fileName + "_" + i;
                    metas.Add(meta);
                    i++;
                }
            }
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.spritesheet = metas.ToArray();

        }
    }
    
}
