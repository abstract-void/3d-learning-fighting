using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HitboxMelee))]
public class BlasterProjectile : MonoBehaviour
{
    // Tarea: proyectil lineal (blaster) que aplica daño/porcentaje y desaparece al impactar.
	// Habla con: FighterDamageReceiver y se destruye/recicla.
	[SerializeField] private float moveSpeed;
	[SerializeField] private float longevity;

	private Rigidbody rigidBody;
	private HitboxMelee hitbox;

	private Vector2 direction;

	private float lifespan = 0f;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		rigidBody.AddForce(moveSpeed * direction, ForceMode.Impulse);
	}

	private void Update()
	{
		lifespan += Time.deltaTime;

		if (lifespan > longevity)
		{
			Disable();
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Fighter"))
		{
			if (hitbox.IsFighterSelf(collider.transform))
			{
				return;
			}

			Disable();
		}
	}

	public void InitializeHitbox(Transform fighterTransform, float damage, float knockback)
	{
		hitbox = GetComponent<HitboxMelee>();
		hitbox.Initialize(fighterTransform, damage, knockback);
	}

	public void Enable(Vector2 initialPosition, Vector2 direction)
	{
		rigidBody.linearVelocity = new Vector3();

		transform.position = initialPosition;

		this.direction = direction;

		gameObject.SetActive(true);
	}

	public void Disable()
	{
		direction = Vector2.zero;
		lifespan = 0;
		
		rigidBody.linearVelocity = new Vector3();

		StartCoroutine(Teleport());
	}

	IEnumerator Teleport()
	{
		yield return new WaitForSeconds(0.01f);

		lifespan = 0;

		transform.position = new Vector3(1000f, 1000f, 1000f);

		gameObject.SetActive(false);
	}
}
