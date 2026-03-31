using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerFighterController))]
public class BlasterSpecial : MonoBehaviour
{
    // Tarea: especial core de Fox: blaster que dispara un proyectil lineal con cooldown.
	// Habla con: PlayerFighterController y ProjectileStraight y aplica daño/porcentaje al impactar.
	[SerializeField] private GameObject blasterPrefab;
	[SerializeField] private FighterStats fighterStats;

	private PlayerFighterController fighterController;

	private BlasterProjectile blasterProjectile;

	private Coroutine attackCoroutine;

	private void Awake()
	{
		fighterController = GetComponent<PlayerFighterController>();

		GameObject blasterObject = Instantiate(blasterPrefab, new Vector3(1000f, 1000f, 1000f), blasterPrefab.transform.rotation);

		blasterProjectile = blasterObject.GetComponent<BlasterProjectile>();
		blasterProjectile.InitializeHitbox(transform, fighterStats.SpecialAttackBaseDamage, fighterStats.SpecialAttackKnockback);

		fighterController.SpecialAttackLaunched += LaunchAttack;
	}

	private void LaunchAttack()
	{
		if (attackCoroutine == null)
		{
			attackCoroutine = StartCoroutine(Attack());
		}
	}

	private IEnumerator Attack()
	{
		yield return new WaitForSeconds(fighterStats.SpecialAttackStartupTime);

		Vector3 offsetPosition = transform.forward * transform.localScale.z;

		blasterProjectile.Enable(transform.position + offsetPosition, transform.forward);
		fighterController.StartAttacking();
		
		yield return new WaitForSeconds(fighterStats.SpecialAttackPerformTime);

		fighterController.StopAttacking();

		yield return new WaitForSeconds(fighterStats.SpecialAttackRecoveryTime);

		yield return new WaitForSeconds(fighterStats.SpecialAttackCooldown);

		attackCoroutine = null;
	}
}
