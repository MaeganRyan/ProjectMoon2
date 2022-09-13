using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControlScript : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public static int moneyAmount = 100;
    //int isWoodSold;
    public GameObject wood;


    void Start(){
        moneyAmount = PlayerPrefs.GetInt("MoneyAmount");
        //isWoodSold = PlayerPrefs.GetInt ("IsWoodSold");

        //if (isWoodSold == 1)
            wood.SetActive (true);
        //else
            wood.SetActive (false);
    }

    void Update (){
        moneyText.text = "Money : " + moneyAmount.ToString() + "$";


    }

    public void gotoShop()
    {
        PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
        SceneManager.LoadScene ("ShopScene");
    }

    public void playGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
