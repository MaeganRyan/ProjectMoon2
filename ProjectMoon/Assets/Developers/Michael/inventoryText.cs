using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventoryText : MonoBehaviour
{
    public TextMeshProUGUI moneyAmountText;
    public TextMeshProUGUI woodAmountText;
    public TextMeshProUGUI botAmountText;
    public TextMeshProUGUI crystalAmountText;
    public TextMeshProUGUI poisonAmountText;
    public TextMeshProUGUI scrollAmountText;
    public TextMeshProUGUI soulAmountText;
    public TextMeshProUGUI stoneAmountText;

    // Update is called once per frame
    void Update()
    {
        moneyAmountText.text = "Bank Account: \n$ " + ShopControlScript.moneyAmount.ToString();
        woodAmountText.text = "x " + ShopControlScript.woodAmount.ToString();
        botAmountText.text = "x " + ShopControlScript.botAmount.ToString();
        crystalAmountText.text = "x " + ShopControlScript.crystalAmount.ToString();
        poisonAmountText.text = "x " + ShopControlScript.poisonAmount.ToString();
        scrollAmountText.text = "x " + ShopControlScript.scrollAmount.ToString();
        soulAmountText.text = "x " + ShopControlScript.soulAmount.ToString();
        stoneAmountText.text = "x " + ShopControlScript.stoneAmount.ToString();
    }
}
