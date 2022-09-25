using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craftingInteractableManager : MonoBehaviour
{
    public GameObject[] craftingResources;

    // Update is called once per frame
    void Update()
    {
       if(ShopControlScript.woodAmount <= 0)
       {
        craftingResources[0].SetActive(false);
       } 
       else
       {
        craftingResources[0].SetActive(true);
       }

       if(ShopControlScript.crystalAmount <= 0)
       {
        craftingResources[1].SetActive(false);
       } 
       else
       {
        craftingResources[1].SetActive(true);
       }

       if(ShopControlScript.botAmount <= 0)
       {
        craftingResources[2].SetActive(false);
       } 
       else
       {
        craftingResources[2].SetActive(true);
       }

       if(ShopControlScript.scrollAmount <= 0)
       {
        craftingResources[3].SetActive(false);
       } 
       else
       {
        craftingResources[3].SetActive(true);
       }

       if(ShopControlScript.poisonAmount <= 0)
       {
        craftingResources[4].SetActive(false);
       } 
       else
       {
        craftingResources[4].SetActive(true);
       }
    }
}
