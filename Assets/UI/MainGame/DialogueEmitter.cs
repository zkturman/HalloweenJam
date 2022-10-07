using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEmitter : MonoBehaviour, TextEmitter
{
    [SerializeField]
    private List<string> dialogueLines;
    private DialogueDisplayBehaviour dialogueManager;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueDisplayBehaviour>();
    }

    public void EmitText()
    {
        dialogueManager.DisplayDialogue(dialogueLines[0]);
    }
}
