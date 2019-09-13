using System;
using UnityEditor.Presets;
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
    private Rigidbody2D rb;

    private Vector2 velocity;
	
    private bool _grounded;
	public Transform center;
	private float _gravityMagnitude = 9.8f;

    private void Awake()
    {      
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        
    }


    private float moveInput;
    private bool jumpInput;
    
    private void Update()
    {
	    moveInput = Input.GetAxisRaw("Horizontal");
	    jumpInput = Input.GetButtonDown("Jump");
    }

    private void OnCollisionStay2D(Collision2D other)
    {
	    _grounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
	    _grounded = false;
    }

    private void FixedUpdate()
    {
	    Vector2 gravityDirection = center.position - transform.position;
	    Vector2 accelerationSide = Vector2.zero;
	    transform.rotation = Quaternion.FromToRotation(-Vector3.up, gravityDirection);
	    
		// jump
		if (_grounded)
		{
			
			if (jumpInput)
			{
				jumpInput = false;
				// Calculate the velocity required to achieve the target jump height.
				rb.AddForce(Mathf.Sqrt(jumpHeight) * -gravityDirection.normalized, ForceMode2D.Impulse);
				_grounded = false;
			}
		}
		var accelerationDown = gravityDirection.normalized * _gravityMagnitude * rb.mass;
		rb.AddForce(accelerationDown);
		
		// fall down when in the air
		Debug.DrawRay(transform.position, gravityDirection);
		
		// horizontal movement
		float acceleration = _grounded ? walkAcceleration : airAcceleration;
		float deceleration = _grounded ? groundDeceleration : 0;

		// Update the velocity assignment statements to use our selected
		// acceleration and deceleration values.
		if (moveInput != 0)
		{
			accelerationSide = speed * moveInput * transform.right;
		}
		else
		{
			//velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
		}
		rb.AddForce(accelerationSide);
		//transform.Translate(velocity * Time.deltaTime);
		
		// collision detection
//		Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
//		
//		_grounded = false;
//		foreach (Collider2D hit in hits)
//		{
//			if (hit == boxCollider)
//				continue;
//
//			ColliderDistance2D colliderDistance = hit.Distance(boxCollider);
//
//			if (colliderDistance.isOverlapped)
//			{
//				// stop moving on collision
//				//transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
//				Debug.Log(colliderDistance.pointA - colliderDistance.pointB);
//				
//				// detect grounding:  angle between the collision normal and the world up is below 90 and velocity is downwards
//				if (Vector2.Angle(colliderDistance.normal, -gravityDirection.normalized) < 90)
//				{
//					_grounded = true;
//				}
//			}
//		}

    }
}
