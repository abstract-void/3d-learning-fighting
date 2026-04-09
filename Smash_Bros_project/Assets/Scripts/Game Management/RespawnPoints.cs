using UnityEngine;

public class RespawnPoints : MonoBehaviour
{
    // Tarea: define puntos de respawn seguros para P1 y P2 en el mapa.
	// Habla con: FighterLifeSystem y devuelve la posición por PlayerId.
	[SerializeField] private Transform[] spawnPoints;

	public Vector3 GetPosition(string playerID)
	{
		if (playerID == "P1")
		{
			return spawnPoints[0].position;
		}
		else if (playerID == "P2")
		{
			return spawnPoints[1].position;
		}

		return Vector2.zero;
	}

	public Quaternion GetRotation(Vector3 position)
	{
		float rotationAngles;
		
		if (position.x < 0)
		{
			rotationAngles = 90;
		}
		else
		{
			rotationAngles = -90;
		}

		Quaternion rotation = Quaternion.Euler(0, rotationAngles, 0);

		return rotation;
	}
}
