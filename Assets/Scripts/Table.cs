using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : WaiterInteractable {
    [HideInInspector]
    public Chair[] Chairs;
    
    public Waiter ResponsibleWaiter;
    public GameObject dinnerPlate;

    public bool isTaken = false;

    private BoxCollider2D _tableCollider;
    private static FoodIndex foods;

    // Use this for initialization
    void Start ()
    {
        if (!foods)
            foods = GameObject.FindObjectOfType<FoodIndex>();
        //Order = new FoodOrder(2, this);
        _tableCollider = GetComponent<BoxCollider2D>();
        Chairs = GetComponentsInChildren<Chair>();

    }
	
	// Update is called once per frame
	void Update () {
        dinnerPlate.SetActive(false);
		if(this.Order != null)
        {
            if (this.Order.Status == FoodOrder.OrderStatus.Food)
            {
                dinnerPlate.SetActive(true);
                this.Order.DoWork(Time.deltaTime);

                if (Random.value < 0.005)
                {
                    foods.GenerateRandomFoodAtLocation(Human.RandomVector(1, this.transform.position), this.Order.Size / 10);
                }
            }
        }
        if (OccupiedChairCount == this.Chairs.Length)
        {
            _tableCollider.isTrigger = false;
        }
        else
        {
            _tableCollider.isTrigger = true;
        }
    }

    public bool HasOrder()
    {
        return this.Order != null && this.Order.Status == FoodOrder.OrderStatus.Idea;
    }

    public bool IsReadyForCheck()
    {
        return this.Order != null && this.Order.Status == FoodOrder.OrderStatus.Check;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Doggy dog = collision.gameObject.GetComponent<Doggy>();
        if (dog != null)
        {
            dog.TableUnder = this;
        }
    }

    private int OccupiedChairCount
    {
        get
        {
            int count = 0;
            foreach (Chair c in Chairs)
                if (c.IsSatIn)
                    count++;
            return count;
        }
    }

    public Chair[] RandomChairs()
    {
        List <Chair> rngChairs = new List<Chair>(Chairs);
        rngChairs.Sort((x,y) => (int)(Random.value*3) - 1);
        return rngChairs.ToArray();
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Doggy dog = collision.gameObject.GetComponent<Doggy>();
        if (dog != null)
        {
            if(OccupiedChairCount == this.Chairs.Length)
            {
                dog.transform.position = GetStandingPosition();
                this.ResponsibleWaiter.DetectDogAt(GetStandingPosition());
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Doggy dog = collision.gameObject.GetComponent<Doggy>();
        if (dog != null)
        {
            if(dog.TableUnder == this)
                dog.TableUnder = null;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Doggy dog = collision.gameObject.GetComponent<Doggy>();
        if (dog != null)
        {
            if (OccupiedChairCount == this.Chairs.Length)
            {
                this.ResponsibleWaiter.DetectDogAt(dog.transform.position);
            }
        }
    }
}
