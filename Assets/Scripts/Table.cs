using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : WaiterInteractable {

    public Chair[] Chairs;

    public FoodOrder Order;
	// Use this for initialization
	void Start () {
        Order = new FoodOrder(10, this);
	}
	
	// Update is called once per frame
	void Update () {
		
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
