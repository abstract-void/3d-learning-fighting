using UnityEngine;

[RequireComponent(typeof(PlayerFighterController), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	// Tarea: movimiento izquierda/derecha en plano lateral.
	// Habla con: PlayerFighterController y aplica desplazamiento según el input asignado a su PlayerId.
	[SerializeField] private float moveSpeed;
	[SerializeField] private float rotationDamp;

	private PlayerFighterController controller;

	private Rigidbody rigidBody;

	private void Awake()
	{
		controller = GetComponent<PlayerFighterController>();
		rigidBody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Move();
		Rotate();
	}

	private void Move()
	{
		rigidBody.AddForce(moveSpeed * controller.Direction);
	}

	private void Rotate()
	{
		if (controller.Direction.magnitude > 0)
		{
			rigidBody.MoveRotation(Quaternion.Euler(0, 90 * controller.Direction.x, 0));
		}
	}
}
