using UnityEngine;
using UnityEngine.UIElements;

public class SettingsUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button saveButton;
    private Button backButton;

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        saveButton = new SaveButton("SaveButton").GenerateButton(rootVisualElement);
        backButton = new SceneButton("BackButton", "StartScreen").GenerateButton(rootVisualElement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
