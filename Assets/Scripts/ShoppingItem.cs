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
    public Image itemImage2;
    public Button sellButton;
    public GameObject pantsIncludedIcon;
    public TMP_Text itemNameText;
    public ShoppingBuy buy;

    [Header("Background Selector")]
    public Image background;
    public Color normalColor;
    public Color selectedColor;

    public ShoppingListArea actualCategory => transform.parent.parent.gameObject.GetComponent<ShoppingListArea>();

    public async Task SetupItem(ScriptableObject itemObject)
    {
        item = itemObject;

        switch(item)
        {
            case Body: 
                itemInfo = ((Body)item).info; 
                if(!((Body)item).showPants) pantsIncludedIcon.SetActive(true); 
                break;
            case Head: itemInfo = ((Head)item).info; break;
            case Hands: itemInfo = ((Hands)item).info; break;
            case Pants: itemInfo = ((Pants)item).info; break;
            case Legs: itemInfo = ((Legs)item).info; break;
        }

        if(itemInfo != null)
        {
            itemImage.sprite = itemInfo.icon[0];
            if(itemInfo.icon.Count > 1) itemImage2.sprite = itemInfo.icon[1];
            itemNameText.text = itemInfo.outfitName;

            if(itemInfo.buyed)
            {
                buy.buyBlocker.SetActive(false);
                GetComponent<Button>().interactable = true;
                if(itemInfo.price > 0) sellButton.gameObject.SetActive(true);

                if(GameController.IsEquiped(item))
                    background.color = selectedColor;
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
            if(itemInfo.price > 0) sellButton.gameObject.SetActive(true);
            actualCategory.shoppingList.SetMessage("Successfully bought item!", result);
            actualCategory.shoppingList.UpdatePlayerCoins();
        }
        else
        {
            actualCategory.shoppingList.SetMessage("Failed to buy item!", result);
        }
    }

    public void SellItem()
    {
        GameController.SellItem(itemInfo.outfitName, itemInfo.price, ItemSellResponse);
    }

    void ItemSellResponse(bool success, string result)
    {
        if(success)
        {
            sellButton.gameObject.SetActive(false);
            buy.buyText.text = "BUY:\n"+itemInfo.price+" COINS";
            buy.buyBlocker.SetActive(true);
            GetComponent<Button>().interactable = false;
            actualCategory.shoppingList.UpdatePlayerCoins();
            actualCategory.shoppingList.UpdateSelectedItems();
        }
        else
        {
            actualCategory.shoppingList.SetMessage("Failed to sell item!", result);
        }
    }

    public void EquipItem()
    {
        GameController.EquipItem(item, ItemEquipResponse);
    }

    void ItemEquipResponse(bool success, string result)
    {
        if(success)
        {
            actualCategory.shoppingList.UpdateSelectedItems();
        }
        else
        {
            background.color = normalColor;
            actualCategory.shoppingList.SetMessage("Failed to buy item!", result);
        }
    }
}
