using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerDialogueState : MonoBehaviour, ObjectState
{
    private StarterAssetsInputs _input;
    private DialogueDisplayBehaviour _dialogueDisplay;
    private ObjectState nextState;
    private PlayerActionState _actionState;
    private GameStateController _gameStateController;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _dialogueDisplay = FindObjectOfType<DialogueDisplayBehaviour>();
        _actionState = GetComponent<PlayerActionState>();
        _gameStateController = FindObjectOfType<GameStateController>();
        nextState = this;
    }

    public ObjectState GetNextState()
    {
        return nextState;
    }

    public void StateUpdate()
    {
        DialogueInteract();
    }

    private void DialogueInteract()
    {
        if (_input.interact)
        {
            if (_dialogueDisplay.IsDialogueFinished())
            {
                _input.interact = false;
                nextState = _actionState;
                _dialogueDisplay.ResetDialogue();
                updateGameState();
                retriggerDialogueEmitter();
            }
            else
            {
                _dialogueDisplay.DisplayNextLine();
                _input.interact = false;
            }
        }
    }

    private void updateGameState()
    {
        if (_gameStateController.InRetrievedSkullState())
        {
            _gameStateController.SetReceivedHintsState();
        }
        else if (_gameStateController.InReceivedHintsState())
        {
            //check all shards --> hasallshardsstate
        }
        else if (_gameStateController.InHasAllShardsState())
        {
            _gameStateController.SetGameFinishedState();
        }
    }

    private void retriggerDialogueEmitter()
    {
        Collider[] allDialogueObjects = Physics.OverlapSphere(transform.position, 2f);
        for (int i = 0; i < allDialogueObjects.Length; i++)
        {
            GameObject hit = allDialogueObjects[i].gameObject;
            if (hit.tag == "Interactable")
            {
                hit.GetComponent<DialogueEmitter>()!.EmitText();
            }
        }
    }

    public void StateLateUpdate()
    {

    }

    public void RestoreDefaults()
    {
        nextState = this;
    }
}
