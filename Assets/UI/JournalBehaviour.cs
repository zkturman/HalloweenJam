using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject notepad;
    private Animator notepadAnimator;
    [SerializeField]
    private JournalUpdateDispalyBehaviour journalUI;
    [SerializeField]
    private GameObject journalProjector;
    private float openDuration = 1.5f;
    private float closeDuration = 1.5f;
    [SerializeField]
    private GameObject playerHud;

    private void Awake()
    {
        notepadAnimator = notepad.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenNotepad()
    {
        gameObject.SetActive(true);
        StartCoroutine(openNotepadRoutine());
    }

    private IEnumerator openNotepadRoutine()
    {
        notepadAnimator.SetTrigger("Open");
        playerHud.SetActive(false);
        yield return new WaitForSeconds(openDuration);
        journalProjector.SetActive(true);
    }

    public void CloseNotepad()
    {
        StartCoroutine(closeNotepadRoutine());
    }

    private IEnumerator closeNotepadRoutine()
    {
        notepadAnimator.SetTrigger("Close");
        journalProjector.SetActive(false);
        yield return new WaitForSeconds(closeDuration);
        playerHud.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TurnRightPage()
    {
        if (journalUI.CanFlipRight())
        {
            journalUI.FlipPageRight();
        }
    }

    public void TurnLeftPage()
    {
        if (journalUI.CanFlipLeft())
        {
            journalUI.FlipPageLeft();
        }
    }

    public bool IsPassengerFound(int passengerId)
    {
        return journalUI.IsPassengerFound(passengerId);
    }

    public void AddFoundPassenger(int passengerId)
    {
        journalUI.FlagPassengerAsFound(passengerId);
    }

    public void AddPassengerColorTag(int passengerId, Color colorTag)
    {
        journalUI.AddFoundPassengerColor(passengerId, colorTag);
    }
}
