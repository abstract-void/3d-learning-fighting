using UnityEngine;

public class CharacterSelectUI : MonoBehaviour
{
    // Tarea: selección de personaje para P1 y P2 (Fox/Link) y confirmación.
	// Habla con: GameState y StageSelectUI y escribe la selección.
	public void SelectCharacter(GameObject characterPrefab)
	{
		if (GameState.Instance.P1Prefab == null)
		{
			GameState.Instance.P1Prefab = characterPrefab;
		}
		else
		{
			GameState.Instance.P2Prefab = characterPrefab;
		}
	}
}
