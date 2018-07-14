using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterInteractable : MonoBehaviour {

    public GameObject WaiterStandingPosition;

    public FoodOrder Order;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetStandingPosition()
    {
        return WaiterStandingPosition.transform.position;
    }
    public float GetStandingAngle()
    {
        return WaiterStandingPosition.transform.rotation.eulerAngles.z;
    }
}
