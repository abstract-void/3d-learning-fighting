using UnityEngine;

public class SingletonGuard : MonoBehaviour
{
    // Tarea: evita duplicados (GameState, SceneLoader, [Extra1 AudioManager]).
	// Habla con: todos los managers y destruye automáticamente cualquier instancia repetida al cargar escenas.
	[SerializeField] private GameObject p1Prefab; // Just for debugging purposes
	[SerializeField] private GameObject p2Prefab; // Just for debugging purposes

	private static SingletonGuard Instance { get; set; }

	private GameState gameState;
	private SceneLoader sceneLoader;

	private void Awake()
	{
		Initialize();
	}

	private void Initialize()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;

			gameState = GetComponent<GameState>();
			gameState.Initialize();

			sceneLoader = GetComponent<SceneLoader>();
			sceneLoader.Initialize();

			// Just for debugging purposes
			if (sceneLoader.SceneIsLoaded("MatchScene"))
			{
				SetDefaultGameState();
			}
		}
	}

	// Just for debugging purposes
	private void SetDefaultGameState()
	{
		GameState.Instance.P1Prefab = p1Prefab;
		GameState.Instance.P2Prefab = p2Prefab;

		GameState.Instance.StageName = "MatchScene";

		GameState.Instance.TimeLimit = 180;
		GameState.Instance.TotalLives = 3;
	}
}
