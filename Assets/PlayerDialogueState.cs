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

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _dialogueDisplay = FindObjectOfType<DialogueDisplayBehaviour>();
        _actionState = GetComponent<PlayerActionState>();
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
                _dialogueDisplay.ClearDialogue();
            }
            else
            {
                _dialogueDisplay.DisplayNextLine();
                _input.interact = false;
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
