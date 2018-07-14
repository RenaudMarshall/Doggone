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

    public FoodOrder GetOrder()
    {
        FoodOrder order = this.Order;
        this.Order = null;
        return Order;
    }
}
