using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SpriteAnimator))]
public class LeekExplosion : MonoBehaviour {

    public float ExplosionForcePerLevel;
    public float ExplosionRadiusPerLevel;
    public float ExplosionDamagePerLevel;
    public float Timer = 2;
    public Sprite[] sprites;

    [HideInInspector]
    public float Level;


    public void Update(){
		DebugGizmos.DrawWireSphere(transform.position, Level * ExplosionRadiusPerLevel, Color.red);
    }

    public void GoBoom(){

        SpriteAnimator animator = GetComponent<SpriteAnimator>();
        animator.PlayOneShot(GetComponent<SpriteRenderer>(), sprites, 4);

		transform.localScale = transform.localScale * Level * ExplosionRadiusPerLevel;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Level * ExplosionRadiusPerLevel);

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

