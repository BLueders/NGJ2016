using UnityEngine;
using System.Collections;

public class LeekComponent : MonoBehaviour {

	public bool IsActive = false;
    public float Timer = 5;

    public GameObject Explosion;

    [HideInInspector]
    public GameObject owner;

    [HideInInspector]
    public float LeekLevel;

    void Update(){
        if(!IsActive)
            return;
        Timer -= Time.deltaTime;
        if(Timer < 0){
            Explode();
        }
    }

	void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject != owner){
			if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ground"){
				Explode();
			}
        }
    }

    void Explode(){
        GameObject explosionObject = Instantiate<GameObject>(Explosion);
        explosionObject.transform.position = transform.position;
        explosionObject.transform.rotation = transform.rotation;
        LeekExplosion leekExplosion = explosionObject.GetComponent<LeekExplosion>();
        leekExplosion.Level = LeekLevel;
        leekExplosion.GoBoom();
        Destroy(gameObject);
    }
}
