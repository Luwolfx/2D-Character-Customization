using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

public static class GameController
{
    public static int playerCoins => PlayerPrefs.GetInt("PlayerCoins", 500);

    public static ItemsEquiped itemsEquiped = new ItemsEquiped();

    static async Task SaveItemsData()
    {
        string itemsData = JsonConvert.SerializeObject(itemsEquiped);

        if(!System.IO.File.Exists(Application.persistentDataPath + "/Save/"))
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Save/");

        await System.IO.File.WriteAllTextAsync(Application.persistentDataPath + "/Save/ItemsData.json", itemsData);

        Debug.Log("Saved Items Data!\nPath: "+Application.persistentDataPath + "/Save/ItemsData.json");
    }

    public static async Task LoadItemsData()
    {
        if(!System.IO.File.Exists(Application.persistentDataPath + "/Save/ItemsData.json"))
        {
            await itemsEquiped.LoadSavedItems();
            UpdateEquipedItems();
            return;
        }
    
        string itemsData = System.IO.File.ReadAllText(Application.persistentDataPath + "/Save/ItemsData.json");

        itemsEquiped = JsonConvert.DeserializeObject<ItemsEquiped>(itemsData);

        await itemsEquiped.LoadSavedItems();

        UpdateEquipedItems();
    }

    private static event Action<bool, string> onItemBuyEventResponse;
    public static void BuyItem(string itemName, int price, Action<bool, string> response)
    {
        onItemBuyEventResponse = response;
        if(PlayerPrefs.GetInt(itemName, 0) == 1)
            onItemBuyEventResponse?.Invoke(false, "You already have this item!");
        else if(playerCoins < price)
            onItemBuyEventResponse?.Invoke(false, "Not enough coins to buy item!");
        else
        {
            PlayerPrefs.SetInt("PlayerCoins", (playerCoins-price));
            PlayerPrefs.SetInt(itemName, 1);
            onItemBuyEventResponse?.Invoke(true, "Item "+itemName+" sucessfully bought!");
        }
    }

    private static event Action<bool, string> onItemSellEventResponse;
    public static async void SellItem(string itemName, int price, Action<bool, string> response)
    {
        onItemSellEventResponse = response;
        if(price == 0)
            onItemSellEventResponse?.Invoke(false, "Can't sell free items!");
        else if(PlayerPrefs.GetInt(itemName, 0) == 0)
            onItemSellEventResponse?.Invoke(false, "You don't have this item!");
        else
        {
            PlayerPrefs.SetInt("PlayerCoins", (playerCoins+price));
            PlayerPrefs.SetInt(itemName, 0);
            await LoadItemsData();
            onItemSellEventResponse?.Invoke(true, "Item "+itemName+" sucessfully sold!");
        }
    }

    private static event Action<bool, string> onItemEquipEventResponse;
    public static async void EquipItem(ScriptableObject item, Action<bool, string> response)
    {
        onItemEquipEventResponse = response;
        if(!CheckPurchased(item))
            onItemEquipEventResponse?.Invoke(false, "You don't own this item yet!");
        else if(IsEquiped(item))
            onItemEquipEventResponse?.Invoke(false, "This item is already equiped!");
        else
        {
            string itemName = "";
            switch(item)
            {
                case Head: 
                    itemsEquiped.head = (Head)item; 
                    itemName = itemsEquiped.head.info.outfitName;
                    itemsEquiped.headId = itemsEquiped.head.outfitId;
                    break;
                case Body: 
                    itemsEquiped.body = (Body)item; 
                    itemName = itemsEquiped.body.info.outfitName; 
                    itemsEquiped.bodyId = itemsEquiped.body.outfitId;
                    if(!itemsEquiped.body.showPants && itemsEquiped.pants)
                    {
                        itemsEquiped.pants = null;
                        itemsEquiped.pantsId = -1;
                    }
                    break;
                case Hands: 
                    itemsEquiped.hands = (Hands)item; 
                    itemName = itemsEquiped.hands.info.outfitName; 
                    itemsEquiped.handsId = itemsEquiped.hands.outfitId;
                    break;
                case Pants: 
                    itemsEquiped.pants = (Pants)item; 
                    itemName = itemsEquiped.pants.info.outfitName;
                    itemsEquiped.pantsId = itemsEquiped.pants.outfitId;
                    if(!itemsEquiped.body.showPants)
                    {
                        itemsEquiped.body = new Body()
                        { 
                            info = new OutfitInformation()
                            {
                                outfitName = "Default",
                            },
                            outfitId = 0,
                            showPants = true
                        };
                        itemsEquiped.bodyId = -1;
                    }
                    break;
                case Legs: 
                    itemsEquiped.legs = (Legs)item; 
                    itemName = itemsEquiped.legs.info.outfitName;
                    itemsEquiped.legsId = itemsEquiped.legs.outfitId;
                    break;
            }

            UpdateEquipedItems();
            await SaveItemsData();
            onItemEquipEventResponse?.Invoke(true, "Item "+itemName+" sucessfully equiped!");
        }

    }

    static void UpdateEquipedItems()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.UpdateEquipedItems();
    }

    public static bool CheckPurchased(ScriptableObject item)
    {
        switch(item)
        {
            case Head: if( ((Head)item).info.buyed ) return true; break;
            case Body: if( ((Body)item).info.buyed ) return true; break;
            case Hands: if( ((Hands)item).info.buyed ) return true; break;
            case Pants: if( ((Pants)item).info.buyed ) return true; break;
            case Legs: if( ((Legs)item).info.buyed ) return true; break;
        }
        return false;
    }

    public static bool IsEquiped(ScriptableObject item)
    {
        switch(item)
        {
            case Head:
                if(itemsEquiped.head == null)
                    return false;

                if( ((Head)item) == itemsEquiped.head ) 
                    return true; 
                break;

            case Body: 
                if(!itemsEquiped.body)
                    return false;

                if( ((Body)item) == itemsEquiped.body ) 
                    return true; 
                    break;

            case Hands: 
                if(!itemsEquiped.hands)
                    return false;

                if( ((Hands)item) == itemsEquiped.hands ) 
                    return true; 
                    break;

            case Pants: 
                if(!itemsEquiped.pants)
                    return false;

                if( ((Pants)item) == itemsEquiped.pants ) 
                    return true; 
                    break;

            case Legs: 
                if(!itemsEquiped.legs)
                    return false;

                if( ((Legs)item) == itemsEquiped.legs ) 
                    return true; 
                    break;
        }
        return false;
    }

}
