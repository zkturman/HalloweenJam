using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SceneButton : UIButton
{
    private string buttonName;
    private string sceneName;

    public SceneButton(string buttonName, string sceneName)
    {
        this.buttonName = buttonName;
        this.sceneName = sceneName;
    }

    public Button GenerateButton(VisualElement rootVisualElement)
    {
        Button newButton = rootVisualElement.Q<Button>(buttonName);
        newButton.RegisterCallback<ClickEvent>(ev => openScene());
        return newButton;
    }

    private void openScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
