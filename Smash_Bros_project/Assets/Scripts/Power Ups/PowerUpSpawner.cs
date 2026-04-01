using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // Tarea: genera power-ups aleatorios cada X segundos en puntos definidos del mapa.
	// Habla con: PowerUpPickup y usa lista de prefabs configurada.
	[SerializeField] private GameObject[] powerUpPrefabs;
	[SerializeField] private Transform[] spawnPoints;

	[SerializeField] private float spawnTime;
	
	private GameObject[] activePowerUps;

	private float spawnCounter = 0;

	private void Awake()
	{
		activePowerUps = new GameObject[spawnPoints.Length];
	}

	private void Update()
	{
		RunTimer();
	}

	private void RunTimer()
	{
		spawnCounter += Time.deltaTime;

		if (spawnCounter > spawnTime)
		{
			InstantiatePowerUp();
			spawnCounter = 0;
		}
	}

	private void InstantiatePowerUp()
	{
		bool powerUpsAreAvailable = false;

		for (int i = 0; i < activePowerUps.Length; i++)
		{
			if (activePowerUps[i] == null)
			{
				powerUpsAreAvailable = true;
			}
		}

		if (powerUpsAreAvailable == false)
		{
			return;
		}

		int randomIndex = GetAvailableIndex();

		Vector2 randomPosition = spawnPoints[randomIndex].position;
		GameObject randomPowerUp = GetRandomPowerUp();

		activePowerUps[randomIndex] = Instantiate(randomPowerUp, randomPosition, Quaternion.identity);
	}

	private int GetAvailableIndex()
	{
		int randomIndex;
		
		do
		{
			randomIndex = Random.Range(0, spawnPoints.Length);
		}
		while(activePowerUps[randomIndex] != null);

		return randomIndex;
	}

	private GameObject GetRandomPowerUp()
	{
		int randomIndex = Random.Range(0, powerUpPrefabs.Length);
		return powerUpPrefabs[randomIndex];
	}
}
