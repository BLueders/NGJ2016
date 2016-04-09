using UnityEngine;
using System.Collections;

public class WaterCan : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -0.2f, 0);
		if (transform.position.y < -4) {
			transform.position = new Vector3 (transform.position.x, -4, transform.position.z);
		}
	}
}
