using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    // Tarea: trigger de recogida; al tocarlo aplica efecto al jugador y desaparece.
	// Habla con: PowerUpController del jugador y le pasa tipo/duración.
	[SerializeField] private string type;
	[SerializeField] private float length;

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Fighter"))
		{
			if (collider.TryGetComponent(out PowerUpController powerUpController))
			{
				powerUpController.ApplyPowerUpEffect(type, length);
				Destroy(gameObject);
			}
		}
	}
}
