using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Tarea: carga escenas de forma asíncrona para pasar de selección a combate sin perder el estado.
	// Habla con: GameState y expone métodos de carga (LoadSelectionScene, LoadMatchScene).
	[SerializeField] private string mainMenuScene;
	[SerializeField] private string stageSelectionScene;
	[SerializeField] private string characterSelectionScene;
	[SerializeField] private string matchScene;

	public static SceneLoader Instance { get; private set; }

	public void Initialize()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void LoadMainMenuScene()
	{
		SceneManager.LoadScene(mainMenuScene);
	}

	public void LoadCharacterSelectionScene()
	{
		if (!SceneIsLoaded(characterSelectionScene))
		{
			StartCoroutine(LoadSceneAdditive(characterSelectionScene));
		}
	}

	public void LoadStageSelectionScene()
	{
		if (!SceneIsLoaded(stageSelectionScene))
		{
			StartCoroutine(LoadSceneAdditive(stageSelectionScene));
		}
	}

	public void LoadMatchScene()
	{
		SceneManager.LoadScene(matchScene);
	}

	public void ReturnToPreviousScene()
	{
		if (SceneIsLoaded(stageSelectionScene))
		{
			StartCoroutine(UnloadScene(stageSelectionScene));
		}
		else if (SceneIsLoaded(characterSelectionScene))
		{
			StartCoroutine(UnloadScene(characterSelectionScene));
		}
	}

	public void QuitGame()
	{
		Debug.Log("Quit game");
		Application.Quit();
	}

	public bool SceneIsLoaded(string sceneName)
	{
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			if (sceneName == SceneManager.GetSceneAt(i).name)
			{
				return true;
			}
		}

		return false;
	}

	IEnumerator LoadSceneAdditive(string sceneName)
	{
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}

	IEnumerator UnloadScene(string sceneName)
	{
		yield return SceneManager.UnloadSceneAsync(sceneName);
	}
}
