using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {
	int leekLifeTime = 0;
	public bool needsWater = true;
	public GameObject leekPrefab;
	public GameObject leek;
	int leekLevel;
	// Use this for initialization
	void Start () {
		StartCoroutine(UpdateCr());
	}
	
	// Update is called once per frame
	void Update () {
		if (needsWater) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 0, 45);
		}
		//UpdateCr ();
		//leekLifeTime++;
	}

	IEnumerator UpdateCr () {
		while (true) {
			yield return new WaitForSeconds (3);
			leekLifeTime++;
		}
	}

	public void WaterIt() {
		if (!needsWater) return;
		needsWater = false;
		StartCoroutine(CoolDownWatering());
	}

	public GameObject HarvestLeek() {
		GameObject tmpLeek = leek;
		leek = null;
		// Reset Earth
		leekLevel = 0;
		needsWater = true;
		return tmpLeek;
	}

	IEnumerator CoolDownWatering () {
		yield return new WaitForSeconds (3);
		if (leekLevel == 0) {
			leek = (GameObject)(GameObject.Instantiate (leekPrefab, transform.localPosition+new Vector3(0,0.2f,0), Quaternion.identity));
		}
		leekLevel++;
		leek.transform.position = transform.localPosition + new Vector3 (0, 0.2f*leekLevel, 0);
		needsWater = true;
	}
}
