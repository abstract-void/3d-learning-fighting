using UnityEngine;

[CreateAssetMenu(fileName = "FighterStats", menuName = "Scriptable Objects/FighterStats")]
public class FighterStats : ScriptableObject
{
    // Tarea: datos por personaje (velocidad, daño base, multiplicadores, rangos, cooldowns, masa/knockback scale).
	// Habla con: PlayerFighterController y FighterCombatCore y permite variar Fox/Link sin nuevos controladores.
	[Header("Ground Attack")]

	[field: SerializeField] public float GroundAttackStartupTime { get; private set; }

	[field: SerializeField] public float GroundAttackPerformTime { get; private set; }

	[field: SerializeField] public float GroundAttackRecoveryTime { get; private set; }

	[field: SerializeField] public float GroundAttackBaseDamage { get; private set; }

	[field: SerializeField] public float GroundAttackKnockback { get; private set; }

	[field: SerializeField] public float GroundAttackCooldown { get; private set; }
	
	[Header("Air Attack")]

	[field: SerializeField] public float AirAttackStartupTime { get; private set; }

	[field: SerializeField] public float AirAttackPerformTime { get; private set; }

	[field: SerializeField] public float AirAttackRecoveryTime { get; private set; }

	[field: SerializeField] public float AirAttackBaseDamage { get; private set; }

	[field: SerializeField] public float AirAttackKnockback { get; private set; }

	[field: SerializeField] public float AirAttackCooldown { get; private set; }
}
