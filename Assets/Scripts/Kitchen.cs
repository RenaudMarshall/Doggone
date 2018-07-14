using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : WaiterInteractable {

    public List<FoodOrder> PrepOrders;
    public List<FoodOrder> DoneOrders;
    // Use this for initialization
    void Start ()
    {
        PrepOrders = new List<FoodOrder>();
        DoneOrders = new List<FoodOrder>();
    }
	
	// Update is called once per frame
	void Update () {
        List<FoodOrder> tmp = new List<FoodOrder>();
		foreach(FoodOrder food in PrepOrders)
        {
            food.DoWork(Time.deltaTime);
            if (food.Status == FoodOrder.OrderStatus.TransitFood)
                tmp.Add(food);
        }
        foreach (FoodOrder food in tmp)
        {
            print("DONe");
            PrepOrders.Remove(food);
            DoneOrders.Add(food);
        }

    }
}
