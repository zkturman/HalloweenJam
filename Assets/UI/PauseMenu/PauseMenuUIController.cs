using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class PauseMenuUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button quitButton;
    private Button continueButton;
    private Button controlsButton;
    private bool pressedContinue = false;
    [SerializeField]
    private GameObject controlsUI;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        quitButton = new SceneButton("QuitButton", "StartScreen").GenerateButton(rootVisualElement);
        continueButton = rootVisualElement.Q<Button>("ContinueButton");
        continueButton.RegisterCallback<ClickEvent>(ev => logPressedContinueAction());
        controlsButton = rootVisualElement.Q<Button>("ControlsButton");
        controlsButton.RegisterCallback<ClickEvent>(ev => openControlsUI());
    }

    private void logPressedContinueAction()
    {
        pressedContinue = true;
    }

    private void openControlsUI()
    {
        controlsUI.SetActive(true);
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
