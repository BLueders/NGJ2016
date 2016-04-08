using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public bool HasPhysics = true;

    public float MaxHorizontalSpeed = 5;
    public float MaxVerticalSpeed = 5;

    public float HorizontalAcceleration = 5;

    public float JumpForce;

    public Vector2 Gravity = new Vector2(0, 10);

    private Vector2 Velocity;
    private bool grounded = true;
    private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 velocity = myRigidbody.velocity;
        if(!HasPhysics){
            velocity = Velocity;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        bool inputJump = Input.GetButtonDown("Jump");

        velocity = UpdateVelocity(velocity, inputX, inputY);

        if(inputJump && grounded){
            velocity = DoJump(velocity);
        }

        myRigidbody.velocity = velocity;
        Velocity = velocity;
	}

    private Vector2 UpdateVelocity(Vector2 velocity, float inputX, float inputY) {
        velocity += new Vector2(inputX *HorizontalAcceleration * Time.deltaTime, 0);
        velocity -= Gravity * Time.deltaTime;
        velocity = new Vector2(Mathf.Clamp(velocity.x, - MaxHorizontalSpeed, MaxHorizontalSpeed), Mathf.Clamp(velocity.y, -MaxVerticalSpeed, MaxVerticalSpeed));
        return velocity;
    }

    private Vector2 DoJump(Vector2 velocity){
        velocity += new Vector2(0, JumpForce);
        return velocity;
    }
}
