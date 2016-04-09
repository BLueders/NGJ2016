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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && !fire1hasBeenPressed) {
			if (canThrow) {
				Throw ();
			}
			fire1hasBeenPressed = true;
		}
		if (Input.GetButtonDown ("Fire2") && !fire2hasBeenPressed) {
			if (!canThrow) {
				Drop ();
			}
			fire2hasBeenPressed = true;
		}


		if (Input.GetButtonUp ("Fire1")) {
			fire1hasBeenPressed = false;
		}
		if (Input.GetButtonUp ("Fire2")) {
			fire2hasBeenPressed = false;
		}
		if (holdingObject != null) {
			holdingObject.transform.position = holdPivot.transform.position;
		}
	}

	void Throw() {
		if (holdingObject == null) return;
		holdingObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5*GetComponent<PlayerMovement>().direction,25);
		holdingObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		holdingObject.GetComponent<BoxCollider2D>().isTrigger = false;
		//holdingObject.transform.parent = transform.parent;
		holdingObject = null;
		canThrow = false;
	}

	void Drop() {
		if (holdingObject == null) return;
		holdingObject.transform.parent = transform.parent;
		holdingObject = null;
		canThrow = false;
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (Input.GetButtonDown ("Fire1") && holdingObject == null && !fire1hasBeenPressed) {
			if (coll.gameObject.tag == "LeekGround") {
				var inst = (GameObject)(GameObject.Instantiate (leek, throwPivot.transform.position, Quaternion.identity));
				holdingObject = inst;
				canThrow = true;
				fire1hasBeenPressed = true;
			}
		}
		if (Input.GetButtonDown ("Fire2") && holdingObject == null && !fire2hasBeenPressed) {
			if (coll.gameObject.tag == "WaterCan") {
				holdingObject = coll.gameObject;
				canThrow = false;
				fire2hasBeenPressed = true;
			}
		}
		if (coll.gameObject.tag == "LeekGround") {
			if (Input.GetButtonDown ("Fire2")) {
				if (holdingObject != null && holdingObject.tag == "WaterCan") {
					coll.gameObject.GetComponent<Earth>().waterAmount++;
				}
			}
		}
	}
}
