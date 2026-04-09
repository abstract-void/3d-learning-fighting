using UnityEngine;

public class SingletonGuard : MonoBehaviour
{
    // Tarea: evita duplicados (GameState, SceneLoader, [Extra1 AudioManager]).
	// Habla con: todos los managers y destruye automáticamente cualquier instancia repetida al cargar escenas.
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
		}
	}
}
