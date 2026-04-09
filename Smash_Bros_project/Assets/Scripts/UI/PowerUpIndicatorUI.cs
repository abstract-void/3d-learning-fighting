using UnityEngine;

public class PowerUpIndicatorUI : MonoBehaviour
{
    // Tarea: muestra power-up activo por jugador (icono o texto).
	// Habla con: PowerUpController y actualiza la UI.
	[SerializeField] private GameObject[] powerUpIndicators;
	[SerializeField] private MatchSetup matchSetup;

	private PowerUpController[] powerUpControllers;

	private void Awake()
	{
		InitializePowerUpControllers();
	}

	private void Update()
	{
		SetIndicators();
	}

	private void InitializePowerUpControllers()
	{
		GameObject[] fighters = matchSetup.GetFighters();

		powerUpControllers = new PowerUpController[fighters.Length];

		for (int i = 0; i < powerUpControllers.Length; i++)
		{
			powerUpControllers[i] = fighters[i].GetComponent<PowerUpController>();
		}
	}

	private void SetIndicators()
	{
		for (int i = 0; i < powerUpControllers.Length; i++)
		{
			if (powerUpControllers[i].PowerUpActive == true)
			{
				powerUpIndicators[i].SetActive(true);
			}
			else
			{
				powerUpIndicators[i].SetActive(false);
			}
		}
	}
}
