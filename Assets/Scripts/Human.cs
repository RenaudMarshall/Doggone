using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human : MonoBehaviour {

    public Animator head;
    public Animator body;
    protected NavMeshAgent meshAgent;
    public float Speed;

    private float _lookingDirection = 0;
    // Use this for initialization
    protected void Start () {

        LookingDirection = 0;

        this.meshAgent = GetComponent<NavMeshAgent>();
        this.meshAgent.updateRotation = false;
        meshAgent.speed = Speed;
    }
	

    public float LookingDirection
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


    protected void MoveTo(Vector2 loc)
    {
        this.meshAgent.destination = loc;
    }


    protected void LookAt(Vector2 loc)
    {
        LookingDirection = DirectionFrom(loc);
    }

    protected float DirectionFrom(Vector2 loc)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(loc.x - this.transform.position.x, loc.y - this.transform.position.y);
    }

    public static Vector3 RandomVector(float radius, Vector3 center)
    {
        return center + Quaternion.Euler(0, 0, 360 * Random.value) * new Vector3(1, 1, 0) * radius * Random.value;
    }
}
