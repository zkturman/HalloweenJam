using UnityEngine;
using UnityEngine.UIElements;

public class CreditsUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button continueButton;
    private Button quitButton;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        continueButton = new SceneButton("ContinueButton", "StartScreen").GenerateButton(rootVisualElement);
        quitButton = new QuitButton("QuitButton").GenerateButton(rootVisualElement);
    }
}
