using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ShoppingItem : MonoBehaviour
{
    [Header("Item Objects")]
    public ScriptableObject item;
    public OutfitInformation itemInfo;
    public Image itemImage;
    public TMP_Text itemNameText;
    public ShoppingBuy buy;

    public ShoppingListArea actualCategory => transform.parent.parent.gameObject.GetComponent<ShoppingListArea>();

    public async Task SetupItem(ScriptableObject itemObject)
    {
        item = itemObject;

        switch(item)
        {
            case Body: itemInfo = ((Body)item).info; break;
            case Head: itemInfo = ((Head)item).info; break;
            case Hands: itemInfo = ((Hands)item).info; break;
            case Pants: itemInfo = ((Pants)item).info; break;
            case Legs: itemInfo = ((Legs)item).info; break;
        }

        if(itemInfo != null)
        {
            itemImage.sprite = itemInfo.icon[0];
            itemNameText.text = itemInfo.outfitName;

            if(itemInfo.buyed)
            {
                buy.buyBlocker.SetActive(false);
                GetComponent<Button>().interactable = true;
            }
            else
            {
                buy.buyText.text = "BUY:\n"+itemInfo.price+" COINS";
            }
        }
        else
            Destroy(gameObject);

    }

    public void BuyItem()
    {
        GameController.BuyItem(itemInfo.outfitName, itemInfo.price, ItemBuyResponse);
    }

    void ItemBuyResponse(bool success, string result)
    {
        if(success)
        {
            buy.buyBlocker.SetActive(false);
            GetComponent<Button>().interactable = true;
            actualCategory.shoppingList.SetMessage("Successfully bought item!", result);
            actualCategory.shoppingList.UpdatePlayerCoins();
        }
        else
        {
            actualCategory.shoppingList.SetMessage("Failed to buy item!", result);
        }
    }
}
