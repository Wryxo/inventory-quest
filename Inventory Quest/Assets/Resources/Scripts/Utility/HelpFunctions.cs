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
        float min = (float)((DictionaryEntry)f[0]).Value;
        float max = (float)((DictionaryEntry)f[f.Count - 1]).Value;
        float range = max - min;
        float mean = 0;
        float pquad = 0;
        float quad = 0;
        float pmean;
        for (int i = 1; i < f.Count - 1; i++)
        {
            mean += (float)((DictionaryEntry)f[i]).Value * (float)((DictionaryEntry)f[i]).Key;
            quad += (float)((DictionaryEntry)f[i]).Value;
            pquad += (float)((DictionaryEntry)f[i]).Key;
        }
        if (quad > 0)
        {
            pmean = mean / pquad;
            mean /= quad;
        }
        else
        {
            mean = min + range / 2;
            pmean = .5f;
        }
        mean -= min;
        float a = UnityEngine.Random.value;
        float c = UnityEngine.Random.value * mean + UnityEngine.Random.value * (range - mean);
        if (pmean != 0 && (pmean == 1 || c / pmean < (range - c) / (1 - pmean)))
        {
            min += c * (1 - a);
            range = c * a / pmean;
            x.baseDifficulty = (int)Mathf.Floor(min);
            x.sidesPerDie = (int)Mathf.Ceil(range);
        }
        else
        {
            max -= c * (1 - a);
            range = (range - c) * a / (1 - pmean);
            x.baseDifficulty = (int)Mathf.Floor(max - range);
            x.sidesPerDie = (int)Mathf.Ceil(range);
        }
        return x;
    }

    public static string Attract = "Attractivity";
    public static string Jump = "Jump";
    public static string Swim = "Swim";
    public static string Branches = "Branches";
    public static string Run = "Run";

}
