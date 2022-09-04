using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hands_", menuName = "Outfit/Hands")]
public class Hands : ScriptableObject
{
    [Header("Outfit Information")]
    public OutfitInformation info;

    [Header("Outfit Values")]
    public int outfitId = -1;
    public int rightHandOutfitId = -1;
    public int leftHandOutfitId = -1;
}
