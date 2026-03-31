using UnityEngine;

[RequireComponent(typeof(HitboxMelee), typeof(BombExplosion))]
public class BombProjectile : MonoBehaviour
{
    // Tarea: proyectil de bomba con temporizador de detonación o detonación al chocar.
	// Habla con: BombExplosion y decide cuándo explota.
	[SerializeField] private float moveSpeed;
	[SerializeField] private float longevity;
	[SerializeField] private float gravity;

	private Rigidbody rigidBody;
	
	private HitboxMelee hitbox;
	private BombExplosion bombExplosion;

	private Vector3 direction;

	private float lifespan = 0f;

    public float angle;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		bombExplosion = GetComponent<BombExplosion>();
		
		gameObject.SetActive(false);
	}

	private void FixedUpdate()
	{
		ApplyGravity();
	}

	private void Update()
	{
		lifespan += Time.deltaTime;

		if (lifespan > longevity)
		{
			bombExplosion.Explode();
			Disable();
		}
	}

	private void Launch()
	{
        rigidBody.AddForce(moveSpeed * direction, ForceMode.Impulse);
	}

	private void ApplyGravity()
	{
		rigidBody.AddForce(Vector3.up * gravity);
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
		
		lifespan = 0;

		gameObject.SetActive(true);

		Launch();
	}

	public void Disable()
	{
		gameObject.SetActive(false);
		
		direction = Vector2.zero;
		lifespan = 0;
		
		rigidBody.linearVelocity = new Vector3();

		transform.position = new Vector3(1000f, 1000f, 1000f);
	}
}
