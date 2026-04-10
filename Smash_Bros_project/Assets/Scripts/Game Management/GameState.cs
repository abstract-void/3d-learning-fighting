using UnityEngine;

public class GameState : MonoBehaviour
{
    // Tarea: guarda selección pre-combate y reglas base (P1 character, P2 character, mapa seleccionado, vidas, temporizador).
	// Habla con: CharacterSelectUI, StageSelectUI, MatchSetup y SceneLoader, y persiste entre escenas con DontDestroyOnLoad.
	[SerializeField] private float defaultTime;
	[SerializeField] private int defaultLives;

	public static GameState Instance { get; private set; }

	public GameObject P1Prefab { get; set; }

	public GameObject P2Prefab { get; set; }

	public string StageName { get; set; }

	public float TimeLimit { get; set; }

	public int TotalLives { get; set; }

	public void Initialize()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void ResetState()
	{
		P1Prefab = null;
		P2Prefab = null;

		StageName = "";

		TimeLimit = defaultTime * 60;
		TotalLives = defaultLives;
	}
}
