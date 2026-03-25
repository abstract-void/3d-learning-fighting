using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerJump))]
public class PlayerFighterController : MonoBehaviour
{
	// Tarea: controlador principal común (2.5D): lee input “directo” (por PlayerId), gestiona estados (en suelo/en aire), y coordina movimiento/combate.
	// Habla con: PlayerMovement2_5D, PlayerJump, FighterCombatCore, FighterDamageReceiver y expone referencias comunes para HUD/cámara.
	[SerializeField] private InputActionAsset inputActions;

	[SerializeField] private string playerId;

	private InputActionMap actionMap;

	private PlayerJump playerJump;

	public string PlayerId => playerId;

	public Vector2 Direction { get; private set; }

	private void Awake()
	{
		actionMap = inputActions.FindActionMap(playerId);
		playerJump = GetComponent<PlayerJump>();
	}

	private void OnEnable()
	{
		actionMap.Enable();

		actionMap.FindAction("Move").performed += Move;
		actionMap.FindAction("Move").canceled += StopMoving;
		actionMap.FindAction("Jump").performed += Jump;
	}

	private void OnDisable()
	{
		actionMap.Disable();

		actionMap.FindAction("Move").performed -= Move;
		actionMap.FindAction("Move").canceled -= StopMoving;
		actionMap.FindAction("Jump").performed -= Jump;
	}

	private void Move(InputAction.CallbackContext context)
	{
		Direction = context.ReadValue<Vector2>();
	}

	private void StopMoving(InputAction.CallbackContext context)
	{
		Direction = new Vector2();
	}

	private void Jump(InputAction.CallbackContext context)
	{
		playerJump.Jump();
	}
}
