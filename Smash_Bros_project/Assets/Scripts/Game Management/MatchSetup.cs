using UnityEngine;

public class MatchSetup : MonoBehaviour
{
    // Tarea: instancia los prefabs elegidos en spawn P1/P2 y les asigna su “PlayerId” (P1 o P2) y referencias comunes.
	// Habla con: GameState, PlayerFighterController y CameraMatchController, y asegura que HUD y cámara apuntan a los jugadores correctos.
	[SerializeField] private RespawnPoints respawnPoints;

	private void Awake()
	{
		InstantiateFighter(GameState.Instance.P1Prefab, "P1");
		InstantiateFighter(GameState.Instance.P2Prefab, "P2");
	}

	private void InstantiateFighter(GameObject fighterPrefab, string playerId)
	{
		Vector3 position = respawnPoints.GetPosition(playerId);
		float rotationAngles = 90 * position.normalized.x;
		Quaternion rotation = Quaternion.Euler(0, rotationAngles, 0);

		GameObject fighterObj = Instantiate(fighterPrefab, position, rotation);
		PlayerFighterController fighterController = fighterObj.GetComponent<PlayerFighterController>();

		fighterController.SetPlayerId(playerId);

		fighterObj.SetActive(true);
	}
}
