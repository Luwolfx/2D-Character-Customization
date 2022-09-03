using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class OutfitInformation
{
    [Header("Outfit Information")]
    public List<Sprite> icon = new List<Sprite>();
    public string outfitName;

    [Header("Shop Information")]
    public int price;
    public bool buyed => (PlayerPrefs.GetInt(outfitName, 0) == 1 || price <= 0);
}
