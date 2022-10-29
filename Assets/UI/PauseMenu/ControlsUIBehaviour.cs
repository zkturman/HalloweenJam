using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ControlsUIBehaviour : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private Button backButton;
    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        backButton = rootVisualElement.Q<Button>("BackButton");
        backButton.RegisterCallback<ClickEvent>(ev => closeControlUI());
    }

    private void closeControlUI()
    {
        gameObject.SetActive(false);
    }
}
