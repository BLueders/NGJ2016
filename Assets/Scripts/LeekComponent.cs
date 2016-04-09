using UnityEngine;
using System.Collections;

public class LeekComponent : MonoBehaviour {

	public bool IsActive = false;
    public float Timer = 5;

    public GameObject Explosion;

    public GameObject owner;

    public float LeekLevel;

    void Update(){
        if(!IsActive)
            return;
        Timer -= Time.deltaTime;
        if(Timer < 0){
            Explode();
        }
    }

    void OnCollisionEnter2D(Collider other){
        if(other.gameObject != owner){
            if(other.tag == "Player" || other.tag == "Ground"){
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
        Destroy(gameObject);
    }
}
