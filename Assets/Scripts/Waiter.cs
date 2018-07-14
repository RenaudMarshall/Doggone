using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour {

    public enum WaiterTask
    {
        Idle,
        GetOrder,
        PlaceOrder,
        GetFood,
        DeliverFood,
        GetCheck,
        GetReciept,
        ChaseDog
    }

    public Animator head;
    public Animator body;

    private NavMeshAgent meshAgent;
    
    public float GrabRange;
    public float DetectionAngle;
    public float Speed;

    FoodOrder CarriedOrder;

    public Kitchen OwnKitchen;
    public GameObject OwnRegister;

    public Table[] TablesUnderDuty;
    private WaiterTask currentTask;
    private Table currentTable;

    private float _lookingDirection = 0;
    // Use this for initialization
    void Start()
    {
        LookingDirection = 0;

        this.meshAgent = GetComponent<NavMeshAgent>();
        this.meshAgent.updateRotation = false;
        meshAgent.speed = Speed;
        foreach (Table t in this.TablesUnderDuty)
        {
            t.ResponsibleWaiter = this;
        }
        //MoveTo(TablesUnderDuty[0].WaiterStandingPosition.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        if (this.currentTask != WaiterTask.ChaseDog)
            DoWork();
        else
            ChaseDog();
	}

    private void FindWork()
    {
        foreach(Table t in this.TablesUnderDuty)
        {
            if (t.HasOrder())
            {
                this.currentTask = WaiterTask.GetOrder;
                this.currentTable = t;
                break;
            }
        }
        foreach (FoodOrder food in this.OwnKitchen.DoneOrders)
        {
            if (food.forTable.ResponsibleWaiter == this)
            {
                this.currentTask = WaiterTask.GetFood;
                this.currentTable = food.forTable;
                break;
            }
        }
    }

    private void ChaseDog()
    {

    }

    private void DoWork()
    {
        LookAt(this.meshAgent.steeringTarget);
        switch (currentTask)
        {
            case WaiterTask.GetOrder:
                GetTableOrder(currentTable);
                break;
            case WaiterTask.PlaceOrder:
                PlaceOrder();
                break;
            case WaiterTask.GetFood:
                GetFoodForTable(currentTable);
                break;
            case WaiterTask.DeliverFood:
                DeliverForTable(currentTable);
                break;
            default:
                this.FindWork();
                break;
        }
    }

    private void GetTableOrder(Table t)
    {
        if (Vector3.Distance(this.transform.position, t.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != t.GetStandingPosition())
                this.meshAgent.SetDestination(t.GetStandingPosition());
        }
        else
        {
            this.LookingDirection = t.GetStandingAngle();
            if (t.Order.Status == FoodOrder.OrderStatus.Idea)
                t.Order.DoWork(Time.deltaTime);
            else if (t.Order.Status == FoodOrder.OrderStatus.Order)
            {
                this.CarriedOrder = t.Order;
                this.currentTask = WaiterTask.PlaceOrder;
            }
            else
            {
                //Something went very wrong
            }

        }
    }

    private void PlaceOrder()
    {
        if (Vector3.Distance(this.transform.position, OwnKitchen.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != OwnKitchen.GetStandingPosition())
                this.meshAgent.SetDestination(OwnKitchen.GetStandingPosition());
        }
        else
        {
            this.LookingDirection = OwnKitchen.GetStandingAngle();

            OwnKitchen.PrepOrders.Add(this.CarriedOrder);

            SetWorkerIdle();

        }
    }


    private void GetFoodForTable(Table t)
    {
        if (Vector3.Distance(this.transform.position, OwnKitchen.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != OwnKitchen.GetStandingPosition())
                this.meshAgent.SetDestination(OwnKitchen.GetStandingPosition());
        }
        else
        {
            this.LookingDirection = OwnKitchen.GetStandingAngle();

            foreach (FoodOrder food in this.OwnKitchen.DoneOrders)
            {
                if (food.forTable == t)
                {
                    this.CarriedOrder = food;
                    break;
                }
            }
            this.OwnKitchen.DoneOrders.Remove(this.CarriedOrder);
            this.currentTask = WaiterTask.DeliverFood;
            this.currentTable = t;
            print("Got fooood");
        }
    }

    private void DeliverForTable(Table t)
    {
        if (Vector3.Distance(this.transform.position, t.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != t.GetStandingPosition())
                this.meshAgent.SetDestination(t.GetStandingPosition());
        }
        else
        {
            this.LookingDirection = t.GetStandingAngle();
            if (this.CarriedOrder.Status == FoodOrder.OrderStatus.TransitFood)
            {
                this.CarriedOrder.DoWork(Time.deltaTime);
            }
            else
            {
                print("delivered fooood");
                SetWorkerIdle();
            }

        }
    }

    private void MoveTo(Vector2 loc)
    {
        this.meshAgent.destination = loc;
    }


    private void LookAt(Vector2 loc)
    {
        LookingDirection = DirectionFrom(loc);
    }

    private float DirectionFrom(Vector2 loc)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(loc.x - this.transform.position.x, loc.y - this.transform.position.y);
    }

    private void SetWorkerIdle()
    {

        this.currentTask = WaiterTask.Idle;
        this.currentTable = null;
        this.CarriedOrder = null;
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
