using UnityEngine;
using System.Collections;

public class ThrowComponent : MonoBehaviour {

    public GameObject holdPivot;
    public GameObject throwPivot;
    public GameObject holdingObject;
    public bool canThrow = true;
    bool fire1hasBeenPressed;
    bool fire2hasBeenPressed;
    public GameObject leek;

    public Vector2 ThrowDirection;
    public float ThrowForce;
	public float ThrowMovementFactor;

    PlayerMovement movement;

    // Use this for initialization
    void Start() {
        movement = GetComponent<PlayerMovement>();
    }

    public void HandleAction1(bool buttonPressed, bool buttonDown, bool buttonUp) {
        if (buttonDown) {
            DoAction();
        } else if (buttonPressed) {
            if (holdingObject != null) {
                if (holdingObject.tag == "WaterCan") {
                    WaterAction();
                }
            }
        }
    }

    public void HandleAction2(bool buttonPressed, bool buttonDown, bool buttonUp) {
        if(buttonDown){
            Drop();
        }
    }

    // Update is called once per frame
    void Update() {
        if (holdingObject != null) {
            holdingObject.transform.position = holdPivot.transform.position;
        }
    }

    void DoAction() {
        if (holdingObject != null) {
            Throw();
        } else {
            PickUp();
        }
    }

    void WaterAction() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D col in colliders) {
            
            Earth earth = col.gameObject.GetComponent<Earth>();
            if(earth){
                earth.WaterIt();
            }
        }
    }

    void PickUp() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D col in colliders) {
            if (col.gameObject.tag == "LeekGround") {
                holdingObject = col.gameObject.GetComponent<Earth>().HarvestLeek();
                if(holdingObject != null) {
                    canThrow = true;
					break;
                }
            }
            if (col.gameObject.tag == "WaterCan") {
                holdingObject = col.gameObject;
                canThrow = false;
                break;
            }
        }
    }

    void Throw() {
        if (holdingObject == null || !canThrow)
            return;
        Rigidbody2D leekRigidbody = holdingObject.GetComponent<Rigidbody2D>();
		Vector3 throwVelocity = ThrowDirection.normalized * ThrowForce + ThrowMovementFactor*new Vector2(Mathf.Abs(movement.Velocity.x),movement.Velocity.y);
        throwVelocity.x *= movement.Direction;
        leekRigidbody.velocity = throwVelocity;
        leekRigidbody.isKinematic = false;
        holdingObject.GetComponent<BoxCollider2D>().isTrigger = false;
        LeekComponent leek = holdingObject.GetComponent<LeekComponent>();
        if(leek != null){
            leek.IsActive = true;
            leek.owner = gameObject;
        }
        holdingObject = null;
        canThrow = false;
    }

    void Drop() {
        if (holdingObject == null || holdingObject.tag != "WaterCan")
            return;
        Rigidbody2D holdingRigidBody = holdingObject.GetComponent<Rigidbody2D>();
        if(holdingRigidBody){
            holdingRigidBody.velocity = movement.Velocity;
        }
        holdingObject = null;
        canThrow = false;
    }
}
