using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

public static class GameController
{
    public static int playerCoins => PlayerPrefs.GetInt("PlayerCoins", 500);

    public static ItemsEquiped itemsEquiped;

    static async Task SaveItemsData()
    {
        Debug.Log("Saving Items Data...");
        string itemsData = JsonConvert.SerializeObject(itemsEquiped);

        if(!System.IO.File.Exists(Application.persistentDataPath + "/Save/"))
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Save/");

        await System.IO.File.WriteAllTextAsync(Application.persistentDataPath + "/Save/ItemsData.json", itemsData);

        Debug.Log("Saved Items Data!\nPath: "+Application.persistentDataPath + "/Save/ItemsData.json");
    }

    static async Task LoadItemsData()
    {
        if(!System.IO.File.Exists(Application.persistentDataPath + "/Save/ItemsData.json"))
            return;

        Debug.Log("Save Found!    | Loading Items Data...");
    
        string itemsData = System.IO.File.ReadAllText(Application.persistentDataPath + "/Save/ItemsData.json");

        itemsEquiped = JsonConvert.DeserializeObject<ItemsEquiped>(itemsData);

        Debug.Log("Items Data Loaded!");
    }

    private static event Action<bool, string> onItemBuyEventResponse;
    public static void BuyItem(string itemName, int price, Action<bool, string> response)
    {
        onItemBuyEventResponse = response;
        if(PlayerPrefs.GetInt(itemName, 0) == 1)
        {
            onItemBuyEventResponse?.Invoke(false, "You already have this item!");
        }
        if(playerCoins < price)
        {
            onItemBuyEventResponse?.Invoke(false, "Not enough coins to buy item!");
        }
        else
        {
            PlayerPrefs.SetInt("PlayerCoins", (playerCoins-price));
            PlayerPrefs.SetInt(itemName, 1);
            onItemBuyEventResponse?.Invoke(true, "Item "+itemName+" sucessfully bought!");
        }
    }

}
