using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MainMenuUI : MonoBehaviour
{
    // Tarea: pantalla inicial con botón para ir a selección.
	// Habla con: SceneLoader y dispara la navegación.
	[SerializeField] private GameObject characterBlockingPanel;
	[SerializeField] private GameObject stageBlockingPanel;

	[SerializeField] private Button nextButton;
	
	[SerializeField] private TMP_Text nextText;
	[SerializeField] private TMP_Text previousText;

	private void Awake()
	{
		GameState.Instance.ResetState();
		Time.timeScale = 1;
	}

	private void Update()
	{
		EnableStartButton();
		DisableSelections();
	}

	public void Next()
	{
		previousText.text = "Previous";

		if (SceneLoader.Instance.SceneIsLoaded("CharacterSelectionScene") == false)
		{
			SceneLoader.Instance.LoadCharacterSelectionScene();

			EnableCharacterSelection();
			
			nextButton.interactable = false;

			return;
		}

		if (SceneLoader.Instance.SceneIsLoaded("StageSelectionScene") == false)
		{
			SceneLoader.Instance.LoadStageSelectionScene();

			EnableStageSelection();

			nextText.text = "Start";
			
			nextButton.interactable = false;

			return;
		}
		
		SceneLoader.Instance.LoadMatchScene();
	}

	public void Previous()
	{
		if (SceneLoader.Instance.SceneIsLoaded("CharacterSelectionScene") == false)
		{
			SceneLoader.Instance.QuitGame();
			return;
		}

		SceneLoader.Instance.ReturnToPreviousScene();
		
		if (SceneLoader.Instance.SceneIsLoaded("CharacterSelectionScene") == true)
		{
			EnableCharacterSelection();
		}

		nextText.text = "Next";
	}

	public void UpdateTimeLimit(float timeLimit)
	{
		GameState.Instance.TimeLimit = timeLimit * 60;
	}

	public void UpdateLives(float lives)
	{
		GameState.Instance.TotalLives = (int)lives;
	}

	private void EnableCharacterSelection()
	{
		characterBlockingPanel.SetActive(false);
		stageBlockingPanel.SetActive(true);
	}

	private void EnableStageSelection()
	{
		characterBlockingPanel.SetActive(true);
		stageBlockingPanel.SetActive(false);
	}

	private void DisableSelections()
	{
		if (SceneLoader.Instance.SceneIsLoaded("CharacterSelectionScene") == true)
		{
			return;
		}

		characterBlockingPanel.SetActive(true);
		stageBlockingPanel.SetActive(true);

		previousText.text = "Exit";
	}

	private void EnableStartButton()
	{
		if (SceneLoader.Instance.SceneIsLoaded("StageSelectionScene") == false)
		{
			if (SceneLoader.Instance.SceneIsLoaded("CharacterSelectionScene") == false)
			{
				nextButton.interactable = true;
			}
			else
			{
				if (GameState.Instance.P1Prefab != null && GameState.Instance.P2Prefab != null)
				{
					nextButton.interactable = true;
				}
			}
		}
		else
		{
			if (string.IsNullOrEmpty(GameState.Instance.StageName) == false)
			{
				nextButton.interactable = true;
			}
		}
	}
}
