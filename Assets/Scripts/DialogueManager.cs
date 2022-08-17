using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] Animator animator;

    [SerializeField] DialogueTrigger cutsceneObject;

    

    private Queue<string> sentences;


    private void Awake()
    {
        sentences = new Queue<string>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && cutsceneObject.GetInCutscene())
        {
            DisplayNextSentence();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
            return;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (!cutsceneObject.GetLinesRemain())
            {
                EndDialogue();
                return;
            }
            else
            {
                cutsceneObject.AddToCurrentDialogue();
                StartDialogue(cutsceneObject.GetCurrentDialogue());
                return;
            }
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        cutsceneObject.SetInCutsceneFalse();
        
    }

    
    
}
