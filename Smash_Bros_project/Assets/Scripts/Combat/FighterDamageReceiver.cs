using UnityEngine;

[RequireComponent(typeof(FighterPercentKnockback))]
public class FighterDamageReceiver : MonoBehaviour
{
    // Tarea: recibe golpes/proyectiles y aplica “porcentaje + knockback”; punto único de “me han pegado”.
	// Habla con: FighterPercentKnockback y FighterLifeSystem y notifica al HUD cuando cambia el porcentaje.
	private FighterPercentKnockback percentKnockback;

	private float damageReceived;

	public bool IsInvicible { get; set; }

	private void Awake()
	{
		percentKnockback = GetComponent<FighterPercentKnockback>();
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Hitbox"))
		{
			ApplyDamage(collider);
		}
	}

	public void ApplyDamage(Collider collider)
	{
		if (collider.gameObject.TryGetComponent(out HitboxMelee hitbox))
		{
			if (hitbox.IsFighterSelf(transform) || IsInvicible == true)
			{
				return;
			}
			
			Vector2 knockbackDirection = hitbox.GetKnockbackDirection(collider.transform.position);

			damageReceived += hitbox.Damage;

			percentKnockback.ApplyKnockback(knockbackDirection, damageReceived * hitbox.Knockback);

			Debug.Log(damageReceived);
		}
	}

	public void ResetDamage()
	{
		damageReceived = 0;
	}
}
