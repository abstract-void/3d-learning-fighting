using System;
using UnityEngine;

public class FighterLifeSystem : MonoBehaviour
{
    // Tarea: vidas (3), perder vida al caer y respawn.
	// Habla con: KillZone, RespawnPoints y MatchRulesResolver, y notifica a HUD las vidas restantes.
	[SerializeField] private float invincibilityTime;

	private RespawnPoints respawnPoints;

	private PlayerFighterController fighterController;
	private FighterDamageReceiver damageReceiver;

	private float currentLives;

	private float invincibilityCounter;

	private bool hasInvincibility;

	public event Action FighterDefeated;

	public float CurrentLives => currentLives;

	private void Update()
	{
		if (hasInvincibility == true)
		{
			RunTimer();
		}
	}

	public void Initialize(RespawnPoints respawnPoints, int totalLives)
	{
		fighterController = GetComponent<PlayerFighterController>();
		damageReceiver = GetComponent<FighterDamageReceiver>();

		this.respawnPoints = respawnPoints;
		
		currentLives = totalLives;
	}

	public void LoseLife()
	{
		currentLives--;

		if (currentLives > 0)
		{
			transform.position = respawnPoints.GetPosition(fighterController.PlayerId);

			damageReceiver.IsInvicible = true;
			damageReceiver.ResetDamage();

			invincibilityCounter = 0;

			hasInvincibility = true;
		}
		else
		{
			gameObject.SetActive(false);
			FighterDefeated?.Invoke();
		}
	}

	private void RunTimer()
	{
		invincibilityCounter += Time.deltaTime;

		if (invincibilityCounter > invincibilityTime)
		{
			damageReceiver.IsInvicible = false;
			hasInvincibility = false;
		}
	}
}
