using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour {
	public Vector3 moveV;

	private float minPosX = -15;
	private float maxPosX = 15;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += moveV*Time.deltaTime;
		if (transform.position.x>maxPosX) {
			transform.position = new Vector3 (minPosX+1,transform.position.y,transform.position.z);
		}
		if (transform.position.x<minPosX) {
			transform.position = new Vector3 (maxPosX-1,transform.position.y,transform.position.z);
		}
	}
}
