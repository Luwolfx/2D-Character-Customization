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

    [Header("Item List Objects")]
    public Transform areaListContent;
    public GameObject itemPrefab;

    [Header("Items found")]
    public List<ScriptableObject> items = new List<ScriptableObject>();


    public async Task SetupArea(string name)
    {
        areaName = name;
        areaNameText.text = name+":";

        print("SEARCHING > Outfits/"+areaName);

        List<Object> itemsObj =  new List<Object>( Resources.LoadAll("Outfits/"+areaName, typeof(ScriptableObject)) );
        print("Objects found: "+itemsObj.Count);

        foreach(Object item in itemsObj)
        {
            items.Add((ScriptableObject) item );
        }

        int instantiatedNumber = 1;

        foreach(ScriptableObject item in items)
        {
            GameObject instantiated = Instantiate(itemPrefab, areaListContent);

            instantiated.name = "Item_"+instantiatedNumber;
            await instantiated.GetComponent<ShoppingItem>().SetupItem(item);

            instantiatedNumber++;
        }
    }
}
