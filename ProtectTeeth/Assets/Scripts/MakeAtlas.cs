using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine.U2D;

[CreateAssetMenu(menuName = "Tools/Sprite Atlas Generator Config")]
public class SpriteAtlasConfig : ScriptableObject
{
    public string inputFolderPath = "Assets/Sprites";
    public string outputAtlasPath = "Assets/SpriteAtlases/MyAtlas.spriteatlas";
}

[CustomEditor(typeof(SpriteAtlasConfig))]
public class SpriteAtlasConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // 기본 필드 표시

        SpriteAtlasConfig config = (SpriteAtlasConfig)target;

        if (GUILayout.Button("Generate Sprite Atlas"))
        {
            GenerateAtlas(config.inputFolderPath, config.outputAtlasPath);
        }
    }

    void GenerateAtlas(string folderPath, string atlasSavePath)
    {
        string[] imagePaths = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);
        List<Object> spriteAssets = new List<Object>();

        foreach (string path in imagePaths)
        {
            string assetPath = path.Replace("\\", "/");
            if (!assetPath.StartsWith("Assets")) continue;

            Object sprite = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (sprite != null)
                spriteAssets.Add(sprite);
        }

        if (spriteAssets.Count == 0)
        {
            return;
        }

        SpriteAtlas atlas = new SpriteAtlas();
        ApplyAtlasSettings(atlas);
        atlas.Add(spriteAssets.ToArray());

        string directory = Path.GetDirectoryName(atlasSavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        AssetDatabase.CreateAsset(atlas, atlasSavePath);
        AssetDatabase.SaveAssets();

    }
    public static void ApplyAtlasSettings(SpriteAtlas atlas)
    {
        // 기본 Packing 설정
        atlas.SetPackingSettings(new SpriteAtlasPackingSettings
        {
            enableRotation = true,
            enableTightPacking = true,
            padding = 4
        });

        // 기본 Texture 설정
        atlas.SetTextureSettings(new SpriteAtlasTextureSettings
        {
            readable = false,
            generateMipMaps = false,
            sRGB = true,
            filterMode = FilterMode.Bilinear
        });

        // 플랫폼별 설정 (Default platform)
        TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings
        {
            name = "", // 빈 문자열은 default platform 의미
            overridden = true,
            maxTextureSize = 4096,
            format = TextureImporterFormat.Automatic,
            textureCompression = TextureImporterCompression.Uncompressed
        };

        atlas.SetPlatformSettings(platformSettings);
    }
}
