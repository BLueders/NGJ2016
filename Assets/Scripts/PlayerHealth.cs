using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float HP;

    public bool IsAlive(){
        return HP > 0;
    }

    public void ReceiveDamage(float damage){
        HP -= damage;
        if(!IsAlive()){
            DIE();
        }
    }

    public void DIE(){
        
    }
}
