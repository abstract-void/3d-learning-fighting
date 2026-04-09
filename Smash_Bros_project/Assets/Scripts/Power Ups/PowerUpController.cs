using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Tarea: gestiona “un power-up activo” por jugador con duración; aplica y revierte stats/flags.
	// Habla con: FighterStats (multiplicadores), HUDController/PowerUpIndicatorUI y revierte al terminar.
	private FighterDamageReceiver damageReceiver;

	private string powerUpType;

	private float powerUpLength;
	private float counter;

	public bool PowerUpActive { get; private set; }

	private void Awake()
	{
		damageReceiver = GetComponent<FighterDamageReceiver>();
	}

	private void Update()
	{
		if (PowerUpActive == true)
		{
			RunTimer();
		}
	}

	private void RunTimer()
	{
		counter += Time.deltaTime;

		if (counter > powerUpLength)
		{
			RemovePowerUpEffect(powerUpType);
		}
	}

	public void ApplyPowerUpEffect(string type, float length)
	{
		if (type == "Invincibility")
		{
			if (damageReceiver.IsInvicible == true)
			{
				return;
			}

			damageReceiver.IsInvicible = true;
		}

		powerUpLength = length;
		powerUpType = type;

		counter = 0;

		PowerUpActive = true;
	}

	private void RemovePowerUpEffect(string type)
	{
		if (type == "Invincibility")
		{
			damageReceiver.IsInvicible = false;
		}

		PowerUpActive = false;
	}
}
