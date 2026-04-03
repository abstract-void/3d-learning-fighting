using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    // Tarea: muestra porcentaje y vidas de P1/P2, indicador de power-up activo y temporizador.
	// Habla con: FighterDamageReceiver (porcentaje), FighterLifeSystem (vidas), PowerUpController (indicador) y MatchTimer.
	[SerializeField] private PlayerInfo[] playersInfo;
	[SerializeField] private TMP_Text timeLeftText;
	[SerializeField] private MatchTimer matchTimer;
	[SerializeField] private MatchSetup matchSetup;

	private FighterLifeSystem[] fighterLifeSystems;
	private FighterDamageReceiver[] fighterDamageReceivers;

	private void Start()
	{
		GameObject[] fighters = matchSetup.GetFighters();

		fighterLifeSystems = new FighterLifeSystem[fighters.Length];
		fighterDamageReceivers = new FighterDamageReceiver[fighters.Length];

		for (int i = 0; i < playersInfo.Length; i++)
		{
			if (i >= fighters.Length)
			{
				playersInfo[i].Disable();
				continue;
			}

			PlayerFighterController fighterController = fighters[i].GetComponent<PlayerFighterController>();

			fighterLifeSystems[i] = fighters[i].GetComponent<FighterLifeSystem>();
			fighterDamageReceivers[i] = fighters[i].GetComponent<FighterDamageReceiver>();

			playersInfo[i].SetPlayer(fighterController.PlayerId);
			playersInfo[i].SetLives(fighterLifeSystems[i].CurrentLives);
			playersInfo[i].SetDamage(fighterDamageReceivers[i].DamageReceived);
		}

		timeLeftText.text = GetDisplayTime();
	}

	private void Update()
	{
		timeLeftText.text = GetDisplayTime();

		UpdateLives();
		UpdateDamage();
	}

	private string GetDisplayTime()
	{
		float timeLeft = matchTimer.TimeLeft;

		int minutes = (int)(timeLeft / 60);
		int seconds = (int)(timeLeft % 60);

		return $"{minutes}:{seconds:D2}";
	}

	private void UpdateLives()
	{
		for (int i = 0; i < fighterLifeSystems.Length; i++)
		{
			playersInfo[i].SetLives(fighterLifeSystems[i].CurrentLives);
		}
	}

	private void UpdateDamage()
	{
		for (int i = 0; i < fighterDamageReceivers.Length; i++)
		{
			playersInfo[i].SetDamage(fighterDamageReceivers[i].DamageReceived);
		}
	}

	[System.Serializable]
	private class PlayerInfo
	{
		[SerializeField] private TMP_Text playerText;
		[SerializeField] private TMP_Text damageText;
		[SerializeField] private TMP_Text livesText;

		public void SetPlayer(string playerId)
		{
			playerText.text = playerId;
		}

		public void SetDamage(float damage)
		{
			damageText.text = $"{damage}%";
		}

		public void SetLives(int lives)
		{
			livesText.text = lives.ToString();
		}

		public void Disable()
		{
			playerText.gameObject.SetActive(false);
			damageText.gameObject.SetActive(false);
			livesText.gameObject.SetActive(false);
		}
	}
}
