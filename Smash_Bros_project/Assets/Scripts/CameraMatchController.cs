using UnityEngine;

public class CameraMatchController : MonoBehaviour
{
    // Tarea: cámara para 2 jugadores (seguir centro entre ambos; zoom simple opcional).
	// Habla con: MatchSetup y lee posiciones de P1/P2.
	[SerializeField] private MatchSetup matchSetup;
	[SerializeField] private Vector3 basePosition;
	[SerializeField] private Vector3 distanceMultiplier;
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

		return new Vector3(positionX, positionY, positionZ);
	}

	private Vector3 GetMeanPosition()
	{
		Vector3 addedPositions = Vector3.zero;

		for (int i = 0; i < fighters.Length; i++)
		{
			addedPositions += fighters[i].transform.position;
		}

		return addedPositions / fighters.Length;
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

			float distance = Vector3.Distance(fighters[i].transform.position, fighters[nextIndex].transform.position);

			if (distance > maxDistance)
			{
				maxDistance = distance;
			}
		}

		return maxDistance;
	}
}
