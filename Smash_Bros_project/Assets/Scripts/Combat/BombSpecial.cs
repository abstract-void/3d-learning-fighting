using System.Collections;
using UnityEngine;

public class BombSpecial : MonoBehaviour
{
    // Tarea: especial core de Link: lanza bomba que explota por tiempo o colisión.
	// Habla con: PlayerFighterController y BombProjectile/BombExplosion y aplica daño/knockback en área.
	[SerializeField] private GameObject bombPrefab;
	[SerializeField] private FighterStats fighterStats;

	private PlayerFighterController fighterController;

	private BombProjectile bombProjectile;

	private Coroutine attackCoroutine;

	private void Awake()
	{
		fighterController = GetComponent<PlayerFighterController>();

		GameObject blasterObject = Instantiate(bombPrefab, new Vector3(1000f, 1000f, 1000f), bombPrefab.transform.rotation);

		bombProjectile = blasterObject.GetComponent<BombProjectile>();
		bombProjectile.InitializeHitbox(transform, fighterStats.SpecialAttackBaseDamage, fighterStats.SpecialAttackKnockback);

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

		bombProjectile.Enable(transform.position + offsetPosition, transform.forward);
		fighterController.StartAttacking();
		
		yield return new WaitForSeconds(fighterStats.SpecialAttackPerformTime);

		fighterController.StopAttacking();

		yield return new WaitForSeconds(fighterStats.SpecialAttackRecoveryTime);

		yield return new WaitForSeconds(fighterStats.SpecialAttackCooldown);

		attackCoroutine = null;
	}
}
