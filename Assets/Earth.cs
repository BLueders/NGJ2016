using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {
	public bool needsWater = true;
	public GameObject leekPrefab;
	public LeekComponent leek;

    public float WaterPerSecond;
    public float WaterPerLevel;

    float currentWater;

    Quaternion originalOrientation;

	// Use this for initialization
	void Start () {
        originalOrientation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
		//UpdateCr ();
		//leekLifeTime++;
	}

	public void WaterIt() {
        currentWater += WaterPerSecond * Time.deltaTime;
        if(leek == null){
            leek = (GameObject.Instantiate (leekPrefab, transform.localPosition+new Vector3(0,0.2f,6), Quaternion.identity) as GameObject).GetComponent<LeekComponent>();
        }
        leek.transform.rotation = originalOrientation * Quaternion.AngleAxis(Random.Range(-10f, 10f), Vector3.forward);
        leek.LeekLevel = (int) (currentWater/WaterPerLevel);
	}

	public GameObject HarvestLeek() {
        if(leek == null)
            return null;
		GameObject tmpLeek = leek.gameObject;
		leek = null;
        currentWater = 0;
		return tmpLeek;
	}
}
