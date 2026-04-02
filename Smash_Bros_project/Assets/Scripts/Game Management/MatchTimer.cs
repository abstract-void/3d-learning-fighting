using System;
using UnityEngine;

public class MatchTimer : MonoBehaviour
{
    // Tarea: temporizador (cuenta atrás o cronómetro) y evento “tiempo terminado”.
	// Habla con: HUDController y MatchRulesResolver.
	private float timeLimit;
	private float currentTime;

	public event Action TimeOver;

	private void Awake()
	{
		timeLimit = GameState.Instance.TimeLimit;
		currentTime = timeLimit;
	}

	private void Update()
	{
		RunTimer();
	}

	private void RunTimer()
	{
		currentTime -= Time.deltaTime;

		if (currentTime <= 0)
		{
			TimeOver?.Invoke();
		}
	}
}
