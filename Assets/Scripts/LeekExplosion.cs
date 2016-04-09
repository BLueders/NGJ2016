using UnityEngine;
using System.Collections;

public class LeekExplosion : MonoBehaviour {

    public float ExplosionForcePerLevel;
    public float ExplosionRadiusPerLevel;
    public float ExplosionDamagePerLevel;

    [HideInInspector]
    public float Level;

    public void GoBoom(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Level * ExplosionForcePerLevel);
        foreach (Collider2D col in colliders){
            if(col.gameObject.tag == "Player"){
                col.GetComponent<PlayerHealth>().ReceiveDamage(ExplosionDamagePerLevel * Level);
                Vector2 direction = col.gameObject.transform.position - transform.position;
                direction.Normalize();
                col.GetComponent<PlayerMovement>().AddExternalFoce(ExplosionForcePerLevel * Level * direction);
            }
        }
    }    
}

