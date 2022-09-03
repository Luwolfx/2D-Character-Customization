using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitApplier : MonoBehaviour
{
    [Header("Body part to change outfit")]
    public SpriteRenderer bodyPart;

    [Header("Outfits to apply to Body Part")]
    public List<Sprite> outfits = new List<Sprite>();

    private int currentEquiped;
    
    public void NextOption()
    {
        currentEquiped++;

        if(currentEquiped >= outfits.Count)
            currentEquiped = 0;

        SetOutfit(currentEquiped);
    }

    public void PreviousOption()
    {
        currentEquiped--;

        if(currentEquiped < 0)
            currentEquiped = outfits.Count - 1;

        SetOutfit(currentEquiped);
    }

    public void SetOutfit(int id)
    {
        bodyPart.sprite = outfits[id];
    }
}
