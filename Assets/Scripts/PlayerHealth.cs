using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
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
        int playerId = GetComponent<PlayerCharacterInputController> ().PlayerID;

        if(playerId == 0){
            GameManager.Player2Won();
        }
        if(playerId == 1){
            GameManager.Player1Won();
        }
    }
}
