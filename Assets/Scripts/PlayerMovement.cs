using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float MaxHorizontalSpeed = 5;
    public float MaxVerticalSpeed = 5;

    public float HorizontalAcceleration = 5;

    public float JumpForce;

    public Vector2 Gravity = new Vector2(0, 10);

    public int ID;

    private Vector2 Velocity;
    private bool grounded = true;
    private Rigidbody2D myRigidbody;
	public int Direction = 1;
	public GameObject spritePivot;

    private bool HasControl = true;

    private float noControlTime = 2;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}

    private void TakeAwayControl(){
        HasControl = false;
        myRigidbody.constraints = RigidbodyConstraints2D.None;
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 0.9f;
        material.friction = 0;
        GetComponent<Collider2D>().sharedMaterial = material;
    }

    private void GiveBackControl(){
        HasControl = true;
        transform.rotation = Quaternion.identity;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 0;
        material.friction = 0;
        GetComponent<Collider2D>().sharedMaterial = material;
    }

    private IEnumerator NoControlTimer(){
        yield return new WaitForSeconds(noControlTime);
        GiveBackControl();
    }

	// Update is called once per frame
    public void Move (float inputX, float inputY, bool inputJump) {

        Vector2 velocity = myRigidbody.velocity;

        velocity = UpdateVelocity(velocity, inputX, inputY);

		if (inputX != 0) {
			Direction = inputX > 0 ? 1 : -1;
		}
		spritePivot.transform.localScale = new Vector2 (Direction,spritePivot.transform.localScale.y);
        if(inputJump && grounded){
            velocity = DoJump(velocity);
        }

        myRigidbody.velocity = velocity;
        Velocity = velocity;

		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}

    public void AddExternalFoce(Vector2 force){
        TakeAwayControl();
        myRigidbody.AddForce(force);
        StartCoroutine(NoControlTimer());
    }

    private Vector2 UpdateVelocity(Vector2 velocity, float inputX, float inputY) {
        velocity += new Vector2(inputX *HorizontalAcceleration * Time.deltaTime, 0);
        velocity -= Gravity * Time.deltaTime;
        velocity = new Vector2(Mathf.Clamp(velocity.x, - MaxHorizontalSpeed, MaxHorizontalSpeed), Mathf.Clamp(velocity.y, -MaxVerticalSpeed, MaxVerticalSpeed));
		if (inputX == 0) {
			velocity = new Vector2 (0, velocity.y);
		}
		return velocity;
    }

    private Vector2 DoJump(Vector2 velocity){
        velocity += new Vector2(0, JumpForce);
        return velocity;
    }

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
		}
	}

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground") {
            grounded = false;
        }
    }
}
