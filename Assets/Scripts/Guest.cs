using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : Human {

    public Chair OwnChair;
    private bool _isAtChair;
    private bool _isAtExit;
    public bool IsLeaving = false;
    public GameObject Exit;
    // Use this for initialization
    new void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLeaving)
        {
            GoToChair();
        }
        else
        {
            GoToExit();
        }
    }

    private void GoToExit()
    {
        if (this.Exit)
        {
            if (Vector3.Distance(this.Exit.transform.position, this.transform.position) > 3)
            {
                LookAt(this.meshAgent.steeringTarget);
                this.meshAgent.SetDestination(this.Exit.transform.position);
                _isAtExit = false;
            }
            else
            {
                _isAtExit = true;
            }
        }
    }

    private void GoToChair()
    {
        if (this.OwnChair)
        {
            if (Vector3.Distance(this.OwnChair.transform.position, this.transform.position) > 1)
            {
                LookAt(this.meshAgent.steeringTarget);
                this.meshAgent.SetDestination(this.OwnChair.transform.position);
                _isAtChair = false;
            }
            else
            {
                this.LookingDirection = this.OwnChair.transform.rotation.eulerAngles.z;
                _isAtChair = true;
            }
        }
    }

    public bool IsAtChair
    {
        get { return _isAtChair; }
    }

    public bool IsAtExit
    {
        get { return _isAtExit; }
    }

}
