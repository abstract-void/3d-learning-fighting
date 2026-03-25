using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerGroundCheck : MonoBehaviour
{
    // Tarea: detección de suelo para salto y para decidir ataque de tierra vs aire.
	// Habla con: PlayerJump y FighterCombatCore y expone IsGrounded.
	[SerializeField] private float rayLengthFactor;

	private Collider col;

	public bool IsGrounded { get; private set; }

	private void Awake()
	{
		col = GetComponent<Collider>();
	}

	private void FixedUpdate()
	{
		float rayLength = col.bounds.size.y * 0.5f * (1 + rayLengthFactor);

        Physics.Raycast(col.bounds.center, Vector3.down, out RaycastHit hitInfo, rayLength);

		IsGrounded = hitInfo.collider != null;

		Debug.Log(IsGrounded);
	}
}
