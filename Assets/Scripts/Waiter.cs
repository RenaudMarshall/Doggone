using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

    public Animator head;
    public Animator body;
    public GameObject lookAt;
    private float _lookingDirection = 0;
	// Use this for initialization
	void Start () {
        LookingDirection = 0;
	}
	
	// Update is called once per frame
	void Update () {
        LookingDirection = Mathf.Rad2Deg * Mathf.Atan2(lookAt.transform.position.x - this.transform.position.x, lookAt.transform.position.y - this.transform.position.y);
        //LookingDirection += 1F;
	}

    float LookingDirection
    {
        get
        {
            return _lookingDirection;
        }
        set
        {
            _lookingDirection = value;
            _lookingDirection = (_lookingDirection + 360) % 360;
            body.SetFloat("Angle", _lookingDirection);
            head.SetFloat("Angle", _lookingDirection);
            print(value);
        }
    }
}
