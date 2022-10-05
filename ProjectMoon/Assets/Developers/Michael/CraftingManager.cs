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

    // Slot tracker
    private int tracker = 0;
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

    public AudioSource _audio;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    bool submitted = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            //button.SetActive(false);
            _audio = GetComponent<AudioSource>();
            DialogueSystem.OnDialogueFinish += OnDialogueFinish;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (CustomerTracker.Instance.tracker < CustomerTracker.Instance.winAmount)
        {
            if (CustomerController.Instance.currentCustomer._name == Customers.Gru)
            {
                correctRecipe = recipeResults[3];
            }

            else
            {
                correctRecipe = recipeResults[Random.Range(0, recipeResults.Length - 1)];
            }
        }
        else
        {
            correctRecipe = recipeResults[7];
        }
    }

    private void OnDestroy()
    {
        instance = null;
        DialogueSystem.OnDialogueFinish -= OnDialogueFinish;
    }

    private void Update()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
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

        if (Input.GetMouseButtonUp(0) || Gamepad.current.aButton.wasPressedThisFrame)
        {
            if (currentItem != null && tracker < 4)
            {
                //customCursor.gameObject.SetActive(false);
                Slot currentSlot = null;
                //float shortestDistance = float.MaxValue;

                //foreach(Slot slot in craftingSlots)
                //{
                //    float dist = Vector2.Distance(GameObject.Find("CustomCursor").transform.position, /*Input.mousePosition,*/ slot.transform.position);

                //    if(dist < shortestDistance)
                //    {
                //        shortestDistance = dist;
                //        currentSlot = slot;
                //    }
                //}

                // Gets the tracked slot
                currentSlot = craftingSlots[tracker];

                if (CheckIfHasItem(currentItem))
                {
                    tracker++;

                    currentSlot.gameObject.SetActive(true);
                    currentSlot.gameObject.transform.localScale = new Vector2(0.75f, 0.75f);
                    currentSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                    currentSlot.item = currentItem;
                    itemList[currentSlot.index] = currentItem;
                    currentItem = null;
                    customCursor.gameObject.SetActive(true);
                    customCursor.GetComponent<Image>().sprite = cursorSprite;

                    CheckForCreatedRecipes();
                }
            }
        }
    }

    public static void ResetGame()
    {
        loseCount = 0;
    }

    bool CheckIfHasItem(Item item)
    {
        if (item.tag == "Wood" && ShopControlScript.woodAmount > 0)
        {
            ShopControlScript.woodAmount--;
            return true;
        }

        if (item.tag == "Crystal" && ShopControlScript.crystalAmount > 0)
        {
            ShopControlScript.crystalAmount--;
            return true;
        }

        if (item.tag == "Bot" && ShopControlScript.botAmount > 0)
        {
            ShopControlScript.botAmount--;
            return true;
        }

        if (item.tag == "Scroll" && ShopControlScript.scrollAmount > 0)
        {
            ShopControlScript.scrollAmount--;
            return true;
        }

        if (item.tag == "Poison" && ShopControlScript.poisonAmount > 0)
        {
            ShopControlScript.poisonAmount--;
            return true;
        }

        if (item.tag == "Stone" && ShopControlScript.stoneAmount > 0)
        {
            ShopControlScript.stoneAmount--;
            return true;
        }

        if (item.tag == "Soul" && ShopControlScript.soulAmount > 0)
        {
            ShopControlScript.soulAmount--;
            return true;
        }

        if (item.tag == "Money" && ShopControlScript.moneyAmount > 0)
        {
            ShopControlScript.moneyAmount -= 100;
            return true;
        }

        return false;
    }

    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (Item item in itemList)
        {
            if (item != null)
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
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].GetComponent<Image>().sprite;
                resultSlot.item = recipeResults[i];
                button.SetActive(true);
            }
        }
    }

    public void OnMouseDownItem(Item item)
    {
        if (tracker < 4)
        {
            if (currentItem == null)
            {
                currentItem = item;
                //customCursor.gameObject.SetActive(true);
                //customCursor.sprite = currentItem.GetComponent<Image>().sprite;
                Debug.Log("Is this working");
            }

            if (item.tag == "Wood" && ShopControlScript.woodAmount > 0)
            {
                Debug.Log("Wood!");
                //ShopControlScript.woodAmount--;
                Debug.Log(ShopControlScript.woodAmount);
            }

            if (item.tag == "Crystal" && ShopControlScript.crystalAmount > 0)
            {
                Debug.Log("Crystal!");
                //ShopControlScript.crystalAmount--;
                Debug.Log(ShopControlScript.woodAmount);
            }

            if (item.tag == "Bot" && ShopControlScript.botAmount > 0)
            {
                Debug.Log("Bot");
                //ShopControlScript.botAmount--;
                Debug.Log(ShopControlScript.woodAmount);
            }

            if (item.tag == "Scroll" && ShopControlScript.scrollAmount > 0)
            {
                Debug.Log("Scroll!");
                //ShopControlScript.scrollAmount--;
                Debug.Log(ShopControlScript.scrollAmount);
            }

            if (item.tag == "Poison" && ShopControlScript.poisonAmount > 0)
            {
                Debug.Log("Poison!");
                //ShopControlScript.poisonAmount--;
                Debug.Log(ShopControlScript.poisonAmount);
            }

            if (item.tag == "Stone" && ShopControlScript.stoneAmount > 0)
            {
                Debug.Log("Stone!");
                //ShopControlScript.stoneAmount--;
                Debug.Log(ShopControlScript.stoneAmount);
            }

            if (item.tag == "Soul" && ShopControlScript.soulAmount > 0)
            {
                Debug.Log("Soul!");
                //ShopControlScript.soulAmount--;
                Debug.Log(ShopControlScript.soulAmount);
            }

            if (item.tag == "Money" && ShopControlScript.moneyAmount > 0)
            {
                Debug.Log("Money!");
                //ShopControlScript.moneyAmount -= 100;
                Debug.Log(ShopControlScript.moneyAmount);
            }
        }
    }

    public void OnClickSlot(Slot slot)
    {
        if (slot.item.tag == "Wood")
        {
            ShopControlScript.woodAmount++;
            Debug.Log(ShopControlScript.woodAmount);
        }

        if (slot.item.tag == "Crystal")
        {
            ShopControlScript.crystalAmount++;
            Debug.Log(ShopControlScript.crystalAmount);
        }

        if (slot.item.tag == "Bot")
        {
            ShopControlScript.botAmount++;
            Debug.Log(ShopControlScript.botAmount);
        }

        if (slot.item.tag == "Scroll")
        {
            ShopControlScript.scrollAmount++;
            Debug.Log(ShopControlScript.scrollAmount);
        }

        if (slot.item.tag == "Poison")
        {
            ShopControlScript.poisonAmount++;
            Debug.Log(ShopControlScript.poisonAmount);
        }

        if (slot.item.tag == "Stone")
        {
            ShopControlScript.poisonAmount++;
            Debug.Log(ShopControlScript.poisonAmount);
        }

        if (slot.item.tag == "Soul")
        {
            ShopControlScript.poisonAmount++;
            Debug.Log(ShopControlScript.poisonAmount);
        }

        if (slot.item.tag == "Money")
        {
            Debug.Log("Money!");
            ShopControlScript.moneyAmount += 100;
            Debug.Log(ShopControlScript.moneyAmount);
        }

        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
        tracker--;
    }


    public void WinButton()
    {
        StartCoroutine(WinButtonCoroutine());
    }

    public IEnumerator WinButtonCoroutine()
    {
        if (!submitted)
        {
            submitted = true;

            // Important Case: Lose
            if (loseCount == 3)
            {
                OnItemSubmit?.Invoke(1);
                _audio.PlayOneShot(incorrectSound);

                yield return (WaitForDialogueCallback());

                SceneManager.LoadScene("LoseScene");
                yield break;
            }

            // Important Case: Win
            if (correctRecipe == resultSlot.item && CustomerTracker.Instance.tracker >= CustomerTracker.Instance.winAmount)
            {
                OnItemSubmit?.Invoke(0);
                _audio.PlayOneShot(correctSound);

                Debug.LogError("PLAYER WON");

                yield return (WaitForDialogueCallback());

                SceneManager.LoadScene("WinScene");
                yield break;
            }

            // Important Case: Shop
            if (correctRecipe == resultSlot.item && winCount == 2 && CustomerTracker.Instance.tracker < CustomerTracker.Instance.winAmount)
            {
                OnItemSubmit?.Invoke(0);
                _audio.PlayOneShot(correctSound);

                Debug.LogError("PLAYER correct");
                yield return (WaitForDialogueCallback());

                ShopControlScript.moneyAmount += 100;
                SceneManager.LoadScene("ShopScene");
                yield break;
            }

            // Normal Check
            if (correctRecipe == resultSlot.item)
            {
                // Player Successfully Served the Customer

                OnItemSubmit?.Invoke(0);
                _audio.PlayOneShot(correctSound);
                yield return (WaitForDialogueCallback());

                winCount++;
                ShopControlScript.moneyAmount += 100;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                yield break;
            }

            else
            {
                // Player made an error
                OnItemSubmit?.Invoke(2);
                loseCount++;
                _audio.PlayOneShot(incorrectSound);

                Debug.LogError(loseCount);
                submitted = false;
            }
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
