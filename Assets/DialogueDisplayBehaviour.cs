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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialogueEntryPoint()
    {
        mainGameUIController.AddMessage(dialogueEntryMessasge);
    }

    public void AddStepThroughDialogue(string[] dialogueLines)
    {
        this.dialogueLines = dialogueLines;
    }

    public void DisplayNextLine()
    {
        string nextLine = dialogueLines[currentLine];
        currentLine++;
        bool isFinished = IsDialogueFinished();
        mainGameUIController.AddDialogue(nextLine, isFinished);
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

    public void ClearDialogue()
    {
        Debug.Log("Cleared dialogue");
        mainGameUIController.AddMessage("");
        currentLine = 0;
    }
}
