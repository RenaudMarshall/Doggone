using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiter : Human {

    public enum WaiterTask
    {
        Idle,
        GetOrder,
        PlaceOrder,
        GetFood,
        DeliverFood,
        GetCheck,
        GetReciept,
        ReturnReciept,
        ChaseDog,
        PartolForDog
    }

    
    public float GrabRange;
    public float DetectionAngle;

    FoodOrder CarriedOrder;

    public Kitchen OwnKitchen;
    public WaiterInteractable OwnRegister;

    public float DogSearchTime = 1;

    public Table[] TablesUnderDuty;
    private WaiterTask currentTask;
    private WaiterTask tabledTask;
    private Table currentTable;

    private Vector3 LastKnownDogLocation;
    private float ChaseCoolDown;
    private float SearchCoolDown;

    // Use this for initialization
    void Start()
    {
        base.Start();
        foreach (Table t in this.TablesUnderDuty)
        {
            t.ResponsibleWaiter = this;
        }
        //MoveTo(TablesUnderDuty[0].WaiterStandingPosition.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        switch (this.currentTask)
        {
            case WaiterTask.ChaseDog:
                ChaseDog();
                break;
            case WaiterTask.PartolForDog:
                PatrolForDog();
                break;
            default:
                DoWork();
                break;
        }
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
            if (t.IsReadyForCheck())
            {
                this.currentTask = WaiterTask.GetCheck;
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
        LookAt(LastKnownDogLocation);
        this.meshAgent.SetDestination(LastKnownDogLocation);
        this.ChaseCoolDown-= Time.deltaTime;
        if (this.ChaseCoolDown <= 0)
        {
            this.currentTask = WaiterTask.PartolForDog;
            this.SearchCoolDown = DogSearchTime * 2;
        }
    }

    private void PatrolForDog()
    {
        const int searchRadius = 20;
        if(Random.value < 0.01)
            this.meshAgent.SetDestination(LastKnownDogLocation + Vector3.up * Random.value * 20 + Vector3.right * Random.value * 20 - new Vector3(20 / 2, 20 / 2, 0));
        LookAt(this.meshAgent.steeringTarget);
        this.SearchCoolDown -= Time.deltaTime;
        if (this.SearchCoolDown <= 0)
        {
            this.currentTask = this.tabledTask;
        }
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
            case WaiterTask.GetCheck:
                GetCheckFromTable(currentTable);
                break;
            case WaiterTask.GetReciept:
                GetReceipt();
                break;
            case WaiterTask.ReturnReciept:
                ReturnReceipt(currentTable);
                break;
            default:
                this.FindWork();
                break;
        }
    }

    private void GetTableOrder(Table t)
    {
        if (!IsEnrouteToTable(t))
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
        if (!IsEnrouteToKitchen())
        {
            this.LookingDirection = OwnKitchen.GetStandingAngle();

            OwnKitchen.PrepOrders.Add(this.CarriedOrder);

            SetWorkerIdle();

        }
    }


    private void GetFoodForTable(Table t)
    {
        if (!IsEnrouteToKitchen())
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
        if (!IsEnrouteToTable(t))
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

    private void GetCheckFromTable(Table t)
    {
        if (!IsEnrouteToTable(t))
        {
            this.LookingDirection = t.GetStandingAngle();

            this.CarriedOrder = t.Order;
            this.currentTask = WaiterTask.GetReciept;
        }
    }
    private void GetReceipt()
    {
        if (!IsEnrouteToRegister())
        {
            this.LookingDirection = OwnRegister.GetStandingAngle();
            if (this.CarriedOrder.Status == FoodOrder.OrderStatus.Check)
                this.CarriedOrder.DoWork(Time.deltaTime);
            else
                this.currentTask = WaiterTask.ReturnReciept;
        }
    }
    private void ReturnReceipt(Table t)
    {
        if (!IsEnrouteToTable(t))
        {
            if (this.CarriedOrder.Status == FoodOrder.OrderStatus.Reciept)
                this.CarriedOrder.DoWork(Time.deltaTime);
            else
                this.SetWorkerIdle();
        }
    }

    private bool IsEnrouteToTable(Table t)
    {
        if (Vector3.Distance(this.transform.position, t.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != t.GetStandingPosition())
                this.meshAgent.SetDestination(t.GetStandingPosition());
            return true;
        }
        return false;
    }
    private bool IsEnrouteToKitchen()
    {
        if (Vector3.Distance(this.transform.position, OwnKitchen.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != OwnKitchen.GetStandingPosition())
                this.meshAgent.SetDestination(OwnKitchen.GetStandingPosition());
            return true;
        }
        return false;
    }
    private bool IsEnrouteToRegister()
    {
        if (Vector3.Distance(this.transform.position, OwnRegister.GetStandingPosition()) > 1)
        {
            if (this.meshAgent.destination != OwnRegister.GetStandingPosition())
                this.meshAgent.SetDestination(OwnRegister.GetStandingPosition());
            return true;
        }
        return false;
    }


    private void SetWorkerIdle()
    {

        this.currentTask = WaiterTask.Idle;
        this.currentTable = null;
        this.CarriedOrder = null;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            float angleFromDetection = Mathf.Abs(Mathf.DeltaAngle(DirectionFrom(other.transform.position), this.LookingDirection));
            if (angleFromDetection < DetectionAngle)
            {
                if(this.currentTask != WaiterTask.ChaseDog && this.currentTask != WaiterTask.PartolForDog)
                    this.tabledTask = this.currentTask;
                this.currentTask = WaiterTask.ChaseDog;
                this.LastKnownDogLocation = other.transform.position;
                this.ChaseCoolDown = DogSearchTime;
            }
        }
    }
}
