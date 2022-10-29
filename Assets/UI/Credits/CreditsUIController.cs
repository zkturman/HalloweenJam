using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button continueButton;
    private Button quitButton;
    private GroupBox buttonBox;
    private VisualElement creditsContainer;
    private Label thankYouMessage;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        continueButton = new SceneButton("ContinueButton", "StartScreen").GenerateButton(rootVisualElement);
        quitButton = new QuitButton("QuitButton").GenerateButton(rootVisualElement);
        buttonBox = rootVisualElement.Q<GroupBox>("ButtonBox");
        thankYouMessage = rootVisualElement.Q<Label>("ThankYouMessage");
        creditsContainer = rootVisualElement.Q<VisualElement>("CreditsContainer");
        StartCoroutine(playCredits());
    }
    private IEnumerator playCredits()
    {
        yield return new WaitForSeconds(1f);
        creditsContainer.ToggleInClassList("Top");
        yield return new WaitForSeconds(10f);
        thankYouMessage.visible = true;
        thankYouMessage.ToggleInClassList("FadeTransition");
        yield return new WaitForSeconds(1f);
        buttonBox.visible = true;
    }
}
