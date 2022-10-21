using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PauseMenuUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button quitButton;
    private Button continueButton;
    private bool pressedContinue = false;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        quitButton = new SceneButton("QuitButton", "StartScreen").GenerateButton(rootVisualElement);
        continueButton = rootVisualElement.Q<Button>("ContinueButton");
        continueButton.RegisterCallback<ClickEvent>(ev => logPressedAction());
    }

    private void logPressedAction()
    {
        pressedContinue = true;
    }

    public bool HasUserPressedContinue()
    {
        return pressedContinue;
    }

    private void OnDisable()
    {
        pressedContinue = false;
    }
}
