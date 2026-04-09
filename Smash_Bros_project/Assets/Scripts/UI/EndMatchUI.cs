using UnityEngine;
using TMPro;

public class EndMatchUI : MonoBehaviour
{
    // Tarea: pantalla de resultado (ganador, reiniciar, volver a selección).
	// Habla con: MatchRulesResolver y SceneLoader.
	[SerializeField] private MatchRulesResolver rulesResolver;

	[SerializeField] private GameObject fightersInfo;
	[SerializeField] private GameObject endMatch;

	[SerializeField] private TMP_Text winnerNameText;

	private void Awake()
	{
		fightersInfo.SetActive(true);
		endMatch.SetActive(false);

		rulesResolver.MatchFinished += DisplayEndMenu;
	}

	public void RestartMatch()
	{
		SceneLoader.Instance.LoadMatchScene();
	}

	public void GoBack()
	{
		SceneLoader.Instance.ReturnToPreviousScene();
	}

	private void DisplayEndMenu(GameObject winner)
	{
		if (winner.TryGetComponent(out PlayerFighterController fighterController))
		{
			winnerNameText.text = fighterController.PlayerId;
		}
		
		fightersInfo.SetActive(false);
		endMatch.SetActive(true);
	}
}
