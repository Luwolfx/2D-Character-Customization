using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Head_", menuName = "Outfit/Head")]
public class Head : ScriptableObject
{
    [Header("Outfit Information")]
    public OutfitInformation info;

    [Header("Outfit Values")]
    public int outfitId = -1;
}
