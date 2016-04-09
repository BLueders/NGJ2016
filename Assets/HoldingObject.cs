using UnityEngine;
using System.Collections;

public class HoldingObject : MonoBehaviour {

    public enum HoldingObjectType{
        WaterinCan, Leek, Misc
    }

    public HoldingObjectType HoldingType;

    [HideInInspector]
    public GameObject owner;
}

