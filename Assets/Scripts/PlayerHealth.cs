using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float HP;

    public bool IsAlive(){
        return HP > 0;
    }

}
