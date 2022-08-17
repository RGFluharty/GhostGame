using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public int currentDialogue;
    public int linesLength;

    bool linesRemain = false;

    bool inCutscene = false;

    [SerializeField] bool openingLevelCutscene;

    private void Start()
    {
        inCutscene = false;
        currentDialogue = -1;
        linesLength = dialogue.Length;
        if (openingLevelCutscene)
        {
            TriggerDialogue();
        }
        
    }

    public void TriggerDialogue()
    {
        inCutscene = true;
        AddToCurrentDialogue();
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[currentDialogue]);
    }

    public bool GetLinesRemain()
    {
        if (currentDialogue == linesLength-1)
        {
            linesRemain = false;
        }
        else
        {
            linesRemain = true;
        }
        return linesRemain;
    }

    public void AddToCurrentDialogue()
    {
        currentDialogue++;
    }

    public Dialogue GetCurrentDialogue()
    {
        return dialogue[currentDialogue];
    }

    public void SetInCutsceneFalse()
    {
        inCutscene = false;
    }

    public bool GetInCutscene()
    {
        return inCutscene;
    }

}
