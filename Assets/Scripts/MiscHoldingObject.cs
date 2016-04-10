using UnityEngine;
using System.Collections;

public class MiscHoldingObject : HoldingObject {

    public Sprite[] sprites;

	// Use this for initialization
	void Start () {
	    HoldingType = HoldingObjectType.Misc;
        GetComponent<SpriteAnimator>().PlayLoop(GetComponent<SpriteRenderer>(), sprites, 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
