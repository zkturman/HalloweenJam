using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueDisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    private MainGameUIController mainGameUIController;
    [SerializeField]
    private string dialogueEntryMessasge;
    private string[] dialogueLines;
    private int currentLine = 0;
    private bool isActive = false;

    public void DisplayDialogueEntryPoint()
    {
        mainGameUIController.DisplayDialogueBox(true);
        mainGameUIController.AddMessage(dialogueEntryMessasge);
        isActive = true;
    }

    public void AddStepThroughDialogue(string[] dialogueLines)
    {
        this.dialogueLines = dialogueLines;
    }

    public void AddSpeaker(string name)
    {
        mainGameUIController.AddDialogueSpeaker(name);
    }

    public void DisplayNextLine()
    {
        string nextLine = dialogueLines[currentLine];
        currentLine++;
        bool isFinished = IsDialogueFinished();
        mainGameUIController.AddDialogue(nextLine, isFinished);
        mainGameUIController.SetEndOfDialogue(isLastLine());
    }

    private bool isLastLine()
    {
        return currentLine == dialogueLines.Length; 
    }

    public bool IsDialogueFinished()
    {
        bool isFinished = false;
        if (dialogueLines == null)
        {
            isFinished = true;
        }
        else
        {
            isFinished = dialogueLines.Length == currentLine;
        }
        return isFinished;
    }

    public void DisplayPassingDialogue(string dialogueText)
    {

    }
    
    public void ResetDialogue()
    {
        mainGameUIController.AddMessage("");
        mainGameUIController.SetEndOfDialogue(false);
        currentLine = 0;
    }

    public void ExitDialogue()
    {
        if (isActive)
        {
            Debug.Log("Cleared dialogue");
            mainGameUIController.DisplayDialogueBox(false);
            ResetDialogue();
            isActive = false;
            mainGameUIController.AddDialogueSpeaker("");
        }
    }
}
