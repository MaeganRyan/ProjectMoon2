using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopControlScript : MonoBehaviour
{
    public static int moneyAmount = 100;
    public static int woodAmount = 10;
    public static int botAmount = 10;
    public static int crystalAmount = 10;
    public static int poisonAmount = 10;
    public static int scrollAmount = 10;
    private int moneyTracker;
    private int woodTracker;
    private int crystalTracker;
    private int botTracker;
    private int poisonTracker;
    private int scrollTracker;

    public TextMeshProUGUI moneyAmountText;
    public TextMeshProUGUI woodAmountText;
    public TextMeshProUGUI botAmountText;
    public TextMeshProUGUI crystalAmountText;
    public TextMeshProUGUI poisonAmountText;
    public TextMeshProUGUI scrollAmountText;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    public GameObject[] objs;

    void OnEnable()
    {
    //Register Button Events
        button1.onClick.AddListener(() => buttonCallBack(button1));
        button2.onClick.AddListener(() => buttonCallBack(button2));
        button3.onClick.AddListener(() => buttonCallBack(button3));
        button4.onClick.AddListener(() => buttonCallBack(button4));
        button5.onClick.AddListener(() => buttonCallBack(button5));
    }

    void Start()
    {
        CraftingManager.winCount = 0;
        moneyTracker = moneyAmount;
        woodTracker = woodAmount;
        crystalTracker = crystalAmount;
        botTracker = botAmount;
        scrollTracker = scrollAmount;
        poisonTracker = poisonAmount;

        objs = GameObject.FindGameObjectsWithTag("BuyButton");

        //EnableButton();
    }

    void Update()
    {
        moneyAmountText.text = "Money: $ " + moneyAmount.ToString();

        woodAmountText.text = "In Inventory : " + woodAmount.ToString();
        botAmountText.text = "In Inventory : " + botAmount.ToString();
        crystalAmountText.text = "In Inventory : " + crystalAmount.ToString();
        poisonAmountText.text = "In Inventory : " + poisonAmount.ToString();
        scrollAmountText.text = "In Inventory : " + scrollAmount.ToString();

        EnableButton();       
    }

    private void buttonCallBack(Button buttonPressed)
{
    if (buttonPressed == button1)
    {
        moneyAmount -= 5;
        woodAmount += 1;
        Debug.Log("Clicked: " + button1.name);
    }

    if (buttonPressed == button2)
    {
        moneyAmount -= 10;
        botAmount += 1;
        Debug.Log("Clicked: " + button2.name);
    }

    if (buttonPressed == button3)
    {
        moneyAmount -= 15;
        crystalAmount += 1;
        Debug.Log("Clicked: " + button3.name);
    }


    if (buttonPressed == button4)
    {
        moneyAmount -= 20;
        poisonAmount += 1;
        Debug.Log("Clicked: " + button4.name);
    }

    if (buttonPressed == button5)
    {
        moneyAmount -= 25;
        scrollAmount += 1;
        Debug.Log("Clicked: " + button5.name);
    }
}

    public void EnableButton() 
    {
        objs = GameObject.FindGameObjectsWithTag("BuyButton");

        foreach (GameObject BuyButton in objs) 
        {

            if (moneyAmount >= 25)
            {
                GameObject.Find("BuyScroll").GetComponent<Button>().interactable = true;
                Debug.Log("Clicked: " + button5.name);
            }

            if (moneyAmount >= 20)
            {
                GameObject.Find("BuyPoison").GetComponent<Button>().interactable = true;
                Debug.Log("Clicked: " + button4.name);
            }

            if (moneyAmount >= 15)
            {
                GameObject.Find("BuyCrystal").GetComponent<Button>().interactable = true;
                Debug.Log("Clicked: " + button3.name);
            }

            if (moneyAmount >= 10)
            {
                GameObject.Find("BuyBot").GetComponent<Button>().interactable = true;
                Debug.Log("Clicked: " + button2.name);
            }

            if (moneyAmount >= 5)
            {
                GameObject.Find("BuyWood").GetComponent<Button>().interactable = true;
                Debug.Log("Clicked: " + button1.name);
            }

            else
            {
                BuyButton.GetComponent<Button>().interactable = false;
                Debug.Log("No Button");
            }
        }
    }

    public void exitShop()
    {
        //PlayerPrefs.SetInt ("MoneyAmount", moneyAmount);
        SceneManager.LoadScene("SampleScene");
    }

    public void resetPlayerPrefs()
    {
        moneyAmount = moneyTracker;
        woodAmount = woodTracker;
        crystalAmount = crystalTracker;
        botAmount = botTracker;
        scrollAmount = scrollTracker;
        poisonAmount = poisonTracker;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}