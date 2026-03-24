using UnityEngine;

[RequireComponent(typeof(PlayerFighterController))]
public class PlayerMovement : MonoBehaviour
{
	// Tarea: movimiento izquierda/derecha en plano lateral.
	// Habla con: PlayerFighterController y aplica desplazamiento según el input asignado a su PlayerId.
	private PlayerFighterController controller;

	private void Awake()
	{
		controller = GetComponent<PlayerFighterController>();
	}

	private void FixedUpdate()
	{
		Debug.Log($"{controller.PlayerId}: {controller.Direction}");
	}
}
