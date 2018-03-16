using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class SpriteProcessor : AssetPostprocessor
{

    void OnPreprocessTexture()
    {
        //For a sprite-based games, we want textures to have certain common properites whether they are sprite sheets or not
        //However, some of these values should probably change from project to project, so please change what I have here to fit your needs
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spriteImportMode = SpriteImportMode.Multiple;
        textureImporter.mipmapEnabled = false;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.spritePixelsPerUnit = 16;
    }

    public void OnPostprocessTexture(Texture2D texture)
    {
        string path =  assetPath;
        string parentDirectoryPathName = Directory.GetParent( path ).Name;
        int spriteSize = 0;

        //If destination directory name of imported texture can be converted to an int, assume that it was meant to be sliced as a sprite sheet 

        if (System.Int32.TryParse(parentDirectoryPathName, out spriteSize) )
        {
            int colCount = texture.width / spriteSize;
            int rowCount = texture.height / spriteSize;

            List<SpriteMetaData> metas = new List<SpriteMetaData>();

            for (int r = 0; r < rowCount; ++r)
            {
                for (int c = 0; c < colCount; ++c)
                {
                    SpriteMetaData meta = new SpriteMetaData();
                    meta.rect = new Rect(c * spriteSize, r * spriteSize, spriteSize, spriteSize);
                    meta.name = c + "-" + r;
                    metas.Add(meta);
                }
            }

            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.spritesheet = metas.ToArray();

            //Display pop-up window exposing that something auto-magical just happened
            SliceSpriteWindow window = ScriptableObject.CreateInstance<SliceSpriteWindow>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 125);
            window.ShowSpriteSlicePopup(spriteSize, texture.width, texture.height, path);
        }
    }
    
}

public class SliceSpriteWindow : EditorWindow
{

    string TexturePath;
    int TextureWidth = 0;
    int TextureHeight = 0;
    int SpriteSize = 0;

    public void ShowSpriteSlicePopup(int spriteSize, int textureWidth, int textureHeight, string texturePath)
    {
        TexturePath = texturePath;
        TextureWidth = textureWidth;
        TextureHeight = textureHeight;
        SpriteSize = spriteSize;
        ShowPopup();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField(TexturePath +" sliced as a sprite sheet", EditorStyles.wordWrappedLabel);
        GUILayout.Space(15);
        EditorGUILayout.LabelField("Sheet size: " + TextureWidth + "x" + TextureHeight, EditorStyles.wordWrappedLabel);
        GUILayout.Space(15);
        EditorGUILayout.LabelField("Sprite size: " + SpriteSize + "x" + SpriteSize, EditorStyles.wordWrappedLabel);
        GUILayout.Space(15);

        if (GUILayout.Button("Awesome")) this.Close();
    }

}
