using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SpriteAnimator))]
public class LeekLevelUpAnimation : MonoBehaviour {

    public Sprite[] sprites;
    public float LiveTime = 5;
	// Use this for initialization
	void Start () {
        SpriteAnimator animator = GetComponent<SpriteAnimator>();
        animator.PlayOneShot(GetComponent<SpriteRenderer>(), sprites, 4);
	}
	
	// Update is called once per frame
	void Update () {
	    LiveTime -= Time.deltaTime;
        if(LiveTime <= 0){
            Destroy(gameObject);
        }
	}
}
