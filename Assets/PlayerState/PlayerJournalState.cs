using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class PlayerJournalState : MonoBehaviour, ObjectState
{
    private ObjectState _nextState;
    private PlayerActionState _actionState;
    private JournalBehaviour _playerJournal;
    private StarterAssetsInputs _input;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    private PlayerInput _playerInput;
#endif
    private SkullWeaponBehaviour _skullWeapon;

    // Start is called before the first frame update
    [Tooltip("Rotation speed of the character")]
    public float RotationSpeed = 1.0f;
    [Tooltip("Set if the player is currently turning the page.")]
    public bool Turning = false;
    [Tooltip("Amount of time to wait before you can turn pages again.")]
    public float TurnTimeout = 0.8f;
    private float _turnTimeoutDelta;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;
    private const float _threshold = 0.01f;
    private float _cinemachineTargetPitch;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }

    void Start()
    {
        _nextState = this;
        _playerJournal = GetComponentInChildren<JournalBehaviour>(true);
        _actionState = GetComponent<PlayerActionState>();
        _input = GetComponentInChildren<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
        _skullWeapon = GetComponentInChildren<SkullWeaponBehaviour>(true);
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
            TrySetActiveSkull(false);
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
            TrySetActiveSkull(true);
        }
    }

    private void TrySetActiveSkull(bool isActive)
    {
        if (_skullWeapon.enabled)
        {
            _skullWeapon.gameObject.SetActive(isActive);
        }
    }

    private void CameraRotation()
    {
        // if there is an input
        if (_input.look.sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);
        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    public void StateLateUpdate()
    {
        CameraRotation();
    }

}
