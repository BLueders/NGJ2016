using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float HP;
	public Text text;

	public void Start() {
		text.text = HP+"";
	}

    public bool IsAlive(){
        return HP > 0;
    }

    public void ReceiveDamage(float damage){
        HP -= damage;
        if(!IsAlive()){
            DIE();
        }
		text.text = HP+"";
    }

    public void DIE(){
        
    }
}
