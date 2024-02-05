using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RsourceManager : Singleton<RsourceManager>
{
    [SerializeField] private List<Sprite> trapSprites;

    public Sprite GetSprite(int _id)
    {
        if(trapSprites.Count < _id)
        {
            Debug.LogError("trapSprite List out idx");
            return null;
        }
        return trapSprites[_id];
    }
}
