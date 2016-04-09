using UnityEngine;
using System.Collections;

public class WateringCan : HoldingObject {

	public GameObject owner;

    void Start (){
        HoldingType = HoldingObjectType.WaterinCan;
    }
}
