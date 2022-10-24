using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseMenuUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button quitButton;
    private Button continueButton;
    private Button controlsButton;
    private SliderInt brightnessSetting;
    private int currentBrightness = 5;
    private float minBrightness = -5;
    private float maxBrightness = 5;
    private bool pressedContinue = false;
    [SerializeField]
    private GameObject controlsUI;
    [SerializeField]
    private Volume postProcessingVolume;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        quitButton = new SceneButton("QuitButton", "StartScreen").GenerateButton(rootVisualElement);
        continueButton = rootVisualElement.Q<Button>("ContinueButton");
        continueButton.RegisterCallback<ClickEvent>(ev => logPressedContinueAction());
        controlsButton = rootVisualElement.Q<Button>("ControlsButton");
        controlsButton.RegisterCallback<ClickEvent>(ev => openControlsUI());
        controlsUI.SetActive(false);
        brightnessSetting = rootVisualElement.Q<SliderInt>("BrightnessSetting");
        brightnessSetting.RegisterValueChangedCallback(ev => setBrightness());
        brightnessSetting.value = currentBrightness;
    }

    private void logPressedContinueAction()
    {
        pressedContinue = true;
    }

    private void openControlsUI()
    {
        controlsUI.SetActive(true);
    }

    private void setBrightness()
    {
        currentBrightness = brightnessSetting.value;
        postProcessingVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorComponent);
        //colorComponent.postExposure = new FloatParameter(currentBrightness - 5);
        colorComponent.postExposure.Override(currentBrightness - 5);
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
