using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerGroundCheck))]
public class PlayerJump : MonoBehaviour
{
    // Tarea: salto y doble salto.
	// Habla con: PlayerGroundCheck y controla contador de saltos disponibles.
	[SerializeField] private float jumpImpulse;
	[SerializeField] private float gravity;

	[SerializeField] private int maxJumps;

	private Rigidbody rigidBody;
	private PlayerGroundCheck groundChecker;

	private int jumpCounter;
	private bool canJump;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		groundChecker = GetComponent<PlayerGroundCheck>();
	}

	private void FixedUpdate()
	{
		if (canJump)
		{
			ApplyJump();
		}

		ApplyGravity();

		if (groundChecker.IsGrounded)
		{
			jumpCounter = 0;
		}
	}

	private void ApplyJump()
	{
		rigidBody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
		canJump = false;
	}

	private void ApplyGravity()
	{
		rigidBody.AddForce(Vector3.up * gravity);
	}

	public void Jump()
	{
		if (groundChecker.IsGrounded)
		{
			jumpCounter = 1;
		}
		else
		{
			jumpCounter++;
		}
		
		if (jumpCounter < maxJumps)
		{
			canJump = true;
		}
	}
}
