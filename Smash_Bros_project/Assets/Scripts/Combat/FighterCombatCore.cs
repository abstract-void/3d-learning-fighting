using System;
using System.Collections;

using UnityEngine;

public class FighterCombatCore : MonoBehaviour
{
    // Tarea: ataques normales (suelo y aire) con cooldown; sin ataque fuerte aquí.
	// Habla con: PlayerFighterController, HitboxMelee y PlayerGroundCheck y decide cuál ejecutar según IsGrounded.

	public event Action AttackPerformed;

	public void LaunchAttack()
	{
		Debug.Log("Neutral attack");
		StartCoroutine(TestAttack());
	}

	private IEnumerator TestAttack()
	{
		yield return new WaitForSeconds(1f);
		AttackPerformed?.Invoke();
		Debug.Log("Neutral attack stopped");
	}
}
