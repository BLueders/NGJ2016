using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {
	int leekLifeTime = 0;
	public int waterAmount;
	// Use this for initialization
	void Start () {
		StartCoroutine(UpdateCr());
	}
	
	// Update is called once per frame
	void Update () {
		//UpdateCr ();
		//leekLifeTime++;
	}

	IEnumerator UpdateCr () {
		while (true) {
			yield return new WaitForSeconds (3);
			leekLifeTime++;
		}
	}
}
