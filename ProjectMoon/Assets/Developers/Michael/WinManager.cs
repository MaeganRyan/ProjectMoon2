using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public GameObject[] products;

    public GameObject correctProduct;
    // Start is called before the first frame update
    
    void Start()
    {
        correctProduct = products[Random.Range(0,products.Length)];
    }

    public void WinButton()
    {
        if (correctProduct.activeInHierarchy)
        {
        SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("You Lose!");
        }
    }
}
