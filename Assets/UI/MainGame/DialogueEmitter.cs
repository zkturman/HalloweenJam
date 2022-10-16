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
    private SkullWeaponBehaviour playerSkull;
    private MonsterHandler monsterHandler;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueDisplayBehaviour>();
        monsterHandler = FindObjectOfType<MonsterHandler>();
        playerSkull = FindObjectOfType<SkullWeaponBehaviour>(true);
    }

    public void EmitText()
    {
        dialogueLines = generateDialogueLines();
        dialogueManager.AddStepThroughDialogue(dialogueLines);
        dialogueManager.AddSpeaker(speakerName);
    }

    private string[] generateDialogueLines()
    {
        return new string[0];
    }
}
