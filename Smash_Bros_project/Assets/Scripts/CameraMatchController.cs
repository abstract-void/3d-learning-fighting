using UnityEngine;

public class CameraMatchController : MonoBehaviour
{
    // Tarea: cámara para 2 jugadores (seguir centro entre ambos; zoom simple opcional).
	// Habla con: MatchSetup y lee posiciones de P1/P2.
	[SerializeField] private MatchSetup matchSetup;
	[SerializeField] private Vector3 basePosition;
	[SerializeField] private Vector3 distanceMultiplier;
	[SerializeField] private Vector3 maxPosition;
	[SerializeField] private Vector3 minPosition;
	[SerializeField] private float speed;

	private GameObject[] fighters;

	private Camera cam;

	private void Start()
	{
		cam = GetComponent<Camera>();
		fighters = matchSetup.GetFighters();

		cam.transform.position = GetGoalPosition();
	}

	private void LateUpdate ()
	{
		Vector3 goalPosition = GetGoalPosition();
		cam.transform.position = Vector3.Lerp(cam.transform.position, goalPosition, speed * Time.deltaTime);
	}

	private Vector3 GetGoalPosition()
	{
		Vector2 meanPosition = GetMeanPosition();
		float maxFightersDistance = GetMaxFightersDistance();

		float positionX = basePosition.x + meanPosition.x;
		float positionY = basePosition.y + meanPosition.y + maxFightersDistance * distanceMultiplier.y;
		float positionZ = basePosition.z - maxFightersDistance * distanceMultiplier.z;

		positionX = Mathf.Clamp(positionX, minPosition.x, maxPosition.x);
		positionY = Mathf.Clamp(positionY, minPosition.y, maxPosition.y);
		positionZ = Mathf.Clamp(positionZ, minPosition.z, maxPosition.z);

		return new Vector3(positionX, positionY, positionZ);
	}

	private Vector3 GetMeanPosition()
	{
		Vector3 addedPositions = Vector3.zero;

		int totalFighters = 0;

		for (int i = 0; i < fighters.Length; i++)
		{
			if (fighters[i].activeSelf == true)
			{
				addedPositions += fighters[i].transform.position;
				totalFighters++;
			}
		}

		if (totalFighters == 0)
		{
			totalFighters = 1;
		}

		return addedPositions / totalFighters;
	}

	private float GetMaxFightersDistance()
	{
		float maxDistance = 0;

		for (int i = 0; i < fighters.Length; i++)
		{
			int nextIndex;
			
			if (i + 1 < fighters.Length)
			{
				nextIndex = i + 1;
			}
			else
			{
				nextIndex = 0;
			}

			
			if (fighters[i].activeSelf == true && fighters[nextIndex].activeSelf == true)
			{
				float distance = Vector3.Distance(fighters[i].transform.position, fighters[nextIndex].transform.position);

				if (distance > maxDistance)
				{
					maxDistance = distance;
				}
			}
		}

		return maxDistance;
	}
}
