using System;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(PlayerGroundCheck))]
public class FighterCombatCore : MonoBehaviour
{
    // Tarea: ataques normales (suelo y aire) con cooldown; sin ataque fuerte aquí.
	// Habla con: PlayerFighterController, HitboxMelee y PlayerGroundCheck y decide cuál ejecutar según IsGrounded.
	[SerializeField] FighterStats fighterStats;

	[SerializeField] HitboxMelee groundHitbox;
	[SerializeField] HitboxMelee airHitbox;

	private PlayerGroundCheck groundChecker;

	private Coroutine attackRoutine;

	public event Action AttackStarted;
	public event Action AttackPerformed;
	public event Action AttackFinished;

	private void Awake()
	{
		groundChecker = GetComponent<PlayerGroundCheck>();
	}

	public bool LaunchAttack()
	{
		if (attackRoutine != null)
		{
			return false;
		}

		if (groundChecker.IsGrounded)
		{
			IEnumerator attack = Attack(groundHitbox, fighterStats.GroundAttackStartupTime, fighterStats.GroundAttackPerformTime, fighterStats.GroundAttackRecoveryTime, fighterStats.GroundAttackCooldown);
			attackRoutine = StartCoroutine(attack);
		}
		else
		{
			IEnumerator attack = Attack(airHitbox, fighterStats.AirAttackStartupTime, fighterStats.AirAttackPerformTime, fighterStats.AirAttackRecoveryTime, fighterStats.AirAttackCooldown);
			attackRoutine = StartCoroutine(attack);
		}

		return true;
	}

	private IEnumerator Attack(HitboxMelee hitbox, float startupTime, float performTime, float recoveryTime, float cooldown)
	{
		AttackStarted?.Invoke();

		yield return new WaitForSeconds(startupTime);

		hitbox.Enable();

		yield return new WaitForSeconds(performTime);

		hitbox.Disable();

		AttackPerformed?.Invoke();

		yield return new WaitForSeconds(recoveryTime);

		AttackFinished?.Invoke();

		yield return new WaitForSeconds(cooldown);

		attackRoutine = null;
	}
}
