using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DialogueCast(Dialogue dia);

public class DialogueCaster : MonoBehaviour
{
    public static event DialogueCast dialogueCast;
    public Dialogue test;
    private bool inDialogue = false;

    void Start()
    {
        DialogueSystem.dialogueDoneCaster += OnDialogueFinish;
    }

    void OnDestroy()
    {
        DialogueSystem.dialogueDoneCaster -= OnDialogueFinish;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.P) && !inDialogue)
       {
        dialogueCast ?. Invoke(test);
        inDialogue = true;
       } 
    }

    void OnDialogueFinish()
    {
        inDialogue = false;
    }
}
