using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class JournalDisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    private MainGameUIController mainGameUIController;
    [SerializeField]
    private float displayTimeInSeconds = 3f;
    private const string JOURNAL_UPDATE_TEMPLATE = "Updated description for passenger No. {0}.";

    public void IndicateUpdatedEntry(int passengerId)
    {
        string updateMessage = string.Format(JOURNAL_UPDATE_TEMPLATE, passengerId);
        StartCoroutine(indicateUpdateEntryRoutine(updateMessage));

    }

    private IEnumerator indicateUpdateEntryRoutine(string updateMessage)
    {
        mainGameUIController.AddJournalUpdateText(updateMessage);
        mainGameUIController.DisplayJournalUpdateMessage(true);
        yield return new WaitForSeconds(displayTimeInSeconds);
        mainGameUIController.DisplayJournalUpdateMessage(false);
    }
}
