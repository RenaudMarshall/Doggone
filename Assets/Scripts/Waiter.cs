using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : MonoBehaviour {

    public Animator head;
    public Animator body;
    
    public float GrabRange;
    public float DetectionAngle;
    public float Speed;

    FoodOrder CarriedOrder;

    public GameObject Kitchen;
    public GameObject Register;

    public Table[] TablesUnderDuty;
    int activeTable = 0;

    private float _lookingDirection = 0;
	// Use this for initialization
	void Start () {
        LookingDirection = 0;
	}
	
	// Update is called once per frame
	void Update () {
        MoveTo(TablesUnderDuty[0].WaiterStandingPosition.transform.position);
	}

    private void MoveTo(Vector2 loc)
    {
        LookAt(loc);
        this.transform.position = Vector2.MoveTowards(this.transform.position, loc, Speed * Time.deltaTime);
    }


    private void LookAt(Vector2 loc)
    {
        LookingDirection = DirectionFrom(loc);
    }

    private float DirectionFrom(Vector2 loc)
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
                LookAt(other.transform.position);
            }
        }
    }
}
