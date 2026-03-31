using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    // Tarea: aplica daño/porcentaje/knockback en radio a jugadores dentro del área.
	// Habla con: FighterDamageReceiver de los afectados y usa OverlapSphere/trigger.
	[SerializeField] private float range;

	private Collider bombCollider;
	private BombProjectile bombProjectile;

	private void Awake()
	{
		bombCollider = GetComponent<Collider>();
		bombProjectile = GetComponent<BombProjectile>();
	}

	public void Explode()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);

		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (hitColliders[i].gameObject.layer == LayerMask.NameToLayer("Fighter"))
			{
				if (hitColliders[i].TryGetComponent(out FighterDamageReceiver damageReceiver))
				{
					damageReceiver.ApplyDamage(bombCollider);
				}
			}
		}

		bombProjectile.Disable();
	}
}
