using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    // Tarea: gestiona “un power-up activo” por jugador con duración; aplica y revierte stats/flags.
	// Habla con: FighterStats (multiplicadores), HUDController/PowerUpIndicatorUI y revierte al terminar.
	private FighterDamageReceiver damageReceiver;

	private string powerUpType;

	private float powerUpLength;
	private float counter;

	private bool powerUpActive;

	private void Awake()
	{
		damageReceiver = GetComponent<FighterDamageReceiver>();
	}

	private void Update()
	{
		if (powerUpActive == true)
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
			powerUpActive = false;
		}
	}

	public void ApplyPowerUpEffect(string type, float length)
	{
		if (type == "Invincibility")
		{
			damageReceiver.IsInvicible = true;
		}

		powerUpLength = length;
		powerUpType = type;

		counter = 0;

		powerUpActive = true;
	}

	private void RemovePowerUpEffect(string type)
	{
		if (type == "Invincibility")
		{
			damageReceiver.IsInvicible = false;
		}
	}
}
