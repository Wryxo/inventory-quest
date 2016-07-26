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

    public static StatCheck FitStatCheck(string statName, ArrayList f) //May be replaced later on, it's really dumb
    {
        var x = new StatCheck();
        x.statName = statName;
        x.nDice = 1;
        float high, low;
        bool lowset = false;
        float dy;
        float phigh = UnityEngine.Random.value;
        float plow = UnityEngine.Random.value;
        low = 0;
        high = 0;
        if(phigh < plow)
        {
            var tmp = phigh;
            phigh = plow;
            plow = tmp;
        }
        for(int i = 0; i < f.Count; i++)
        {
            if (!lowset)
            {
                if (plow < ((Vector2)f[i]).y)
                {
                    dy = ((Vector2)f[i]).y - ((Vector2)f[i - 1]).y;
                    low = (((Vector2)f[i - 1]).x * (((Vector2)f[i]).y - plow) + ((Vector2)f[i]).x * (plow - ((Vector2)f[i - 1]).y)) / dy;
                    lowset = true;
                }
            } else
            {
                if (plow < ((Vector2)f[i]).y)
                {
                    dy = ((Vector2)f[i]).y - ((Vector2)f[i - 1]).y;
                    high = (((Vector2)f[i - 1]).x * (((Vector2)f[i]).y - phigh) + ((Vector2)f[i]).x * (phigh - ((Vector2)f[i - 1]).y)) / dy;
                    break;
                }
            }
        }
        x.baseDifficulty = (int)Mathf.Floor(low);
        x.sidesPerDie = (int)Mathf.Ceil(high - low);
        return x;
    }

    public static string Attract = "Attractivity";
    public static string Jump = "Jump";
    public static string Swim = "Swim";
    public static string Branches = "Branches";
    public static string Run = "Run";

}
