using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Tarea: pantalla inicial con botón para ir a selección.
	// Habla con: SceneLoader y dispara la navegación.
	private void Awake()
	{
		Time.timeScale = 1;
	}

	public void LoadCharacterSelectionScene()
	{
		SceneLoader.Instance.LoadCharacterSelectionScene();
	}

	public void LoadStageSelectionScene()
	{
		SceneLoader.Instance.LoadStageSelectionScene();
	}

	public void ReturnToPreviousScene()
	{
		SceneLoader.Instance.ReturnToPreviousScene();
	}

	public void LoadMatchScene()
	{
		SceneLoader.Instance.LoadMatchScene();
	}

	public void QuitGame()
	{
		SceneLoader.Instance.QuitGame();
	}
}
