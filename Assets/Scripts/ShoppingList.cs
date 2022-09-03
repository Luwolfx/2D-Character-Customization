using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    [Header("Shoppping Objects")]
    public Transform shoppingContent;
    public GameObject shoppingAreaPrefab;

    [Header("Shopping Item Types")]
    public List<string> itemTypes = new List<string>();


    void Start()
    {
        SetupShopping();
    }

    async void SetupShopping()
    {
        int instantiatedNumber = 1;
        foreach(string type in itemTypes)
        {
            GameObject instantiated = Instantiate(shoppingAreaPrefab, shoppingContent);
            
            instantiated.name = "Area_"+instantiatedNumber;
            await instantiated.GetComponent<ShoppingListArea>().SetupArea(type);

            instantiatedNumber++;
        }
    }
}
