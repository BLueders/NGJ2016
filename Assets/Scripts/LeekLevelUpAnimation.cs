using UnityEngine;
using System.Collections;

public class LeekLevelUpAnimation : MonoBehaviour {

    public float LiveTime = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    LiveTime -= Time.deltaTime;
        if(LiveTime <= 0){
            Destroy(gameObject);
        }
	}
}
