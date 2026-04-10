using UnityEngine;

public class StageSelectUI : MonoBehaviour
{
    // Tarea: selección de mapa (solo 1 disponible, sistema preparado).
	// Habla con: GameState y SceneLoader y confirma la arena.
	public void SelectStage(string stageName)
	{
		GameState.Instance.StageName = stageName;
	}
}
