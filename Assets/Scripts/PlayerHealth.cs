using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour {

	public float HP;
	public Text text;

    PlayerMovement movement;

	public void Start() {
		text.text = HP+"";
        movement = GetComponent<PlayerMovement>();
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
        if(movement.ID == 0){
            GameManager.Player2Won();
        }
        if(movement.ID == 1){
            GameManager.Player1Won();
        }
    }
}
