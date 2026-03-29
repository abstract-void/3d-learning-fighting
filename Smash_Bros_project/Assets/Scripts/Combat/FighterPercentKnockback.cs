using UnityEngine;

public class FighterPercentKnockback : MonoBehaviour
{
    // Tarea: convierte daño en incremento de porcentaje y calcula fuerza de empuje según porcentaje actual.
	// Habla con: FighterDamageReceiver y aplica la fuerza al Rigidbody del jugador.
	[SerializeField] private float knockbackMultiplier;

	Rigidbody rigidBody;

	Vector2 knockbackForce;

	bool applyKnockback = false;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (applyKnockback == true)
		{
			rigidBody.AddForce(knockbackForce, ForceMode.Impulse);
			applyKnockback = false;
		}
	}

	public void ApplyKnockback(Vector2 direction, float intensity)
	{
		knockbackForce = intensity * knockbackMultiplier * direction;
		applyKnockback = true;
	}
}
