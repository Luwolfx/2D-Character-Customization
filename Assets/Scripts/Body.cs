using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "Body_", menuName = "Outfit/Body")]
public class Body : ScriptableObject
{
    [Header("Outfit Information")]
    [SerializeField] public OutfitInformation info;

    [Header("Outfit Values")]
    public int outfitId = -1;
    [JsonIgnore] public bool showPants = false;
}
