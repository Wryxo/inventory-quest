using UnityEngine;
using System.Collections;

public static class HelpFunctions {

	public static Texture2D spriteToTexture(Sprite sprite)
    {
        var croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        var pixels = sprite.texture.GetPixels(0, 0, (int)sprite.rect.width, (int)sprite.rect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;
    }

    public static string Attract = "Attractivity";
    public static string Jump = "Jump";
    public static string Swim = "Swim";
    public static string Branches = "Branches";
    public static string Run = "Run";

}
