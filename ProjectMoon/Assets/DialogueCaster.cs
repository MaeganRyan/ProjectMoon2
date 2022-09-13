using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void DialogueCast(Dialogue dia);

public class DialogueCaster : MonoBehaviour
{
    public static event DialogueCast dialogueCast;
    private bool inDialogue = false;
    public Speaker test;
    public Dialogue current;
    void Start()
    {
        DialogueSystem.dialogueDoneCaster += OnDialogueFinish;
        Invoke("Cast",1f);
    }

    void OnDestroy()
    {
        DialogueSystem.dialogueDoneCaster -= OnDialogueFinish;
    }

    void Cast()
    {
        Dialogue temp = (Dialogue)ScriptableObject.CreateInstance("Dialogue");
        Line templ = new Line();
        templ.speaker = test;
        templ.line = "I need a " + CraftingManager.instance.correctRecipe.itemName;
        temp.voiceLines.Add(templ);
        current = temp;
        dialogueCast ?. Invoke(temp);
        inDialogue = true;
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
        inDialogue = false;
    }
}
