using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CustomerTracker : MonoBehaviour
{
    public static CustomerTracker Instance;

    // Reset this on main menu.
    public int tracker = 0;
    public int winAmount = 14;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            if (tracker == 0)
            {
                ShopControlScript.ResetGame();
            }

            tracker++;
        }
        else if (scene.name == "MainMenu")
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}
