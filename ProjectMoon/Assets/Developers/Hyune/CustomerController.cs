using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Customers
{ 
    Robot = 0,
    Gru,
    WingedDude,
    Joker,
    Gnome,
    Scientist,
    Pumpkin,
    Grass,
    FatDude
}

public class CustomerController : MonoBehaviour
{
    public static CustomerController Instance;
    public Customers currentCustomer;
    [SerializeField] private List<Sprite> characterSprites = new List<Sprite>();
    private Image characterSprite;
    private Animator anim;

    // Creates Singleton
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            currentCustomer = (Customers)Random.Range(0, 8);
            characterSprite = GetComponent<Image>();
            characterSprite.sprite = characterSprites[(int)currentCustomer];
            anim = GetComponent<Animator>();
        }
    }

    // Destroys Singleton on scene reload
    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayEnterAnimation();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayExitAnimation();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void PlayEnterAnimation()
    {
        anim.SetTrigger("Enter");
    }

    public void PlayExitAnimation()
    {
        anim.SetTrigger("Exit");
    }
}
