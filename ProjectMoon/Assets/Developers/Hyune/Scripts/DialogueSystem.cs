using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public delegate void DialogueCallback();

public class DialogueSystem : MonoBehaviour
{
    public static event DialogueCallback dialogueDoneCaster;

    // Accesses Dialogue S.O.
    public Dialogue currentDialogue;
    public Color dialogueColor;

    string currentLine;

    public GameObject textbox;
    public TMP_Text textComponent;

    private AudioSource voice;

    public float waitTime = 0.02f;

    public bool debug;
    public bool inDialogue;
    public bool acceptingInput;

    private void Start()
    {
        DialogueCaster.dialogueCast += OnDialogueCast;

        voice = GetComponent<AudioSource>();
        textbox.SetActive(false);
    }

    private void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.E) && !inDialogue)
        {
            ShowDialogue(currentDialogue);
        }
    }

    private void OnDestroy()
    {
        DialogueCaster.dialogueCast -= OnDialogueCast;
    }

    private void OnDialogueCast(Dialogue newDialogue)
    {
        ShowDialogue(newDialogue);
    }

    void ShowDialogue(Dialogue newDialogue)
    {
        StartCoroutine(DialogueWrapper(newDialogue));
    }

    // boss coroutine that tracks the completion of the entire task
    public IEnumerator DialogueWrapper(Dialogue dialogue)
    {
        inDialogue = true;

        textbox.SetActive(true);

        // load the new dialogue
        yield return NewDialogue(dialogue);

        // for each line in the dialogue, print it then wait for input
        for (int i = 0; i < dialogue.voiceLines.Count; i++)
        {
            yield return PrintDialogue(currentDialogue.voiceLines[i]);
            yield return WaitForInput();
        }

        // exit the dialogue loop
        yield return Exit();

        textbox.SetActive(false);

        dialogueDoneCaster?.Invoke();
        inDialogue = false;
    }

    public IEnumerator PrintDialogue(Line newLine)
    {
        // set it equal to currentLine
        if (newLine.speaker._name != "")
        {
            currentLine = newLine.speaker._name + ": " + newLine.line;
        }
        else
        {
            currentLine = newLine.line;
        }

        textComponent.textInfo.textComponent.text = currentLine;

        textComponent.color = Color.clear;

        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        // Define color for the speaker and :
        for (int i = 0; i < newLine.speaker._name.Length; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

            for (int j = 0; j < 4; ++j)
            {
                var index = charInfo.vertexIndex + j;
                var orig = meshInfo.vertices[index];

                // modifier here
                meshInfo.colors32[index] = dialogueColor;
            }

            // Set the color for all
            for (int l = 0; l < textInfo.meshInfo.Length; ++l)
            {
                var prosMeshInfo = textInfo.meshInfo[l];
                prosMeshInfo.mesh.vertices = prosMeshInfo.vertices;
                prosMeshInfo.mesh.colors32 = prosMeshInfo.colors32;
                textComponent.UpdateGeometry(prosMeshInfo.mesh, l);
            }
        }

        // Define color for all
        for (int i = newLine.speaker._name.Length; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

            for (int j = 0; j < 4; ++j)
            {
                var index = charInfo.vertexIndex + j;
                var orig = meshInfo.vertices[index];

                // modifier here
                meshInfo.colors32[index] = dialogueColor;
            }

            // Set the color for all
            for (int l = 0; l < textInfo.meshInfo.Length; ++l)
            {
                var prosMeshInfo = textInfo.meshInfo[l];
                prosMeshInfo.mesh.vertices = prosMeshInfo.vertices;
                prosMeshInfo.mesh.colors32 = prosMeshInfo.colors32;
                textComponent.UpdateGeometry(prosMeshInfo.mesh, l);
            }

            voice.Stop();

            // Play Audio
            //if (i / 2 == 0)
            //{
            //    voice.PlayOneShot(newLine.voiceLines[GenerateRandomNumber(newLine.voiceLines.Length)]);
            //}

            if (textInfo.characterInfo[i].character != ' ')
            {
                voice.PlayOneShot(newLine.speaker.voiceLines[GenerateRandomNumber(newLine.speaker.voiceLines.Count)]);
            } 

            yield return new WaitForSeconds(waitTime);
            yield return null;
        }
    }

    public IEnumerator NewDialogue(Dialogue dialogue)
    {
        // Dialogue Start
        textComponent.text = "";
        inDialogue = true;
        currentDialogue = dialogue;
        yield return null;
    }

    public IEnumerator WaitForInput()
    {
        bool userInput = false;
        while (!userInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                acceptingInput = false;
                userInput = true;
            }

            yield return null;
        }
    }

    public IEnumerator Exit()
    {
        acceptingInput = false;
        yield return null;
    }

    public int GenerateRandomNumber(int count)
    {
        int temp = Random.Range(0, count - 1);
        return temp % 8;
    }
}