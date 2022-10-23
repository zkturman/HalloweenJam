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
    [SerializeField]
    private AudioSource journalUpdateSound;
    [SerializeField]
    private float clipDurationInSeconds;
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
        StartCoroutine(playSoundEffect());
        yield return new WaitForSeconds(displayTimeInSeconds);
        mainGameUIController.DisplayJournalUpdateMessage(false);
    }

    private IEnumerator playSoundEffect()
    {
        journalUpdateSound.Play();
        yield return new WaitForSeconds(clipDurationInSeconds);
        journalUpdateSound.Stop();
    }
}
