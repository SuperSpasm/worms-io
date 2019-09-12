using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;
	
    private bool _grounded;
	public Transform center;
	private float _gravityMagnitude = 9.8f;

    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
	    Vector2 gravityDirection = center.position - transform.position;
	    Vector2 velocityDown = Vector2.zero;
	    Vector2 velocitySide;
	    
		float moveInput = Input.GetAxisRaw("Horizontal");
		
		// jump
		if (_grounded)
		{
			velocityDown = Vector2.zero;
			
			if (Input.GetButtonDown("Jump"))
			{
				// Calculate the velocity required to achieve the target jump height.
				velocityDown = Mathf.Sqrt(jumpHeight) * -gravityDirection.normalized;
			}
		}
		else
		{
			velocityDown = gravityDirection.normalized * _gravityMagnitude;
		}
		
		// fall down when in the air
		Debug.DrawRay(transform.position, gravityDirection);
		
		// horizontal movement
		float acceleration = _grounded ? walkAcceleration : airAcceleration;
		float deceleration = _grounded ? groundDeceleration : 0;

		// Update the velocity assignment statements to use our selected
		// acceleration and deceleration values.
		if (moveInput != 0)
		{
			velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
		}
		else
		{
			velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
		}
		
		velocity += velocityDown;
		transform.Translate(velocity * Time.deltaTime);
		
		// collision detection
		Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
		
		_grounded = false;
		foreach (Collider2D hit in hits)
		{
			if (hit == boxCollider)
				continue;

			ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

			if (colliderDistance.isOverlapped)
			{
				// stop moving on collision
				transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
				
				// detect grounding:  angle between the collision normal and the world up is below 90 and velocity is downwards
				if (Vector2.Angle(colliderDistance.normal, -gravityDirection.normalized) < 90&&velocityDown.sqrMagnitude <=0)
				{
					_grounded = true;
				}
			}
		}

    }
}
