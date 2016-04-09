using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float MaxHorizontalSpeed = 5;
    public float CurrentMaxHorizontalSpeed = 5;
    public float MaxVerticalSpeed = 5;

    public float HorizontalAcceleration = 5;
    public float DizziHorizontalAcceleration = 0.5f;
    private float originalAcceleration;

    public float JumpForce;

    public Vector2 Gravity = new Vector2(0, 10);

    public int ID;

    public Vector2 Velocity {get; private set;}
    private bool grounded = true;
    private Rigidbody2D myRigidbody;
	public int Direction = 1;
	public GameObject spritePivot;

	public Animator anim1;
	public Animator anim2;

	private Animator anim;

    private bool HasControl = true;

    private float noControlTime = 2;

	// Use this for initialization
	void Start () {
        CurrentMaxHorizontalSpeed = MaxVerticalSpeed;
        myRigidbody = GetComponent<Rigidbody2D>();
        originalAcceleration = HorizontalAcceleration;
		int playerId = GetComponent<PlayerCharacterInputController> ().PlayerID;
		if (playerId == 0) {
			anim2.gameObject.SetActive (false);
			anim = anim1;
		}
		if (playerId == 1) {
			anim1.gameObject.SetActive (false);
			anim = anim2;
		}
	}

    private void TakeAwayControl(){
        HorizontalAcceleration = DizziHorizontalAcceleration;
        HasControl = false;
        myRigidbody.constraints = RigidbodyConstraints2D.None;
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 0.9f;
        material.friction = 0;
        GetComponent<Collider2D>().sharedMaterial = material;
    }

    private void GiveBackControl(){
        HasControl = true;
        HorizontalAcceleration = originalAcceleration;
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
        Velocity = velocity;

        velocity = UpdateVelocity(velocity, inputX, inputY);

		anim.SetInteger ("State", 0);
		if (inputX != 0) {
			anim.SetInteger ("State", 1);
			Direction = inputX > 0 ? 1 : -1;
		}
		spritePivot.transform.localScale = new Vector2 (Direction,spritePivot.transform.localScale.y);
        if(inputJump && grounded){
            velocity = DoJump(velocity);
        }

        myRigidbody.velocity = velocity;


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
        velocity = new Vector2(Mathf.Clamp(velocity.x, - CurrentMaxHorizontalSpeed, CurrentMaxHorizontalSpeed), Mathf.Clamp(velocity.y, -MaxVerticalSpeed, MaxVerticalSpeed));
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
