using UnityEngine;

public class HitboxMelee : MonoBehaviour
{
    // Tarea: hitbox melee (trigger) que aplica un paquete de daño y dirección de empuje al rival.
	// Habla con: FighterDamageReceiver del objetivo y define el “ataque normal” sin animaciones.
	[SerializeField] private Transform fighterTransform;
	[SerializeField] private Vector2 knockbackRatio;

	public float Damage { get; private set; }

	public float Knockback { get; private set; }

	private void Awake()
	{
		knockbackRatio.Normalize();
	}

	public void Initialize(float damage, float knockback)
	{
		Damage = damage;
		Knockback = knockback;
	}

	public void Enable()
	{
		gameObject.SetActive(true);
	}

	public void Disable()
	{
		gameObject.SetActive(false);
	}

	public Vector2 GetKnockbackDirection(Vector3 opponentPosition)
	{
		Vector2 directionToOpponent = opponentPosition - fighterTransform.position;

		directionToOpponent.Normalize();
		
		float directionX;

		if (directionToOpponent.x > 0f)
		{
			directionX = 1f;
		}
		else
		{
			directionX = -1f;
		}

		return new Vector2(knockbackRatio.x * directionX, knockbackRatio.y);
	}
}
