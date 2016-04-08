using UnityEngine;
using System.Collections;

public class ThrowComponent : MonoBehaviour {

	public GameObject holdPivot;
	public GameObject throwPivot;
	public GameObject holdingObject;
	public bool canThrow = true;

	public GameObject leek;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && canThrow) {
			Throw ();
		}
	}

	void Throw() {
		if (holdingObject == null) return;
		holdingObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (5*GetComponent<PlayerMovement>().direction,25);
		holdingObject.GetComponent<Rigidbody2D> ().isKinematic = false;
		holdingObject.GetComponent<BoxCollider2D>().isTrigger = false;
		holdingObject.transform.parent = transform.parent;
		holdingObject = null;
		canThrow = false;
	}
	void OnTriggerStay2D(Collider2D coll) {
		if (Input.GetButtonDown ("Fire2") && holdingObject == null) {
			if (coll.gameObject.tag == "LeekGround") {
				var inst = (GameObject)(GameObject.Instantiate (leek, throwPivot.transform.position, Quaternion.identity));
				inst.transform.parent = holdPivot.transform;
				inst.transform.localPosition = Vector2.zero;
				inst.transform.localScale = leek.gameObject.transform.localScale;
				holdingObject = inst;
				canThrow = true;
			}
		}
	}
}
