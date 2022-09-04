using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoppingList : MonoBehaviour
{
    [Header("Shoppping Objects")]
    public Transform shoppingContent;
    public GameObject shoppingAreaPrefab;
    public TMP_Text coinsText;

    public ItemsEquiped equiped = GameController.itemsEquiped;
    public bool updateEquiped;

    [Header("Message Popup")]
    public GameObject messagePopup;
    public TMP_Text messagePopupTitleText;
    public TMP_Text messagePopupText;

    [Header("Shopping Item Types")]
    public List<string> itemTypes = new List<string>();

    void Start()
    {
        SetupShopping();
    }

    private void Update() 
    {
        if(updateEquiped)
        {
            updateEquiped = false;
            equiped = GameController.itemsEquiped;
        }   
    }

    async void SetupShopping()
    {
        UpdatePlayerCoins();
        await GameController.LoadItemsData();

        Debug.Log("Shop setup in progress");

        int instantiatedNumber = 1;
        foreach(string type in itemTypes)
        {
            GameObject instantiated = Instantiate(shoppingAreaPrefab, shoppingContent);
            
            instantiated.name = "Area_"+instantiatedNumber;
            await instantiated.GetComponent<ShoppingListArea>().SetupArea(type);

            instantiatedNumber++;
        }
    }

    public void UpdatePlayerCoins()
    {
        coinsText.text = "" + GameController.playerCoins;
    }

    public void SetMessage(string title, string message)
    {
        messagePopup.SetActive(true);
        messagePopupTitleText.text = title;
        messagePopupText.text = message;
    }

    public void UpdateSelectedItems()
    {
        foreach(Transform itemCategory in shoppingContent)
        {
            itemCategory.GetComponent<ShoppingListArea>().ResetSelected();
        }
    }
}
