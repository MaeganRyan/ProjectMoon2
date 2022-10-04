using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnDialogueCast(Dialogue dia, int command);

public class DialogueCaster : MonoBehaviour
{
    public static event OnDialogueCast OnDialogueCast;
    //private bool inDialogue = false;

    public Speaker CurrentVillain;

    // Villain Name Tracker
    public List<AudioClip> VillainVoices = new List<AudioClip>();

    void Start()
    {
        DialogueSystem.OnDialogueFinish += OnDialogueFinish;
        Invoke("Cast",1f);
    }

    void OnDestroy()
    {
        DialogueSystem.OnDialogueFinish -= OnDialogueFinish;
    }

    void Cast()
    {
        Dialogue temp = (Dialogue)ScriptableObject.CreateInstance("Dialogue");
        Line templ = new Line();
        CurrentVillain = (Speaker)ScriptableObject.CreateInstance("Speaker");

        CurrentVillain._name = CustomerController.Instance.currentCustomer.ToString();

        for (int i = 0; i < 3; i++)
        {
            CurrentVillain.voiceLines.Add(VillainVoices[Random.Range(0, VillainVoices.Count - 1)]);
        }

        templ.speaker = CurrentVillain;
        //templ.line = "I need a " + CraftingManager.instance.correctRecipe.itemName;
        temp.voiceLines.Add(templ);
        OnDialogueCast?.Invoke(temp, 0);
        //inDialogue = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
       if (Input.GetKeyDown(KeyCode.P) && !inDialogue)
       {
        dialogueCast ?. Invoke(test);
        inDialogue = true;
       } 
       */
    }

    void OnDialogueFinish()
    {
        //inDialogue = false;
    }
}
