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
    private MonsterHandler monsterHandler;
    private GameStateController gameStateController;

    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueDisplayBehaviour>();
        monsterHandler = FindObjectOfType<MonsterHandler>();
        gameStateController = FindObjectOfType<GameStateController>();
    }

    public void EmitText()
    {
        dialogueLines = generateDialogueLines();
        dialogueManager.AddStepThroughDialogue(dialogueLines);
        dialogueManager.AddSpeaker(speakerName);
    }

    private string[] generateDialogueLines()
    {
        string[] lines;
        if (gameStateController.InRetrievedSkullState())
        {
            lines = generateHintLines(postSkullLines);
        }
        else if (gameStateController.InReceivedHintsState())
        {
            lines = generateHintLines(hintLines);
        }
        else if (gameStateController.InHasAllShardsState())
        {
            lines = postShardsLines;
        }
        else
        {
            lines = preSkullLines;
        }
        return lines;
    }

    private string[] generateHintLines(string[] lines)
    {
        List<string> hintText;
        if (lines == null)
        {
            hintText = new List<string>();
        }
        else
        {
            hintText = new List<string>(lines);
        }

        for (int i = 0; i < MonsterHandler.NumberOfShards; i++)
        {
            hintText.Add(monsterHandler.GetHintFromIndex(i));
        }

        return hintText.ToArray();
    }
}
