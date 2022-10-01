using UnityEngine;
using UnityEngine.UIElements;

public class SaveButton : UIButton
{
    private string buttonName;

    public SaveButton(string buttonName)
    {
        this.buttonName = buttonName;
    }

    public Button GenerateButton(VisualElement rootVisualElement)
    {
        Button newButton = rootVisualElement.Q<Button>(buttonName);
        newButton.RegisterCallback<ClickEvent>(ev => saveSettings());
        return newButton;
    }

    private void saveSettings()
    {
        GameSettings.SaveSettings();
    }
}
