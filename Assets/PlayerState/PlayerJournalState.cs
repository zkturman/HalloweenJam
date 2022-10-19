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

    [Tooltip("Set if the player is currently turning the page.")]
    public bool Turning = false;
    [Tooltip("Amount of time to wait before you can turn pages again.")]
    public float TurnTimeout = 0.8f;
    private float _turnTimeoutDelta;
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
        TurnPage();
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
        if (!Turning)
        {
            if (_input.move.x != 0f)
            {
                Turning = true;
                _turnTimeoutDelta = TurnTimeout;
                if (_input.move.x > 0f)
                {
                    _playerJournal.TurnRightPage();
                }
                else
                {
                    _playerJournal.TurnLeftPage();
                }
                Debug.Log("Tried to turn page.");
            }
        }
        else
        {
            _turnTimeoutDelta -= Time.deltaTime;
            if (_turnTimeoutDelta <= 0.0f)
            {
                Turning = false;
            }
        }
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
