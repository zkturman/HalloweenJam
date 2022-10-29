using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerPauseState : MonoBehaviour, ObjectState
{
    private ObjectState _nextState;
    private StarterAssetsInputs _input;
    private PauseMenuBehaviour _menuBehaviour;
    private PlayerActionState _actionState;

    private void Awake()
    {
        _nextState = this;
        _input = GetComponent<StarterAssetsInputs>();
        _menuBehaviour = FindObjectOfType<PauseMenuBehaviour>(true);
        _actionState = GetComponent<PlayerActionState>();
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
        OpenMenu();
        CloseMenu();
    }

    private void OpenMenu()
    {
        if (!_menuBehaviour.gameObject.activeSelf)
        {
            _menuBehaviour.OpenMenu();
            _input.pause = false;
            _input.SetCursorState(false);
        }
    }

    private void CloseMenu()
    {
        if (_input.pause)
        {
            handleMenuClose();
        }
        if (_menuBehaviour.ShouldCloseMenu())
        {
            handleMenuClose();
        }
    }

    private void handleMenuClose()
    {
        _menuBehaviour.CloseMenu();
        _input.pause = false;
        _input.SetCursorState(true);
        _nextState = _actionState;
    }


    public void StateLateUpdate()
    {
    }
}
