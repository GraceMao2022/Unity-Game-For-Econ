using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; ////
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int itemPrice;
    public GameObject item;
    public TextMeshProUGUI priceText;

    public void BuyItem()
    {
        if(itemPrice <= GlobalManager.score && item.tag == "SpeedBoost" && !GlobalManager.unlockSpeedBoost)
        {
            GlobalManager.unlockSpeedBoost = true;
            ///////unlock it immediately -------- tell user how to double jump
            ScoreManager.instance.ChangeScore(-itemPrice);
            priceText.text = "Bought";
        }
        else if(itemPrice <= GlobalManager.score && item.tag == "DoubleJump" && !GlobalManager.unlockDoubleJump)
        {
            GlobalManager.unlockDoubleJump = true;
            ///////unlock it immediately -------- tell user how to double jump
            ScoreManager.instance.ChangeScore(-itemPrice);
            priceText.text = "Bought";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(itemPrice > GlobalManager.score)
        {
            if(item.tag == "SpeedBoost" && !GlobalManager.unlockSpeedBoost)
                priceText.text = itemPrice.ToString() + "\nNot Enough Gems";
            else if(item.tag == "DoubleJump" && !GlobalManager.unlockDoubleJump)
                priceText.text = itemPrice.ToString() + "\nNot Enough Gems";
        }
        if(item.tag == "SpeedBoost" && GlobalManager.unlockSpeedBoost)
        {
            priceText.text = "Bought";
        }
        else if(item.tag == "DoubleJump" && GlobalManager.unlockDoubleJump)
        {
            priceText.text = "Bought";
        }
    }
}
