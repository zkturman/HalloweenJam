using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueDisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    private MainGameUIController mainGameUIController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDialogue(string dialogueText)
    {
        mainGameUIController.AddDialogue(dialogueText, false);
    }

    public void DisplayPassingDialogue(string dialogueText)
    {

    }
}
