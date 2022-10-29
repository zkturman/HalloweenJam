using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		private ObjectState currentState;

		private void Awake()
		{

		}

		private void Start()
		{
			currentState = GetComponent<PlayerActionState>();
		}

		private void Update()
		{
			currentState.StateUpdate();
			currentState = currentState.GetNextState();
			currentState.RestoreDefaults();
		}

		private void LateUpdate()
		{
			currentState.StateLateUpdate();
		}
	}
}