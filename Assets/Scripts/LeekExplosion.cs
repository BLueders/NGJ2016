using UnityEngine;
using System.Collections;

public class LeekExplosion : MonoBehaviour {

    public float ExplosionForcePerLevel;
    public float ExplosionRadiusPerLevel;
    public float ExplosionDamagePerLevel;
    public float Timer = 2;

    [HideInInspector]
    public float Level;

    public void Update(){
        DebugGizmos.DrawWireSphere(transform.position, Level * ExplosionForcePerLevel, Color.red);
    }

    public void GoBoom(){
        transform.localScale = transform.localScale * Level * ExplosionForcePerLevel;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Level * ExplosionForcePerLevel);

        foreach (Collider2D col in colliders){
            if(col.gameObject.tag == "Player"){
                col.GetComponent<PlayerHealth>().ReceiveDamage(ExplosionDamagePerLevel * Level);
                Vector2 direction = col.gameObject.transform.position - transform.position;
                direction.Normalize();
                col.GetComponent<PlayerMovement>().AddExternalFoce(ExplosionForcePerLevel * Level * direction);
            }
        }
        DestroyObject();
    }    

    private IEnumerator DestroyObject(){
        yield return new WaitForSeconds(Timer);
        Destroy(gameObject);
    }
}

