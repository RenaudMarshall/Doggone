using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : WaiterInteractable {

    public Chair[] Chairs;
    
    public Waiter ResponsibleWaiter;
   
	// Use this for initialization
	void Start () {
        Order = new FoodOrder(2, this);
	}
	
	// Update is called once per frame
	void Update () {
		if(this.Order != null)
        {
            if (this.Order.Status == FoodOrder.OrderStatus.Food)
                this.Order.DoWork(Time.deltaTime);
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
    public void OnTriggerExit2D(Collider2D collision)
    {
        Doggy dog = collision.gameObject.GetComponent<Doggy>();
        if (dog != null)
        {
            if(dog.TableUnder == this)
                dog.TableUnder = null;
        }
    }
}
