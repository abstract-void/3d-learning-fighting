using UnityEngine;

public class SingletonGuard : MonoBehaviour
{
    // Tarea: evita duplicados (GameState, SceneLoader, [Extra1 AudioManager]).
	// Habla con: todos los managers y destruye automáticamente cualquier instancia repetida al cargar escenas.
	private static SingletonGuard Instance { get; set; }

	public GameState GameState { get; private set; }

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

			GameState = GetComponent<GameState>();
			GameState.Initialize();
		}
	}
}
