using UnityEngine;
using UnityEngine.UIElements;

public class MainGameUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button gameOverButton;
    private Button creditsButton;
    private Label dialogueLabel;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        gameOverButton = new SceneButton("GameOverButton", "GameOver").GenerateButton(rootVisualElement);
        creditsButton = new SceneButton("CreditsButton", "Credits").GenerateButton(rootVisualElement);
        dialogueLabel = new MessageLabel("DialogueLabel").GenerateLabel(rootVisualElement);
    }

    public void AddMessage(string messageText)
    {
        dialogueLabel.text = messageText;
    }

    public void AddDialogue(string dialogueText, bool endOfDialogue)
    {
        dialogueLabel.text = dialogueText;
    }
}
