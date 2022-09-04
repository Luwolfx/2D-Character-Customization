using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class ShoppingListArea : MonoBehaviour
{
    [Header("Item List Information")]
    public string areaName;
    public TMP_Text areaNameText;
    public ShoppingList shoppingList => transform.parent.parent.parent.parent.gameObject.GetComponent<ShoppingList>();

    [Header("Item List Objects")]
    public Transform areaListContent;
    public GameObject itemPrefab;


    public async Task SetupArea(string name)
    {
        areaName = name;
        areaNameText.text = name+":";

        List<Object> itemsObj =  new List<Object>( Resources.LoadAll("Outfits/"+areaName, typeof(ScriptableObject)) );

        int instantiatedNumber = 1;
        
        foreach(Object item in itemsObj)
        {
            GameObject instantiated = Instantiate(itemPrefab, areaListContent);

            instantiated.name = "Item_"+instantiatedNumber;
            await instantiated.GetComponent<ShoppingItem>().SetupItem((ScriptableObject) item);

            instantiatedNumber++;
        }
    }

    public void ResetSelected()
    {
        foreach(Transform item in areaListContent)
        {
            ShoppingItem shopItem = item.GetComponent<ShoppingItem>();
            if(GameController.IsEquiped(shopItem.item))
                shopItem.background.color = shopItem.selectedColor;
            else
                shopItem.background.color = shopItem.normalColor;
        }
    }
}
