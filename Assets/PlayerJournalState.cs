using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerJournalState : MonoBehaviour, ObjectState
{
    private ObjectState _nextState;
    private PlayerActionState _actionState;
    private JournalBehaviour _playerJournal;
    private StarterAssetsInputs _input;
    // Start is called before the first frame update
    void Start()
    {
        _nextState = this;
        _playerJournal = GetComponentInChildren<JournalBehaviour>(true);
        _actionState = GetComponent<PlayerActionState>();
        _input = GetComponentInChildren<StarterAssetsInputs>();

    }

    public ObjectState GetNextState()
    {
        return _nextState;
    }

    public void RestoreDefaults()
    {
        _nextState = this;
    }

    public void StateUpdate()
    {
        NotepadOpen();
        NotepadClose();
    }

    private void NotepadOpen()
    {
        if (!_playerJournal.gameObject.activeSelf)
        {
            _input.journal = false;
            _playerJournal.OpenNotepad();
        }
    }

    private void TurnPage()
    {

    }

    private void NotepadClose()
    {
        if (_input.journal)
        {
            _input.journal = false;
            _playerJournal.CloseNotepad();
            _nextState = _actionState;
        }
    }

    public void StateLateUpdate()
    {

    }

}
