using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Object = UnityEngine.Object;

[Serializable]
public class ItemsEquiped
{
    public int headId = 0;
    [JsonIgnore] public Head head;

    public int bodyId = 2;
    [JsonIgnore] public Body body;

    public int handsId = 0;
    [JsonIgnore] public Hands hands;

    public int pantsId = 0;
    [JsonIgnore] public Pants pants;

    public int legsId = 0;
    [JsonIgnore] public Legs legs;

    [JsonIgnore] public bool loadedItems;

    void Start() 
    {
        head = new Head();
        body = new Body();
        hands = new Hands();
        pants = new Pants();
        legs = new Legs();
    }

    public async Task LoadSavedItems()
    {
        head = null;
        if(headId != -1)
        {
            List<Object> headObj =  new List<Object>( Resources.LoadAll("Outfits/Head", typeof(ScriptableObject)) );
            foreach(Object item in headObj)
            {
                if(((Head)item).outfitId == headId && GameController.CheckPurchased((ScriptableObject)item))
                {
                    head = (Head)item;
                    break;
                }
            }
        }
        if(head == null)
        {
            List<Object> headObj =  new List<Object>( Resources.LoadAll("Outfits/Head", typeof(ScriptableObject)) );
            foreach(Object item in headObj)
            {
                if(((Head)item).outfitId == 0 && GameController.CheckPurchased((ScriptableObject)item))
                {
                    head = (Head)item;
                    break;
                }
            }
        }

        body = null;
        if(bodyId != -1)
        {
            List<Object> bodyObj =  new List<Object>( Resources.LoadAll("Outfits/Body", typeof(ScriptableObject)) );
            foreach(Object item in bodyObj)
            {
                if(((Body)item).outfitId == bodyId && GameController.CheckPurchased((ScriptableObject)item))
                {
                    body = (Body)item;
                    break;
                }
            }
        }
        else
        {
            body = new Body()
            { 
                info = new OutfitInformation()
                {
                    outfitName = "Default",
                },
                outfitId = 0,
                showPants = true
            };
        }
        if(body == null)
        {
            List<Object> bodyObj =  new List<Object>( Resources.LoadAll("Outfits/Body", typeof(ScriptableObject)) );
            foreach(Object item in bodyObj)
            {
                if(((Body)item).outfitId == 2 && GameController.CheckPurchased((ScriptableObject)item))
                {
                    body = (Body)item;
                    break;
                }
            }
        }

        hands = null;
        if(handsId != -1)
        {
            List<Object> handsObj =  new List<Object>( Resources.LoadAll("Outfits/Hands", typeof(ScriptableObject)) );
            foreach(Object item in handsObj)
            {
                if(((Hands)item).outfitId == handsId && GameController.CheckPurchased((ScriptableObject)item))
                {
                    hands = (Hands)item;
                    break;
                }
            }
        }
        if(hands == null)
        {
            List<Object> handsObj =  new List<Object>( Resources.LoadAll("Outfits/Hands", typeof(ScriptableObject)) );
            foreach(Object item in handsObj)
            {
                if(((Hands)item).outfitId == 0 && GameController.CheckPurchased((ScriptableObject)item))
                {
                    hands = (Hands)item;
                    break;
                }
            }
        }

        pants = null;
        if(pantsId != -1)
        {
            List<Object> pantsObj =  new List<Object>( Resources.LoadAll("Outfits/Pants", typeof(ScriptableObject)) );
            foreach(Object item in pantsObj)
            {
                if(((Pants)item).outfitId == pantsId && GameController.CheckPurchased((ScriptableObject)item))
                {
                    pants = (Pants)item;
                    break;
                }
            }
        }

        legs = null;
        if(legsId != -1)
        {
            List<Object> legsObj =  new List<Object>( Resources.LoadAll("Outfits/Legs", typeof(ScriptableObject)) );
            foreach(Object item in legsObj)
            {
                if(((Legs)item).outfitId == legsId && GameController.CheckPurchased((ScriptableObject)item))
                {
                    legs = (Legs)item;
                    break;
                }
            }
        }
        if(legs == null)
        {
            List<Object> legsObj =  new List<Object>( Resources.LoadAll("Outfits/Legs", typeof(ScriptableObject)) );
            foreach(Object item in legsObj)
            {
                if(((Legs)item).outfitId == 0 && GameController.CheckPurchased((ScriptableObject)item))
                {
                    legs = (Legs)item;
                    break;
                }
            }
        }

        loadedItems = true;
        Debug.Log("All Equiped Items Loaded!");
    }
}
