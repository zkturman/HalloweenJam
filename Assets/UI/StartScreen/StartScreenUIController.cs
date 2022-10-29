using UnityEngine;
using UnityEngine.UIElements;

public class StartScreenUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button startButton;
    private Button settingsButton;
    private Button quitButton;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        startButton = new SceneButton("StartButton", "BackgroundStory").GenerateButton(rootVisualElement);
        quitButton = new QuitButton("QuitButton").GenerateButton(rootVisualElement);
    }
}
