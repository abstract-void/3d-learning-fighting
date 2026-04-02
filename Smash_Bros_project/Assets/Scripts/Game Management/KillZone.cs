using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Tarea: detecta salida del escenario y fuerza pérdida de vida (no daño).
	// Habla con: FighterLifeSystem y dispara respawn.
	private void OnTriggerEnter(Collider collider)
	{
		FighterLifeSystem lifeSystem = collider.GetComponent<FighterLifeSystem>();
		lifeSystem.LoseLife();
	}
}
