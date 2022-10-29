using UnityEngine;
using UnityEngine.UIElements;

public class MainGameUIController : MonoBehaviour
{
    private VisualElement rootVisualElement;
    private VisualElement dialogueBox;
    private Label dialogueLabel;
    private Label speakerNameLabel;
    private Label lineEndLabel;
    private const string CONTINUE_TEXT = "Continue";
    private const string END_TEXT = "End";
    private VisualElement journalUpdateBox;
    private Label journalUpdateLabel;
    [SerializeField]
    private VisualTreeAsset heartHealthTemplate;
    private VisualElement healthField;
    private int numberOfHearts = 0;
    private VisualElement counterParentElement;
    private bool countersVisible = false;
    private Label skullChargesText;
    private string skullCharges = "0";
    private Label shardCountText;
    private string shardCount = "0";

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        dialogueBox = rootVisualElement.Q<VisualElement>("DialogueBox");
        dialogueBox.visible = false;
        dialogueLabel = new MessageLabel("DialogueLabel").GenerateLabel(rootVisualElement);
        speakerNameLabel = new MessageLabel("SpeakerName").GenerateLabel(rootVisualElement);
        lineEndLabel = new MessageLabel("LineEndText").GenerateLabel(rootVisualElement);
        journalUpdateBox = rootVisualElement.Q<VisualElement>("JournalUpdateBox");
        journalUpdateLabel = new MessageLabel("JournalUpdateText").GenerateLabel(rootVisualElement);
        DisplayDialogueBox(false);
        DisplayJournalUpdateMessage(false);
        healthField = rootVisualElement.Q<VisualElement>("PlayerHealth");
        setHearts(numberOfHearts);
        counterParentElement = rootVisualElement.Q<VisualElement>("PlayerCounters");
        counterParentElement.visible = countersVisible;
        skullChargesText = new MessageLabel("ChargeCount").GenerateLabel(rootVisualElement);
        SetChargeCount(skullCharges);
        shardCountText = new MessageLabel("ShardCount").GenerateLabel(rootVisualElement);
        SetShardCount(shardCount);
    }

    public void DisplayDialogueBox(bool shouldDisplay)
    {
        dialogueBox.visible = shouldDisplay;
    }

    public void AddMessage(string messageText)
    {
        dialogueLabel.text = messageText;
    }

    public void AddDialogue(string dialogueText, bool endOfDialogue)
    {
        dialogueLabel.text = dialogueText;
    }

    public void AddDialogueSpeaker(string name)
    {
        speakerNameLabel.text = name;
    }

    public void SetEndOfDialogue(bool isEndOfDialogue)
    {
        if (isEndOfDialogue)
        {
            lineEndLabel.text = END_TEXT;
        }
        else
        {
            lineEndLabel.text = CONTINUE_TEXT;
        }
    }

    public void AddJournalUpdateText(string info)
    {
        journalUpdateLabel.text = info;
    }

    public void DisplayJournalUpdateMessage(bool shouldDisplay)
    {
        journalUpdateBox.visible = shouldDisplay;
    }

    public void AddHeart()
    {
        numberOfHearts++;
        VisualElement healthHeart = heartHealthTemplate.Instantiate();
        healthField.Add(healthHeart);
    }

    public void RemoveHeart()
    {
        if (numberOfHearts > 0)
        {
            healthField.RemoveAt(numberOfHearts - 1);
            numberOfHearts--;
        }
    }

    private void setHearts(int numberOfHearts)
    {
        for (int i = 0; i < numberOfHearts; i++)
        {
            VisualElement healthHeart = heartHealthTemplate.Instantiate();
            healthField.Add(healthHeart);
        }
    }

    public void EnablePlayerCounters()
    {
        countersVisible = true;
        counterParentElement.visible = countersVisible;
    }

    public void SetChargeCount(string chargeText)
    {
        skullCharges = chargeText;
        skullChargesText.text = skullCharges;
    }

    public void SetShardCount(string shardText)
    {
        shardCount = shardText;
        shardCountText.text = shardCount;
    }
}
