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
    }

    public void CloseNotepad()
    {
        StartCoroutine(closeNotepadRoutine());
    }

    private IEnumerator closeNotepadRoutine()
    {
        notepadAnimator.SetTrigger("Close");
        yield return new WaitForSeconds(closeDuration);
        playerHud?.SetActive(true);
        gameObject.SetActive(false);
    }
}
