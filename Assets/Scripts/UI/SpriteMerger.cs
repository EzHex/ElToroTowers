using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMerger : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesToMerge = null;
    [SerializeField] private SpriteRenderer finalSpriteRenderer = null;

    public SpriteRenderer getRender(Sprite baseSprite, int number)
    {
        getBaseSprite(baseSprite);
        MergeNumber(number);
        return finalSpriteRenderer;
    }
    public void getBaseSprite(Sprite baseSprite)
    {
        spritesToMerge[0] = baseSprite;
    }
    public void Merge()
    {
        Resources.UnloadUnusedAssets();
        var newTex = new Texture2D(256, 256);

        //Initiate the new texture with white transperent pixels
        for(int x = 0; x < newTex.width; x++)
        {
            for(int y =0; y < newTex.height; y++)
            {
                newTex.SetPixel(x, y, new Color(1, 1, 1, 1));
            }
        }

        for (int i = 0; i < spritesToMerge.Length; i++)
        {
            for (int x = 0; x < spritesToMerge[i].texture.width; x++)
            {
                for (int y = 0; y < spritesToMerge[i].texture.width; y++)
                {
                    
                    var color = spritesToMerge[i].texture.GetPixel(x, y);
                    if (color != new Color(1, 1, 1, 1))
                    {
                        newTex.SetPixel(x, y, color);
                    }
                }
            }
        }
        newTex.Apply();
        var finalSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f),256);
        finalSprite.name = "New Sprite";
        finalSpriteRenderer.sprite = finalSprite;
    }
    public void MergeNumber(int number)
    {
        Resources.UnloadUnusedAssets();
        var newTex = new Texture2D(256, 256);

        //Initiate the new texture with white transperent pixels
        for (int x = 0; x < newTex.width; x++)
        {
            for (int y = 0; y < newTex.height; y++)
            {
                newTex.SetPixel(x, y, new Color(1, 1, 1, 1));
            }
        }
        for (int x = 0; x < spritesToMerge[0].texture.width; x++)
        {
            for (int y = 0; y < spritesToMerge[0].texture.height; y++)
            {

                var color = spritesToMerge[0].texture.GetPixel(x, y);
                if (color != new Color(1, 1, 1, 1))
                {
                    newTex.SetPixel(x, y, color);
                }
            }
        }
        for (int x = 0; x < spritesToMerge[number].texture.width; x++)
        {
            for (int y = 0; y < spritesToMerge[number].texture.height; y++)
            {

                var color = spritesToMerge[number].texture.GetPixel(x, y);
                if (color != new Color(1, 1, 1, 1))
                {
                    newTex.SetPixel(x, y, color);
                }
            }
        }

        newTex.Apply();
        var finalSprite = Sprite.Create(newTex, new Rect(0, 0, newTex.width, newTex.height), new Vector2(0.5f, 0.5f),256);
        finalSprite.name = "New Sprite";
        finalSpriteRenderer.sprite = finalSprite;
    }
}
