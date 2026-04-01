using System;

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerJump))]
public class PlayerFighterController : MonoBehaviour
{
	// Tarea: controlador principal común (2.5D): lee input “directo” (por PlayerId), gestiona estados (en suelo/en aire), y coordina movimiento/combate.
	// Habla con: PlayerMovement2_5D, PlayerJump, FighterCombatCore, FighterDamageReceiver y expone referencias comunes para HUD/cámara.
	[SerializeField] private InputActionAsset inputActions;

	private string playerId;

	private InputActionMap actionMap;

	private PlayerJump playerJump;
	private FighterCombatCore fighterCombatCore;

	private bool isAttacking = false;

	public event Action SpecialAttackLaunched;

	public Vector2 Direction { get; private set; }

	private void Awake()
	{
		actionMap = inputActions.FindActionMap(playerId);

		playerJump = GetComponent<PlayerJump>();
		fighterCombatCore = GetComponent<FighterCombatCore>();

		fighterCombatCore.AttackPerformed += StopAttacking;
	}

	private void OnEnable()
	{
		actionMap.Enable();

		actionMap.FindAction("Move").performed += Move;
		actionMap.FindAction("Move").canceled += StopMoving;
		actionMap.FindAction("Jump").performed += Jump;
		actionMap.FindAction("Launch Neutral Attack").performed += LaunchNeutralAttack;
		actionMap.FindAction("Launch Special Attack").performed += LaunchSpecialAttack;
	}

	private void OnDisable()
	{
		actionMap.Disable();

		actionMap.FindAction("Move").performed -= Move;
		actionMap.FindAction("Move").canceled -= StopMoving;
		actionMap.FindAction("Jump").performed -= Jump;
		actionMap.FindAction("Launch Neutral Attack").performed -= LaunchNeutralAttack;
		actionMap.FindAction("Launch Special Attack").performed -= LaunchSpecialAttack;
	}

	public void SetPlayerId(string playerId)
	{
		this.playerId = playerId;
	}

	public void StartAttacking()
	{
		isAttacking = true;
	}

	public void StopAttacking()
	{
		isAttacking = false;
	}

	private void Move(InputAction.CallbackContext context)
	{
		if (isAttacking)
		{
			return;
		}

		Direction = context.ReadValue<Vector2>();
	}

	private void StopMoving(InputAction.CallbackContext context)
	{
		if (isAttacking)
		{
			return;
		}

		Direction = new Vector2();
	}

	private void Jump(InputAction.CallbackContext context)
	{
		if (isAttacking)
		{
			return;
		}

		playerJump.Jump();
	}

	private void LaunchNeutralAttack(InputAction.CallbackContext context)
	{
		if (isAttacking)
		{
			return;
		}

		Direction = Vector2.zero;

		isAttacking = fighterCombatCore.LaunchAttack();
	}

	private void LaunchSpecialAttack(InputAction.CallbackContext context)
	{
		if (isAttacking)
		{
			return;
		}

		Direction = Vector2.zero;

		SpecialAttackLaunched?.Invoke();
	}
}
