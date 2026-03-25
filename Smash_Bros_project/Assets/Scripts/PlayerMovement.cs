using UnityEngine;

[RequireComponent(typeof(PlayerFighterController), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	// Tarea: movimiento izquierda/derecha en plano lateral.
	// Habla con: PlayerFighterController y aplica desplazamiento según el input asignado a su PlayerId.
	[SerializeField] private float moveSpeed;

	private PlayerFighterController controller;

	private Rigidbody rigidBody;

	private void Awake()
	{
		controller = GetComponent<PlayerFighterController>();
		rigidBody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rigidBody.AddForce(moveSpeed * controller.Direction);
	}
}
