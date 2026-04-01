using UnityEngine;

public class GameState : MonoBehaviour
{
    // Tarea: guarda selección pre-combate y reglas base (P1 character, P2 character, mapa seleccionado, vidas, temporizador).
	// Habla con: CharacterSelectUI, StageSelectUI, MatchSetup y SceneLoader, y persiste entre escenas con DontDestroyOnLoad.
	[SerializeField] private GameObject p1Prefab;
	[SerializeField] private GameObject p2Prefab;

	[SerializeField] private string sceneName;

	[SerializeField] private float totalTime;
	[SerializeField] private int totalLives;

	public static GameState Instance { get; private set; }

	public GameObject P1Prefab => p1Prefab;

	public GameObject P2Prefab => p2Prefab;

	public string SceneName => sceneName;

	public float TotalTime => totalTime;

	public int TotalLives => totalLives;

	public void Initialize()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}
