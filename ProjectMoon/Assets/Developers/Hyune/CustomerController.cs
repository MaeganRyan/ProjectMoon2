using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public delegate void OnCustomerEnterComplete();

public class CustomerController : MonoBehaviour
{
    public static CustomerController Instance;
    public static event OnCustomerEnterComplete onCustomerEnterComplete;

    [SerializeField] private List<Customer> customers = new List<Customer>();
    [SerializeField]private Customer landlord;

    [HideInInspector] public Customer currentCustomer;
    private Image characterSprite;

    private Animator anim;


    // Start Day Loop
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            if (CustomerTracker.Instance.tracker < CustomerTracker.Instance.winAmount)
            {
                //currentCustomer = customers[2];
                currentCustomer = customers[Random.Range(0, customers.Count - 1)];
            }
            else
            {
                currentCustomer = landlord;
            }

            characterSprite = GetComponent<Image>();
            characterSprite.sprite = currentCustomer.sprite;
            anim = GetComponent<Animator>();
            PlayEnterAnimation();
            CraftingManager.OnItemSubmit += PlayExitAnimation;
        }
    }

    // Destroys Singleton on scene reload
    private void OnDestroy()
    {
        Instance = null;
        CraftingManager.OnItemSubmit -= PlayExitAnimation;
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    PlayEnterAnimation();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    PlayExitAnimation();
        //}

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnCustomerEnterComplete()
    {
        onCustomerEnterComplete?.Invoke();
    }

    public void PlayEnterAnimation()
    {
        anim.SetTrigger("Enter");
    }

    public void PlayExitAnimation(int command)
    {
        if (command == 1 || command == 0)
        { 
            anim.SetTrigger("Exit");
        }
    }
}
