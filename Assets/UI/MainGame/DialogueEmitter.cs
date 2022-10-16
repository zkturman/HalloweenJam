using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEmitter : MonoBehaviour, TextEmitter
{
    [SerializeField]
    private string speakerName;
    [SerializeField]
    private string[] dialogueLines;
    [SerializeField]
    private string[] preSkullLines;
    [SerializeField]
    private string[] postSkullLines;
    [SerializeField]
    private string[] hintLines;
    [SerializeField]
    private string[] postShardsLines;
    private DialogueDisplayBehaviour dialogueManager;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueDisplayBehaviour>();
    }

    public void EmitText()
    {
        dialogueManager.AddStepThroughDialogue(dialogueLines);
        dialogueManager.AddSpeaker(speakerName);
    }
}
