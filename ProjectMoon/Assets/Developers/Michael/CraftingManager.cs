using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public delegate void OnItemSubmit(int command);

public class CraftingManager : MonoBehaviour
{

    public static event OnItemSubmit OnItemSubmit;

    public static CraftingManager instance;
    private Item currentItem;
    public Image customCursor;
    public GameObject recipeBook;
    private bool isPaused;

    private bool wait;

    public Slot[] craftingSlots;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public Item correctRecipe;
    public Slot resultSlot;
    public static int winCount;
    public GameObject button;
    private Vector2 scaleChange;

    public Sprite cursorSprite;

    public Item wood;

    public GameObject loseText;

    public static int levelCount = 0;

    public static int loseCount = 0;

    public AudioSource audio;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    void Awake()
    {
         if (instance == null)
        {
            instance = this;
        }
        else
        {
        Destroy(this.gameObject);
        }
        if (levelCount < 5)
        {
        correctRecipe = recipeResults[Random.Range(0,recipeResults.Length-1)];
        }
        else
        {
            correctRecipe = recipeResults[7];
        }
    }
    void Start()
    {
        loseText.SetActive(false);
        button.SetActive(false);

        DialogueSystem.OnDialogueFinish += OnDialogueFinish;
    }

    private void OnDestroy()
    {
        DialogueSystem.OnDialogueFinish -= OnDialogueFinish;
    }

    private void Update()
    {

        if(loseCount == 3)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        if (isPaused == true)
        {
            recipeBook.SetActive(true);
        }
        else
        {
            recipeBook.SetActive(false);
        }

        if(Input.GetMouseButtonUp(0) || Gamepad.current.aButton.wasPressedThisFrame)
        {
            if(currentItem != null)
            {
                //customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;

                foreach(Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(GameObject.Find("CustomCursor").transform.position, /*Input.mousePosition,*/ slot.transform.position);

                    if(dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.gameObject.transform.localScale = new Vector2(0.75f, 0.75f);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                itemList[nearestSlot.index] = currentItem;
                currentItem = null;
                customCursor.gameObject.SetActive(true);
                customCursor.GetComponent<Image>().sprite = cursorSprite;

                CheckForCreatedRecipes();
                
            }
        }
    }

    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach(Item item in itemList)
        {
            if(item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
                button.SetActive(true);
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        if (slot.item.tag == "Wood")
        {
            ShopControlScript.woodAmount ++;
            Debug.Log(ShopControlScript.woodAmount);
        }

        if (slot.item.tag == "Crystal")
        {
            ShopControlScript.crystalAmount ++;
            Debug.Log(ShopControlScript.crystalAmount);
        }

        if (slot.item.tag == "Bot")
        {
            ShopControlScript.botAmount ++;
            Debug.Log(ShopControlScript.botAmount);
        }

        if (slot.item.tag == "Scroll")
        {
            ShopControlScript.scrollAmount ++;
            Debug.Log(ShopControlScript.scrollAmount);
        }

        if (slot.item.tag == "Poison")
        {
            ShopControlScript.poisonAmount ++;
            Debug.Log(ShopControlScript.poisonAmount);
        }

        if (slot.item.tag == "Stone")
        {
            ShopControlScript.poisonAmount ++;
            Debug.Log(ShopControlScript.poisonAmount);
        }

        if (slot.item.tag == "Soul")
        {
            ShopControlScript.poisonAmount ++;
            Debug.Log(ShopControlScript.poisonAmount);
        }
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }

    public void OnMouseDownItem(Item item)
    {

        if(currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
            Debug.Log("Is this working");
        }

        if (item.tag == "Wood")
        {
            Debug.Log("Wood!");
            ShopControlScript.woodAmount --;
            Debug.Log(ShopControlScript.woodAmount);
        }

        if (item.tag == "Crystal")
        {
            Debug.Log("Crystal!");
            ShopControlScript.crystalAmount --;
            Debug.Log(ShopControlScript.woodAmount);
        }

        if (item.tag == "Bot")
        {
            Debug.Log("Bot");
            ShopControlScript.botAmount --;
            Debug.Log(ShopControlScript.woodAmount);
        }

        if (item.tag == "Scroll")
        {
            Debug.Log("Scroll!");
            ShopControlScript.scrollAmount --;
            Debug.Log(ShopControlScript.scrollAmount);
        }

        if (item.tag == "Poison")
        {
            Debug.Log("Poison!");
            ShopControlScript.poisonAmount --;
            Debug.Log(ShopControlScript.poisonAmount);
        }

        if (item.tag == "Stone")
        {
            Debug.Log("Stone!");
            ShopControlScript.stoneAmount --;
            Debug.Log(ShopControlScript.stoneAmount);
        }

        if (item.tag == "Soul")
        {
            Debug.Log("Soul!");
            ShopControlScript.soulAmount --;
            Debug.Log(ShopControlScript.soulAmount);
        }


    }

    public void WinButton ()
    {
        StartCoroutine(WinButtonCoroutine());
    }

    public IEnumerator WinButtonCoroutine()
    {
        if (correctRecipe == resultSlot.item && winCount < 2)
        {
            // Player Won

            OnItemSubmit?.Invoke(0);
            audio.PlayOneShot(correctSound);
            yield return (WaitForDialogueCallback());

            winCount++;
            ShopControlScript.moneyAmount += 100;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (correctRecipe == resultSlot.item && winCount == 2 && levelCount < 5)
        {
            OnItemSubmit?.Invoke(0);
            yield return (WaitForDialogueCallback());

            ShopControlScript.moneyAmount += 100;
            SceneManager.LoadScene("ShopScene");
        }
        else if (correctRecipe == resultSlot.item && levelCount > 5 && ShopControlScript.moneyAmount > 100)
        {
            OnItemSubmit?.Invoke(0);
            yield return (WaitForDialogueCallback());

            SceneManager.LoadScene("WinScene");
        }
        else
        {
            // Player Lost
            OnItemSubmit?.Invoke(1);
            yield return (WaitForDialogueCallback());

            loseCount++;
        }

    }
    public void OnDialogueFinish()
    {
        Debug.Log("Received Dialogue Callback");
        wait = false;
    }

    public IEnumerator WaitForDialogueCallback()
    {
        wait = true;
        while (wait)
        {
            yield return null;
        }
    }
}
