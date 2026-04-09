using System;
using UnityEngine;

public class MatchRulesResolver : MonoBehaviour
{
    // Tarea: decide ganador por vidas (al terminar tiempo) o por KO total; gestiona fin de partida.
	// Habla con: FighterLifeSystem y MatchTimer y activa EndMatchUI.
	[SerializeField] private MatchSetup matchSetup;
	[SerializeField] private MatchTimer matchTimer;

	private GameObject[] fighters;

	public event Action<GameObject> MatchFinished;

	private void Awake()
	{
		fighters = matchSetup.GetFighters();

		for (int i = 0; i < fighters.Length; i++)
		{
			FighterLifeSystem fighterLifeSystem = fighters[i].GetComponent<FighterLifeSystem> ();
			fighterLifeSystem.FighterDefeated += CheckActiveFighters;
		}

		matchTimer.TimeOver += DecideWinner;
		
		Time.timeScale = 1;
	}

	private void CheckActiveFighters()
	{
		GameObject winner = null;

		for (int i = 0; i < fighters.Length; i++)
		{
			if (fighters[i].activeSelf == true)
			{
				if (winner != null)
				{
					return;
				}

				winner = fighters[i];
			}
		}

		EndMatch(winner);
	}

	private void DecideWinner()
	{
		FighterLifeSystem winner = fighters[0].GetComponent<FighterLifeSystem>();

		for (int i = 1; i < fighters.Length; i++)
		{
			FighterLifeSystem fighterLifeSystem = fighters[i].GetComponent<FighterLifeSystem>();

			if (fighterLifeSystem.CurrentLives == winner.CurrentLives)
			{
				FighterDamageReceiver damageReceiver = fighters[i].GetComponent<FighterDamageReceiver>();
				FighterDamageReceiver winnerDamageReceiver = winner.GetComponent<FighterDamageReceiver>();

				if (damageReceiver.DamageReceived < winnerDamageReceiver.DamageReceived)
				{
					winner = fighterLifeSystem;
				}
			}
			else if (fighterLifeSystem.CurrentLives > winner.CurrentLives)
			{
				winner = fighterLifeSystem;
			}
		}

		EndMatch(winner.gameObject);
	}

	private void EndMatch(GameObject winner)
	{
		Time.timeScale = 0;
		MatchFinished?.Invoke(winner);
	}
}
