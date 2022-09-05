using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public TMP_Text coinsText;

    void Update()
    {
        coinsText.text = ""+GameController.playerCoins;
    }

    public void AddCoins(int coins)
    {
        PlayerPrefs.SetInt("PlayerCoins", GameController.playerCoins+coins);
    }
}
