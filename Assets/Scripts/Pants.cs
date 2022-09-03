using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pants_", menuName = "Outfit/Pants")]
public class Pants : ScriptableObject
{
    [Header("Outfit Information")]
    public OutfitInformation info;

    [Header("Outfit Values")]
    public int outfitId = -1;
}
