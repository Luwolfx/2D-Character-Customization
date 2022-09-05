using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Legs_", menuName = "Outfit/Legs")]
public class Legs : ScriptableObject
{
    [Header("Outfit Information")]
    public OutfitInformation info;

    [Header("Outfit Values")]
    public int outfitId = -1;
    public int rightLegOutfitId = -1;
    public int leftLegOutfitId = -1;
}
