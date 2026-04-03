using System;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    // Tarea: temporizador (cuenta atrás o cronómetro) y evento “tiempo terminado”.
	// Habla con: HUDController y MatchRulesResolver.
	private float timeLimit;

	public event Action TimeOver;

	public float TimeLeft { get; private set; }

	private void Awake()
	{
		timeLimit = GameState.Instance.TimeLimit;
		TimeLeft = timeLimit;
	}

	private void Update()
	{
		RunTimer();
	}

	private void RunTimer()
	{
		TimeLeft -= Time.deltaTime;

		if (TimeLeft <= 0)
		{
			TimeOver?.Invoke();
		}
	}
}
