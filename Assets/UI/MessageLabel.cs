using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MessageLabel
{
    private string labelName;
    public MessageLabel(string labelName)
    {
        this.labelName = labelName;
    }

    public Label GenerateLabel(VisualElement rootVisualElement)
    {
        Label newLabel = rootVisualElement.Q<Label>(labelName);
        return newLabel;
    }
}
