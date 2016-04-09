﻿using UnityEngine;
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

    public float MaxThrowCharge;
    public float ThrowChargePerSecond;
    public float InitialThrowCharge;
    private float currentThrowCharge;

    bool chargeThrowMode = false;
    float pickupdelay;

    PlayerMovement movement;

    // Use this for initialization
    void Start() {
        movement = GetComponent<PlayerMovement>();
    }

    public void HandleAction1(bool buttonPressed, bool buttonDown, bool buttonUp) {
        movement.CurrentMaxHorizontalSpeed = movement.MaxVerticalSpeed; 
        if (buttonDown) {
            if (holdingObject == null) {
                pickupdelay = 0;
                PickUp();
            }
        } else if (buttonPressed) {
            if (holdingObject != null) {
                if (holdingObject.tag == "WaterCan") {
                    movement.CurrentMaxHorizontalSpeed = 1;
                    WaterAction();
                }
                if (holdingObject.tag == "Leek" && chargeThrowMode && currentThrowCharge != MaxThrowCharge) {
                    holdingObject.transform.rotation = holdingObject.transform.rotation * Quaternion.AngleAxis(Random.Range(-10f, 10f), Vector3.forward);
                    currentThrowCharge += Time.deltaTime * ThrowChargePerSecond;
                    currentThrowCharge = Mathf.Clamp(currentThrowCharge, InitialThrowCharge, MaxThrowCharge);
                }
            }
        } 
        if(buttonUp){
            if (holdingObject != null && holdingObject.tag == "Leek" && chargeThrowMode) {
                Throw();
                chargeThrowMode = false;
            }
            if(canThrow)
                chargeThrowMode = true;
        }
    }

    public void HandleAction2(bool buttonPressed, bool buttonDown, bool buttonUp) {
        if(buttonDown){
            Drop();
        }
    }

    // Update is called once per frame
    void Update() {
        pickupdelay += Time.deltaTime;
        if (holdingObject != null) {
			holdingObject.transform.position = holdPivot.transform.position+Vector3.back;
			holdingObject.transform.localScale = holdPivot.transform.lossyScale;
			holdingObject.transform.eulerAngles = new Vector3 (0,0,0);
        }
    }

    void WaterAction() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D col in colliders) {
            
            Earth earth = col.gameObject.GetComponent<Earth>();
            if(earth){
				holdingObject.transform.eulerAngles = new Vector3 (0,0,-45*GetComponent<PlayerMovement>().Direction);
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
        Vector3 throwVelocity = ThrowDirection.normalized * ThrowForce * currentThrowCharge + ThrowMovementFactor*new Vector2(Mathf.Abs(movement.Velocity.x),movement.Velocity.y);
        throwVelocity.x *= movement.Direction;
        leekRigidbody.velocity = throwVelocity;
        leekRigidbody.angularVelocity = Random.Range(140,180) * currentThrowCharge * movement.Direction;
        leekRigidbody.isKinematic = false;
        holdingObject.GetComponent<Collider2D>().isTrigger = false;
        LeekComponent leek = holdingObject.GetComponent<LeekComponent>();
        if(leek != null){
            leek.IsActive = true;
            leek.owner = gameObject;
        }
        holdingObject = null;
        canThrow = false;
        currentThrowCharge = InitialThrowCharge;
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
