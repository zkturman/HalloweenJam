using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JournalUpdateDispalyBehaviour : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset hintTemplate;
    [SerializeField]
    private VisualTreeAsset passengerInfoTemplate;
    [SerializeField]
    private VisualElement rootVisualElement;
    private VisualElement leftPageElement;
    private VisualElement rightPageElement;
    private VisualElement leftTurnIcon;
    private VisualElement rightTurnIcon;
    private MonsterHandler monsterHandler;
    private bool[] foundPassengers = new bool[MonsterHandler.NumberOfPassengers];
    private Color[] passengerColors = new Color[MonsterHandler.NumberOfPassengers];
    private const string MISSING_PASSENGER_TEXT = "???";
    private const string PASSENGER_ID_PREFIX = "No. ";
    [SerializeField]
    private int passengersPerPage = 3;
    [SerializeField]
    private CustomRenderTexture renderProjector;
    private int currentPageSet = 0;

    private void Awake()
    {
        monsterHandler = FindObjectOfType<MonsterHandler>();
    }

    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        leftPageElement = rootVisualElement.Q<VisualElement>("LeftPage");
        leftTurnIcon = rootVisualElement.Q<VisualElement>("TurnPageLeft");
        rightPageElement = rootVisualElement.Q<VisualElement>("RightPage");
        rightTurnIcon = rootVisualElement.Q<VisualElement>("TurnPageRight");
        buildPageSet();
    }

    private void OnDisable()
    {
        removeAllChildren(leftPageElement);
        removeAllChildren(rightPageElement);
    }

    private void buildPageSet()
    {
        removeAllChildren(leftPageElement);
        removeAllChildren(rightPageElement);
        buildLeftPage();
        buildRightPage();
        if (!CanFlipLeft())
        {
            leftTurnIcon.visible = false;
        }
        else
        {
            leftTurnIcon.visible = true;
        }
        if (!CanFlipRight())
        {
            rightTurnIcon.visible = false;
        }
        else
        {
            rightTurnIcon.visible = true;
        }
        renderProjector.Update();
    }

    private void buildLeftPage()
    {
        if (currentPageSet == 0)
        {
            buildHintPage(leftPageElement);
        }
        else
        {
            int setNumber = currentPageSet + 1;
            buildPassengerPage(leftPageElement, setNumber);
        }
    }

    private void buildRightPage()
    {
        int setNumber;
        if (currentPageSet == 0)
        {
            setNumber = 1;
            buildPassengerPage(rightPageElement, setNumber);
        }
        else
        {
            setNumber = currentPageSet + 2;
            buildPassengerPage(rightPageElement, setNumber);
        }
    }

    private void removeAllChildren(VisualElement parentElement)
    {
        for (int i = parentElement.childCount - 1; i >= 0; i--)
        {
            parentElement.RemoveAt(i);
        }
    }

    private int getNumberOfPages()
    {
        int passengerPages = MonsterHandler.NumberOfPassengers / passengersPerPage;
        int remainder = MonsterHandler.NumberOfPassengers % passengersPerPage;
        if (remainder > 0)
        {
            passengersPerPage++;
        }
        int hintPages = 1;
        return passengerPages + hintPages;
    }

    private int getNumberOfPageSets()
    {
        int numberOfPages = getNumberOfPages();
        int pageSets = numberOfPages / 2;
        int remainder = numberOfPages % 2;
        if (remainder > 0)
        {
            pageSets++;
        }
        return pageSets;
    }

    private void buildHintPage(VisualElement parentElement)
    {
        for (int i = 0; i < MonsterHandler.NumberOfShards; i++)
        {
            VisualElement hint = hintTemplate.Instantiate();
            Label hintLabel = hint.Q<Label>("HintText");
            hintLabel.text = monsterHandler.GetHintFromIndex(i);
            parentElement.Add(hint);
        }
    }

    private void buildPassengerPage(VisualElement parentElement, int setNumberFromOne)
    {
        int startingIndex = (setNumberFromOne - 1) * passengersPerPage;
        int endingIndex = startingIndex + passengersPerPage;
        for (int i = startingIndex; i < endingIndex; i++)
        {
            VisualElement description = passengerInfoTemplate.Instantiate();
            buildPassengerInfo(description, i);
            parentElement.Add(description);
        }
    }

    private void buildPassengerInfo(VisualElement descriptionElement, int index)
    {
        Label passengerIdLabel = descriptionElement.Q<Label>("PassengerId");
        passengerIdLabel.text = PASSENGER_ID_PREFIX + index;
        Label descriptionLabel = descriptionElement.Q<Label>("InfoText");
        VisualElement passengeIdentifier = descriptionElement.Q<VisualElement>("VisualIdentifier");
        if (foundPassengers[index])
        {
            descriptionLabel.text = monsterHandler.GetDescriptionFromMonsterId(index);
            passengeIdentifier.style.backgroundColor = passengerColors[index];
        }
        else
        {
            descriptionLabel.text = MISSING_PASSENGER_TEXT;
        }
    }

    public bool CanFlipRight()
    {
        int numberOfPageSets = getNumberOfPageSets();
        return currentPageSet + 1 < numberOfPageSets;
    }

    public void FlipPageRight()
    {
        currentPageSet++;
        buildPageSet();
    }

    public bool CanFlipLeft()
    {
        return currentPageSet > 0;
    }

    public void FlipPageLeft()
    {
        currentPageSet--;
        buildPageSet();
    }

    public bool IsPassengerFound(int passengerId)
    {
        return foundPassengers[passengerId];
    }

    public void FlagPassengerAsFound(int passengerId)
    {
        foundPassengers[passengerId] = true;
    }

    public void AddFoundPassengerColor(int passengerId, Color colorTag)
    {
        passengerColors[passengerId] = colorTag;
    }
}
