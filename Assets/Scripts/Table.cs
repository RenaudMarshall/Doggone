using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : WaiterInteractable {

    public Chair[] Chairs;
    
    public Waiter ResponsibleWaiter;

    public bool isTaken = false;

    private BoxCollider2D _tableCollider;
   
	// Use this for initialization
	void Start () {
        //Order = new FoodOrder(2, this);
        _tableCollider = GetComponent<BoxCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
		if(this.Order != null)
        {
            if (this.Order.Status == FoodOrder.OrderStatus.Food)
                this.Order.DoWork(Time.deltaTime);
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
