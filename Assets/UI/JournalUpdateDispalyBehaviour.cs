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
    private const string MISSING_PASSENGER_TEXT = "???";
    [SerializeField]
    private int passengersPerPage = 3;
    private int currentPageSet = 0;

    private void Awake()
    {
        monsterHandler = FindObjectOfType<MonsterHandler>();
        for (int i = 0; i < foundPassengers.Length; i++)
        {
            foundPassengers[i] = false;
        }
    }


    private void OnEnable()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        leftPageElement = rootVisualElement.Q<VisualElement>("LeftPage");
        rightPageElement = rootVisualElement.Q<VisualElement>("RightPage");
        buildHintPage(leftPageElement);
        buildPassengerPage(rightPageElement, 1);
    }

    private void buildLeftPage()
    {

    }

    private void buildRightLPage()
    {

    }

    private void OnDisable()
    {
        removeAllChildren(leftPageElement);
        removeAllChildren(rightPageElement);
    }

    private void removeAllChildren(VisualElement parentElement)
    {
        for (int i = parentElement.childCount - 1; i > 0; i--)
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

    private int getNumberOfPageSets(int numberOfPages)
    {
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
        int startingIndex = (setNumberFromOne - 1) * 4;
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
        Label descriptionLabel = descriptionElement.Q<Label>("InfoText");
        if (foundPassengers[index])
        {

        }
        else
        {
            descriptionLabel.text = MISSING_PASSENGER_TEXT;
        }
    }

    public bool CanFlipRight()
    {
        return false;
    }

    public void FlipPageRight()
    {

    }

    public bool CanFlipLeft()
    {
        return false;
    }

    public void FlipPageLeft()
    {

    }
}
