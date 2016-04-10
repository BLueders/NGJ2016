using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	Quaternion originalOrientation;

	// Use this for initialization
	void Start () {
		originalOrientation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShakeScreen() {
		StartCoroutine (ShakeScreenCr());
	}

	IEnumerator ShakeScreenCr() {
		int i = 0;
		while (i<20) {
			transform.rotation = originalOrientation * Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.forward);
			i++;
			yield return new WaitForSeconds(0.01f);
		}
		transform.rotation = originalOrientation;
	}
}
