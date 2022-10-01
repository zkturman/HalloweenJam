using UnityEngine;
using UnityEngine.UIElements;

public class MainGameUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button gameOverButton;
    private Button credtisButton;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        gameOverButton = new SceneButton("GameOverButton", "GameOver").GenerateButton(rootVisualElement);
        credtisButton = new SceneButton("CreditsButton", "Credits").GenerateButton(rootVisualElement);
    }
}
