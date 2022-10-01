using UnityEngine;
using UnityEngine.UIElements;

public class QuitButton : UIButton
{
    private string buttonName;
    
    public QuitButton(string buttonName)
    {
        this.buttonName = buttonName;
    }

    public Button GenerateButton(VisualElement rootVisualElement)
    {
        Button newButton = rootVisualElement.Q<Button>(buttonName);
        newButton.RegisterCallback<ClickEvent>(ev => quitGame());
        return newButton;
    }

    private void quitGame()
    {
        Application.Quit();
    }
}
