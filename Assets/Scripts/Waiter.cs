using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

    public Animator head;
    public Animator body;
    
    public float GrabRange;
    public float DetectionAngle;

    private float _lookingDirection = 0;
	// Use this for initialization
	void Start () {
        LookingDirection = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //LookingDirection += 1F;
	}

    private void LookAt(Vector3 loc)
    {
        LookingDirection = DirectionFrom(loc);
    }

    private float DirectionFrom(Vector3 loc)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(loc.x - this.transform.position.x, loc.y - this.transform.position.y);
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
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            float angleFromDetection = Mathf.Abs(Mathf.DeltaAngle(DirectionFrom(other.transform.position), this.LookingDirection));
            if (angleFromDetection < DetectionAngle)
            {
                print("Player Detected");
                LookAt(other.transform.position);
            }
        }
    }
}
